using System;
using System.Collections.Generic;
using System.Text;
using MonoTorrent.Client;
using NDesk.DBus;

namespace MonoTorrent.DBus
{
	[Interface ("org.monotorrent.engine")]
    public interface IEngine : IExportable
    {
		// The name the engine is identified by
		string Name { get; }

		// The settings associated with the engine
        ObjectPath Settings { get; }

		// The combined download speed of all active downloaders
        int TotalDownloadSpeed { get; }

		// The combined upload speed of all active downloaders
        int TotalUploadSpeed { get; }
		
        // Returns all downloaders
		ObjectPath[] GetDownloaders ();
        
		// Parses an ITorrent from a .torrent file
		ObjectPath RegisterTorrent (string torrentPath, string savePath);
        
        // Removes the IDownloader from the engine
		void UnregisterTorrent (ObjectPath downloader);
    }
}
