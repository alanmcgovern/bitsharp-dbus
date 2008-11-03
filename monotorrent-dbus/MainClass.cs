// MainClass.cs
//
// Copyright (c) 2008 Alan McGovern
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

namespace MonoTorrent.DBus
{
	
	
	public class MainClass
	{
		static string BusName = "org.monotorrent.dbus";
		static ObjectPath ServicePath = new ObjectPath ("/org/monotorrent/service");
		static Bus bus;
		static TorrentService service;
		
		static void Main (string[] args)
		{
			bus = TorrentService.Bus;

			service = TorrentService.Instance;
			bus.Register (MainClass.ServicePath, service);
			Console.CancelKeyPress += delegate {
				foreach (string name in service.AvailableEngines ())
				{
					Console.Write ("Destroying: {0}", name);
					service.DestroyEngine (name);
				}
				System.Threading.Thread.Sleep (1000);
			};

			while (true)
			{
				Console.WriteLine ("Iterate");
				bus.Iterate ();
			}
		}
	}
}
