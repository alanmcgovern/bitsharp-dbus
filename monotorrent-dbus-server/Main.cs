// Main.cs
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
using MonoTorrent.DBus;
using NDesk.DBus;
using org.freedesktop.DBus;

namespace monotorrentdbusserver
{
	class MainClass
	{
		static readonly Bus bus = NDesk.DBus.Bus.Session;

		static string BusName = "org.monotorrent.dbus";
		static ObjectPath ServicePath = new ObjectPath ("/org/monotorrent/service");
		
		public static void Main(string[] args)
		{
			if (bus.RequestName (BusName) != RequestNameReply.PrimaryOwner)
			{
				Console.WriteLine ("The monotorrent-dbus daemon is already running");
				return;
			}

			bus.Register (MainClass.ServicePath, TorrentService.Instance);
			
			Console.CancelKeyPress += delegate {
				foreach (string name in TorrentService.Instance.AvailableEngines ())
				{
					Console.Write ("Destroying: {0}", name);
					TorrentService.Instance.DestroyEngine (name);
				}
			};

			while (true)
			{
				Console.WriteLine ("Iterate");
				bus.Iterate ();
			}
		}
	}
}
