using System;
using System.Collections.Generic;
using System.Text;
using MonoTorrent.Client;
using MonoTorrent.Common;
using NDesk.DBus;

namespace MonoTorrent.DBus
{
	//public delegate void PeerConnectedHandler (PeerConnectionEventArgs e);
	//public delegate void PeerDisconnectedHandler (PeerConnectionEventArgs e);
	//public delegate void PieceHashedHandler (PieceHashedEventArgs e);
	public delegate void TorrentStateChangedHandler (ObjectPath downloader, TorrentState oldState, TorrentState newState);
	public delegate void PeerHandler (ObjectPath downloader, ObjectPath peer);
	
	[Interface ("org.monotorrent.downloader")]
	public interface IDownloader : IExportable
	{
		event PeerHandler PeerConnected;
		event PeerHandler PeerDisconnected;
		event TorrentStateChangedHandler StateChanged;

		bool GetComplete ();

		int GetDownloadSpeed ();

		bool GetHashChecked ();

		double GetProgress ();

		ObjectPath GetSettings ();

		TorrentState GetState ();
		
		ObjectPath GetTorrent ();
		
		//ObjectPath[][] Trackers ();

		int GetUploadSpeed ();

		//void AddTracker (string uri);
		
		ObjectPath[] GetPeers ();
	
		void HashCheck(bool autoStart);
		
		void Pause();

		//void RemoveTracker (ObjectPath path);
		
		void Start();

		void Stop();
	}
}
