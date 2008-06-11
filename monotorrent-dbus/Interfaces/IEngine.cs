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

		// Returns true if the ITorrent is already loaded in the engine
		bool IsRegistered (ObjectPath torrent);
		
		// Parses an ITorrent from a .torrent file
		ObjectPath LoadTorrent (string path);
		
		// Returns an existing downloader for the ITorrent
		ObjectPath GetDownloader (ObjectPath torrent);
		
		// Returns all downloaders
		ObjectPath[] GetDownloaders ();
		
		// Create an IDownloader for the specified ITorrent
		// The IDownloader will save data to the specified path
		ObjectPath RegisterTorrent (ObjectPath torrent, string savePath);
		
		// Removes the IDownloader from the engine
		void UnregisterTorrent (ObjectPath downloader);
    }
}
