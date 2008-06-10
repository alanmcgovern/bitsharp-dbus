// ClientMode.cs
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
using NDesk.DBus;
using org.freedesktop.DBus;
using MonoTorrent.DBus;

namespace Sample
{
	public class ClientMode
	{
		Bus bus;
		ITorrentService service;
		IEngine engine;
		
		public ClientMode()
		{
			bus = TorrentService.Bus;
			
			service = bus.GetObject<ITorrentService> (MainClass.BusName, MainClass.ServicePath);
			Console.WriteLine ("Got service? {0}", service != null);
			
			engine = ChooseEngine();
			Console.WriteLine ("Got engine? {0}", engine.Name);
			
			IDownloader downloader = GetDownloader ();
			
			Console.WriteLine ("Getting ITorrent from: {0}", downloader.Torrent);
			ITorrent torrent = bus.GetObject<ITorrent> (MainClass.BusName, downloader.Torrent);
			
			Console.WriteLine ("Getting files from: {0}", torrent.Files[0]);
			ITorrentFile[] files = GetFiles(torrent);
			
			if (downloader.State == TorrentState.Stopped)
				downloader.Start ();
			
			int counter = 0;
			while (counter++ < 10)
			{
				System.Threading.Thread.Sleep (1000);
				//Console.Clear ();
				Console.WriteLine ("Name:           {0}", torrent.Name);
				Console.WriteLine ("Progress:       {0:0.00}%", downloader.Progress);
				Console.WriteLine ("Download Speed: {0:0.00}kB/sec", downloader.DownloadSpeed / 1024.0);
				Console.WriteLine ("Upload Speed:   {0:0.00}kB/sec", downloader.UploadSpeed / 1024.0);
				foreach (ITorrentFile file in files)
					Console.WriteLine ("\t\t{0} - {1:0.00}%", file.FilePath, file.Progress * 100);
			}
			downloader.Stop ();
		}
		
		private ITorrentFile[] GetFiles (ITorrent torrent)
		{
			ObjectPath[] paths = torrent.Files;
			ITorrentFile[] files = new ITorrentFile[paths.Length];
			for (int i=0; i < paths.Length; i++)
				files[i] = bus.GetObject<ITorrentFile> (MainClass.BusName, paths[i]);
			return files;
		}
		
		private IDownloader GetDownloader ()
		{
			ObjectPath[] downloaders = engine.GetRegisteredDownloaders ();
			if (downloaders.Length > 0)
			{
				Console.WriteLine ("Getting downloader from: {0}", downloaders[0]);
				return bus.GetObject<IDownloader> (MainClass.BusName, downloaders[0]);
			}
			
			return LoadTorrent ("a.torrent", "/home/alan/Desktop");
		}
		
		private IDownloader LoadTorrent (string path, string savePath)
		{
			Console.WriteLine ("Loading torrent from disk: {0}", path);
			ObjectPath downloaderPath = engine.RegisterTorrent (path, savePath);
			Console.Write ("Created downloader at: {0}", downloaderPath);
			
			IDownloader downloader = bus.GetObject<IDownloader> (MainClass.BusName, downloaderPath);
			ITorrent torrent = bus.GetObject<ITorrent> (MainClass.BusName, downloader.Torrent);
			Console.WriteLine ("Torrent Name :{0}", torrent.Name);
			Console.WriteLine ("Torrent Size :{0:0.00}MB", torrent.Size / (1024.0 * 1024.0));
			Console.WriteLine ("Files:");
			ObjectPath[] files = torrent.Files;
			
			foreach (ObjectPath p in files)
			{
				ITorrentFile file = bus.GetObject<ITorrentFile> (MainClass.BusName, p);
				Console.WriteLine ("\t\t{0}", file.FilePath);
			}
			
			return downloader;
		}
		
		private IEngine ChooseEngine ()
		{
			string[] engines = service.AvailableEngines ();
			
			Console.WriteLine ("Which engine would you like to connect to?");
			Console.WriteLine ("Alternatively, enter a new name to create a new engine");
			
			for (int i=0; i < engines.Length; i++)
				Console.WriteLine ("{0}: {1}", i + 1, engines[i]);
			

			string name = null;
			while (string.IsNullOrEmpty (name))
			{
				Console.Write ("Enter the engine name to use: ");
				name = Console.ReadLine ();
			}
			
			ObjectPath enginePath;
			if (Array.Exists(engines, delegate (string s) { return s == name; }))
			{
				enginePath = service.GetEngine (name);
				Console.WriteLine ("Got existing engine");
			}
			else
			{
				ObjectPath settingsPath = service.NewEngineSettings ();
				Console.WriteLine ("Created new engine settings at: {0}", settingsPath);
				
				IEngineSettings settings = bus.GetObject<IEngineSettings>(MainClass.BusName, settingsPath);
				Console.WriteLine ("Got settings? {0}", settings != null);
				
				enginePath = service.CreateEngine (name, settings.Path);
				Console.WriteLine ("Engine created at: {0}", engine);
			}
			
			return bus.GetObject<IEngine>(MainClass.BusName, enginePath);
		}
	}
}
