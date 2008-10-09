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
using System.Collections.Generic;

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
			Console.WriteLine ("Got engine? {0}", engine.GetName());
			
			IDownloader[] downloaders = GetDownloaders ();
			
			while (true)
			{
				System.Threading.Thread.Sleep (1000);
				
				foreach (IDownloader d in downloaders)
				{
					ITorrent torrent = bus.GetObject<ITorrent> (MainClass.BusName, d.GetTorrent());
					ITorrentFile[] files = GetFiles(torrent);
					
					Console.WriteLine ("Name:	 	  {0}", torrent.GetName ());
					Console.WriteLine ("Progress:	   {0:0.00}%", d.GetProgress ());
					Console.WriteLine ("Download Speed: {0:0.00}kB/sec", d.GetDownloadSpeed () / 1024.0);
					Console.WriteLine ("Upload Speed:   {0:0.00}kB/sec", d.GetUploadSpeed () / 1024.0);
					foreach (ITorrentFile file in files)
						Console.WriteLine ("\t\t{0} - {1:0.00}%", file.GetFilePath (), file.GetProgress () * 100);
					
					Console.WriteLine();
					Console.WriteLine();
				}
			}
		}
		
		private ITorrentFile[] GetFiles (ITorrent torrent)
		{
			ObjectPath[] paths = torrent.GetFiles ();
			ITorrentFile[] files = new ITorrentFile[paths.Length];
			for (int i=0; i < paths.Length; i++)
				files[i] = bus.GetObject<ITorrentFile> (MainClass.BusName, paths[i]);
			return files;
		}
		
		private IDownloader[] GetDownloaders ()
		{
			List<IDownloader> downloaders = new List<IDownloader> ();

			foreach (ObjectPath path in engine.GetDownloaders ())
				downloaders.Add(bus.GetObject<IDownloader> (MainClass.BusName, path));
			
			return downloaders.ToArray();
		}
//		
//		private IDownloader LoadTorrent (string path, string savePath)
//		{
//			Console.WriteLine ("Loading torrent from disk: {0}", path);
//			ObjectPath downloaderPath = engine.RegisterTorrent (path, savePath);
//			Console.Write ("Created downloader at: {0}", downloaderPath);
//			
//			IDownloader downloader = bus.GetObject<IDownloader> (MainClass.BusName, downloaderPath);
//			ITorrent torrent = bus.GetObject<ITorrent> (MainClass.BusName, downloader.Torrent);
//			Console.WriteLine ("Torrent Name :{0}", torrent.Name);
//			Console.WriteLine ("Torrent Size :{0:0.00}MB", torrent.Size / (1024.0 * 1024.0));
//			Console.WriteLine ("Files:");
//			ObjectPath[] files = torrent.Files;
//			
//			foreach (ObjectPath p in files)
//			{
//				ITorrentFile file = bus.GetObject<ITorrentFile> (MainClass.BusName, p);
//				Console.WriteLine ("\t\t{0}", file.FilePath);
//			}
//			
//			return downloader;
//		}
//		
		private IEngine ChooseEngine ()
		{
			string[] engines = service.GetAvailableEngines ();
			
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
			
			ObjectPath enginePath = service.GetEngine (name);
			Console.WriteLine ("Got engine at: {0}", enginePath);
			
			return bus.GetObject<IEngine>(MainClass.BusName, enginePath);
		}
	}
}
