// TorrentDownloader.cs
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
using MonoTorrent.Common;
using MonoTorrent.Client;
using NDesk.DBus;

namespace MonoTorrent.DBus
{
	internal class TorrentManagerAdapter : IDownloader
	{
		//public event PeerConnectedHandler PeerConnected;
		//public event PeerDisconnectedHandler PeerDisconnected;
		//public event PieceHashedHandler PieceHashed;
		public event TorrentStateChangedHandler StateChanged;
		
		private ObjectPath path;
		private TorrentManager manager;
		private List<PeerAdapter> peers;
		private TorrentSettingsAdapter settingsAdapter;
		private TorrentAdapter torrent;
		private ObjectPath[][] trackers;
		private int trackerNumber;
		private int peerNumber;
		
		public TorrentManagerAdapter (TorrentManager manager, TorrentAdapter torrent, TorrentSettingsAdapter settings, ObjectPath path)
		{
			this.manager = manager;
			this.torrent = torrent;
			this.settingsAdapter = settings;
			this.path = path;
			this.peers = new List<PeerAdapter>();
			
			manager.TorrentStateChanged += delegate (object sender, TorrentStateChangedEventArgs e) {
				if (StateChanged != null)
					StateChanged (path, EnumAdapter.Adapt(e.OldState), EnumAdapter.Adapt (e.NewState));
			};
			
			manager.PeerConnected += AddPeer;
			manager.PeerDisconnected += RemovePeer;
			
			LoadTrackers (manager.TrackerManager.TrackerTiers);
		}

		public bool Complete
		{
			get { return manager.Complete; }
		}

		public int DownloadSpeed
		{
			get { return manager.Monitor.DownloadSpeed; }
		}

		public bool HashChecked
		{
			get { return manager.HashChecked; }
		}
		
		public ObjectPath Path
		{
			get { return path; }
		}

		public double Progress
		{
			get { return manager.Progress; }
		}

		public ObjectPath Settings
		{
			get { return settingsAdapter.Path; }
		}
		
		public TorrentManager Manager
		{
			get { return manager; }
		}

		public TorrentState State
		{
			get { return EnumAdapter.Adapt (manager.State); }
		}
		
		public ObjectPath Torrent
		{
			get { return torrent.Path; }
		}

		public ObjectPath[][] Trackers {
			get { return trackers; }
		}
		
		public int UploadSpeed
		{
			get { return manager.Monitor.UploadSpeed; }
		}

		
		public void Dispose ()
		{
			TorrentService.Bus.Unregister (Path);
			TorrentService.Bus.Unregister (Settings);
			
			foreach (ObjectPath[] paths in this.trackers)
				foreach (ObjectPath path in paths)
					TorrentService.Bus.Unregister (path);
			
			torrent.Dispose ();
		}
		
		public void Start ()
		{
			manager.Start ();
		}

		public void Stop ()
		{
			manager.Stop ();
		}

		public void HashCheck (bool autoStart)
		{
			manager.HashCheck (autoStart);
		}

		public void Pause ()
		{
			manager.Pause ();
		}
		
		private void LoadTrackers (MonoTorrent.Client.Tracker.TrackerTier[] tiers)
		{
			trackers = new ObjectPath[tiers.Length] [];
			
			for (int i = 0; i < trackers.Length; i++)
			{
				trackers[i] = new ObjectPath[tiers[i].Trackers.Count];
				for (int j = 0; j < trackers[i].Length; j++)
				{
					ObjectPath path = new ObjectPath(string.Format("{0}/{1}", Path, trackerNumber++));
					TorrentService.Bus.Register(path, tiers[i].Trackers[j]);
					trackers[i][j] = path;
				}
			}
		}

		public void AddTracker (string uri)
		{
			// FIXME: Support this
		}

		public ObjectPath[] GetPeers ()
		{
			throw new NotImplementedException();
		}

		public void RemoveTracker (ObjectPath path)
		{
			// FIXME: Support this
		}
		
		private void AddPeer (object sender, PeerConnectionEventArgs e)
		{
			ObjectPath path = new ObjectPath(string.Format("{0}/peers/{1}", Path.ToString(), peerNumber++));
			PeerAdapter d = new PeerAdapter(e.PeerID, path);
			TorrentService.Bus.Register (path, d);
			lock (peers)
				peers.Add(d);
		}
		
		public void RemovePeer(object sender, PeerConnectionEventArgs e)
		{
			lock (peers)
			{
				foreach (PeerAdapter p in peers)
				{
					if (p.Id != e.PeerID)
						continue;
					
					peers.Remove(p);
					break;
				}
			}
		}
	}
}
