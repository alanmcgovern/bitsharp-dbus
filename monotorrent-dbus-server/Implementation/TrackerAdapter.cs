// Tracker.cs
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
using MonoTorrent.Client.Tracker;
using MonoTorrent.Client;
using NDesk.DBus;

namespace MonoTorrent.DBus
{	
	internal class TrackerAdapter : ITracker
	{
		public event AnnounceHandler AnnounceReceived;
		public event ScrapeHandler ScrapeReceived;
		public event StateChangedHandler StateChanged;
		
		private ObjectPath path;
		private TorrentManager manager;
		private MonoTorrent.Client.Tracker.Tracker tracker;
		

		public TrackerAdapter (TorrentManager manager, MonoTorrent.Client.Tracker.Tracker tracker, ObjectPath path)
		{
			this.manager = manager;
			this.tracker = tracker;
			this.path = path;
		}
		
		
		public bool CanAnnounce {
			get { return true; }
		}

		public bool CanScrape {
			get { return tracker.CanScrape; }
		}

		public int Complete {
			get { return tracker.Complete; }
		}

		public int Downloaded {
			get { return tracker.Downloaded; }
		}

		public string FailureMessage {
			get { return tracker.FailureMessage; }
		}

		public int Incomplete {
			get { return tracker.Incomplete; }
		}
		
		public ObjectPath Path {
			get { return path; }
		}

		public string WarningMessage {
			get { return tracker.WarningMessage; }
		}
		


		public void Announce ()
		{
			manager.TrackerManager.Announce (tracker);
		}

		public void Scrape ()
		{
			manager.TrackerManager.Scrape (tracker);
		}

		#region ITracker implementation

		ObjectPath IExportable.GetPath ()
		{
			return Path;
		}
		
		bool ITracker.GetCanAnnounce ()
		{
			return CanAnnounce;
		}
		
		bool ITracker.GetCanScrape ()
		{
			return CanScrape;
		}
		
		int ITracker.GetComplete ()
		{
			return Complete;
		}
		
		int ITracker.GetDownloaded ()
		{
			return Downloaded;
		}
		
		string ITracker.GetFailureMessage ()
		{
			return FailureMessage;
		}
		
		int ITracker.GetIncomplete ()
		{
			return Incomplete;
		}
		
		string ITracker.GetWarningMessage ()
		{
			return WarningMessage;
		}
		
		void ITracker.Announce ()
		{
			Announce ();
		}
		
		void ITracker.Scrape ()
		{
			Scrape ();
		}
		
		#endregion 
		
	}
}
