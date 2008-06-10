// Engine.cs
//
// Copyright (c) 2008 Alan McGovern (alan.mcgovern@gmail.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//

using System;
using System.Collections.Generic;

using MonoTorrent.Client;
using MonoTorrent.Common;
using MonoTorrent.BEncoding;

using NDesk.DBus;
using org.freedesktop.DBus;

namespace MonoTorrent.DBus
{
	internal class EngineAdapter : IEngine
	{
		private const string SettingsFile = "settings";
		private readonly string StoragePath;
		
		private readonly string DownloaderPath = path.ToString () + "/downloaders/{0}";

		private Dictionary <ObjectPath, TorrentManagerAdapter> downloaders;
		private int downloaderNumber;
		private ClientEngine engine;
		private EngineSettingsAdapter engineSettings;
		private string name;
		private ObjectPath path;

		public ClientEngine Engine
		{
			get { return engine; }
		}

		public bool IsRunning
		{
			get { return engine.IsRunning; }
		}

		public string Name
		{
			get { return name; }
		}
		
		public ObjectPath Path
		{
			get { return path; }
		}

		public ObjectPath Settings 
		{
			get { return engineSettings.Path; }
		}

		public int TotalDownloadSpeed
		{
			get { return engine.TotalDownloadSpeed; }
		}

		public int TotalUploadSpeed
		{
			get { return engine.TotalUploadSpeed; }
		}
		

		public EngineAdapter (string name, EngineSettingsAdapter settings, ObjectPath path)
		{
			this.name = name;
			this.engine = new ClientEngine(settings.Settings);
			this.engineSettings = settings;
			this.path = path;
			
			StoragePath = Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData);
			
			StoragePath = System.IO.Path.Combine (StoragePath, "monotorrent-dbus");
			EnsurePath (StoragePath);
			
			StoragePath = System.IO.Path.Combine (StoragePath, string.Format ("engine-{0}", name));
			EnsurePath (StoragePath);
			
			downloaders = new Dictionary<ObjectPath, TorrentManagerAdapter> (new ObjectPathComparer());
			
			LoadState ();
		}


		public void Dispose ()
		{
			TorrentService.Bus.Unregister (Path);
			TorrentService.Bus.Unregister (Settings);
			
			foreach (TorrentManagerAdapter d in downloaders.Values)
				d.Dispose ();
			
			engine.Dispose ();
		}

		public ObjectPath[] GetRegisteredDownloaders ()
		{
			ObjectPath[] paths = new ObjectPath[downloaders.Count];
			downloaders.Keys.CopyTo (paths, 0);
			return paths;
		}

		public ObjectPath RegisterTorrent (string path, string savePath)
		{
			if (path == null)
				throw new ArgumentNullException ("path");
			if (savePath == null)
				throw new ArgumentNullException ("savePath");
			
			Uri uri = new Uri(path);
			Torrent torrent;
			if (System.IO.File.Exists (path))
			{
				torrent = Torrent.Load (path);
			}
			else
			{
				string[] parts = path.Split ('/');
				string filename = parts.Length > 0 ? System.IO.Path.Combine (StoragePath, parts[parts.Length - 1]) : "";
				Console.WriteLine ("Downloading: {0} to {1}", path, filename);
				torrent = Torrent.Load (uri, filename);
			}
			
			TorrentSettings settings = new TorrentSettings ();
			TorrentManager manager = new TorrentManager (torrent, savePath, settings);
			
			return Load(torrent, settings, manager);
		}

		public void UnregisterTorrent (ObjectPath torrent)
		{
			if (torrent == null)
				throw new ArgumentNullException("torrent");
			
			TorrentManagerAdapter d = downloaders[torrent];
			
			downloaders.Remove (torrent);
			TorrentService.Bus.Unregister (torrent);
			engine.Unregister (d.Manager);
		}
		
		private void LoadState ()
		{
			string settings = System.IO.Path.Combine (StoragePath, SettingsFile);
			if (!System.IO.File.Exists (settings))
				return;
			
			byte[] buffer = System.IO.File.ReadAllBytes (settings);
			BEncodedList list = BEncodedValue.Decode<BEncodedList> (buffer);
			
			List<TorrentData> data = new List<TorrentData>();
			foreach (BEncodedDictionary dict in list)
			{
				TorrentData d = new TorrentData();
				d.Deserialize(dict);
				data.Add(d);
			}
			
			foreach (TorrentData d in data)
			{
				Console.WriteLine ("Reloading: {0}", d.TorrentPath);
				Torrent torrent = Torrent.Load (d.TorrentPath);
				TorrentManager manager = new TorrentManager(torrent, d.SavePath, d.Settings, d.FastResume);
				Load (torrent, d.Settings, manager);
			}
		}
		
		private ObjectPath Load (Torrent torrent, TorrentSettings settings, TorrentManager manager)
		{
			ObjectPath managerPath = new ObjectPath (string.Format (DownloaderPath, downloaderNumber++));
			ObjectPath torrentPath = new ObjectPath (string.Format ("{0}/torrent", managerPath.ToString ()));
			ObjectPath settingsPath = new ObjectPath (string.Format ("{0}/settings", managerPath.ToString ()));
			
			TorrentAdapter tAdapter = new TorrentAdapter (torrent, torrentPath);
			TorrentSettingsAdapter sAdapter = new TorrentSettingsAdapter(settings, settingsPath);
			TorrentManagerAdapter mAdapter = new TorrentManagerAdapter(manager, tAdapter, sAdapter, managerPath);
			
			TorrentService.Bus.Register (tAdapter.Path, tAdapter);
			TorrentService.Bus.Register (sAdapter.Path, sAdapter);
			TorrentService.Bus.Register (mAdapter.Path, mAdapter);
			engine.Register (manager);
			
			downloaders.Add (mAdapter.Path, mAdapter);
			return mAdapter.Path;
		}
		
		public void SaveState ()
		{
			BEncodedList list = new BEncodedList ();
			foreach (TorrentManagerAdapter adapter in this.downloaders.Values)
			{
				TorrentData d = new TorrentData ();
				d.FastResume = adapter.Manager.SaveFastResume ();
				d.SavePath = adapter.Manager.SavePath;
				d.Settings = adapter.Manager.Settings;
				d.TorrentPath = adapter.Manager.Torrent.TorrentPath;
				list.Add (d.Serialize ());
			}
			
			System.IO.File.WriteAllBytes (System.IO.Path.Combine (StoragePath, SettingsFile), list.Encode ());
		}
		
		private void EnsurePath (string path)
		{
			if (!System.IO.Directory.Exists (path))
				System.IO.Directory.CreateDirectory (path);
		}
	}
}
