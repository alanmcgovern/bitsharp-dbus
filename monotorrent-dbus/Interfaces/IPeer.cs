// IPeer.cs created with MonoDevelop
// User: alan at 15:58 05/06/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using NDesk.DBus;
using org.freedesktop.DBus;

namespace MonoTorrent.DBus
{
	[Interface ("org.monotorrent.peer")]
	public interface IPeer : IExportable
	{
        bool AmChoking { get; }
        bool AmInterested { get; }
        int AmRequestingPiecesCount { get; }
		EncryptionTypes ActiveEncryption { get; }
        int HashFails { get; }
        bool IsChoking { get; }
        bool IsInterested { get; }
        int IsRequestingPiecesCount { get; }
        bool IsSeeder { get; }
        bool IsConnected { get; }
        string PeerId { get; }
        int PiecesSent { get; }
        bool SupportsFastPeer { get; }
        string Uri { get; } 
	}
}
