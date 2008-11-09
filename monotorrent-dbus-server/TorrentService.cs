// MonoTorrent.cs
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

using NDesk.DBus;
using MonoTorrent.Client;
using MonoTorrent.Common;

namespace MonoTorrent.DBus
{
	public class TorrentService : ITorrentService
	{
		public static readonly Bus Bus = NDesk.DBus.Bus.Session;
		public static readonly string BusName = "org.monotorrent.dbus";
		private const string EnginePath = "/org/monotorrent/{0}/engine";
		private const string EngineSettingsPath = "/org/monotorrent/enginesettings/{0}";
		private const string TorrentSettingsPath = "/org/monotorrent/torrentsettings/{0}";
		private static TorrentService instance = new TorrentService ();
		
		private Dictionary<ObjectPath, EngineSettingsAdapter> engineSettings;
		private Dictionary <string, EngineAdapter> engines;
		private int engineSettingsNumber;
		
		
		public static TorrentService Instance
		{
			get { return instance; }
		}
		
		internal Dictionary<ObjectPath, EngineSettingsAdapter> EngineSettings
		{
			get { return engineSettings; }
		}
		
		
		private TorrentService ()
		{
			engines = new Dictionary<string, EngineAdapter> ();
			engineSettings = new Dictionary<ObjectPath, EngineSettingsAdapter> (new ObjectPathComparer());
		}
		
		
		public string[] AvailableEngines ()
		{
			string[] result = new string[engines.Count];
			engines.Keys.CopyTo (result, 0);
			return result;
		}
		
		private ObjectPath CreateEngine (string name, ObjectPath engineSettings)
		{
			if (string.IsNullOrEmpty (name))
				throw new ArgumentNullException ("name");
			
			if (engineSettings == null)
				throw new ArgumentNullException ("engineSettings");
			
			if (engines.ContainsKey (name))
				return engines[name].Path;

			Console.WriteLine ("Settings: {0}	-	{1}", engineSettings, engineSettings);
			IEngineSettings settings = this.engineSettings[engineSettings];

			ObjectPath path = new ObjectPath (string.Format (EnginePath, name));
			EngineAdapter adapter = new EngineAdapter (name, (EngineSettingsAdapter)settings, path);

			engines.Add(name, adapter);
			TorrentService.Bus.Register (path, adapter);

			return path;
		}
		
		public void DestroyEngine (string name)
		{
			if (string.IsNullOrEmpty (name))
				throw new ArgumentNullException ("name");
			
			if (!engines.ContainsKey (name))
				throw new ArgumentException ("The engine does not exist");
			
			EngineAdapter engine = engines[name];
			engines.Remove (name);
			
			foreach (System.Threading.WaitHandle h in engine.Engine.StopAll ())
				h.WaitOne (2000, true);
			
			engine.SaveState ();
			
			// Dispose will recursively unregister everything from the bus
			engine.Dispose ();
		}
		
		public ObjectPath GetEngine (string name)
		{
	 	   if (!engines.ContainsKey (name))
	 	 	  CreateEngine(name, NewEngineSettings());
			return engines[name].Path;
		}
		
		private ObjectPath NewEngineSettings ()
		{
			ObjectPath path = new ObjectPath (string.Format (EngineSettingsPath, engineSettingsNumber++));
			EngineSettingsAdapter adapter = new EngineSettingsAdapter (new EngineSettings (), path);
			
			TorrentService.Bus.Register (adapter.Path, adapter);
			engineSettings.Add (adapter.Path, adapter);
			
			return adapter.Path;
		}

		#region ITorrentService implementation 
		
		string[] ITorrentService.GetAvailableEngines ()
		{
			return AvailableEngines ();
		}
		
		ObjectPath ITorrentService.GetEngine (string name)
		{
			return GetEngine (name);
		}
		
		void ITorrentService.DestroyEngine (string name)
		{
			DestroyEngine (name);
		}
		
		#endregion 
		
	}
}
