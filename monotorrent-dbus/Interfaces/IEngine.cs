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
        ObjectPath[] GetRegisteredDownloaders();

        bool IsRunning { get; }
		
		string Name { get; }

        ObjectPath Settings { get; }

        int TotalDownloadSpeed { get; }

        int TotalUploadSpeed { get; }

		ObjectPath RegisterTorrent (string torrentPath, string path);
		
		void UnregisterTorrent (ObjectPath path);
    }
}
