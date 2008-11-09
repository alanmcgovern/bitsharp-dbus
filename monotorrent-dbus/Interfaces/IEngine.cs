using System;
using System.Collections.Generic;
using System.Text;
using NDesk.DBus;

namespace MonoTorrent.DBus
{
	public delegate void StatsUpdateHandler();
	
	[Interface ("org.monotorrent.engine")]
	public interface IEngine : IExportable
	{
		event StatsUpdateHandler StatsUpdate;
		
		// The name the engine is identified by
		string GetName ();

		// The settings associated with the engine
		ObjectPath GetSettings ();

		// The combined download speed of all active downloaders
		int GetTotalDownloadSpeed ();

		// The combined upload speed of all active downloaders
		int GetTotalUploadSpeed ();
		
		// Returns all downloaders
		ObjectPath[] GetDownloaders ();
		
		// Parses an ITorrent from a .torrent file
		ObjectPath RegisterTorrent (string torrentPath, string savePath);
		
		// Removes the IDownloader from the engine
		void UnregisterTorrent (ObjectPath downloader);
	}
}
