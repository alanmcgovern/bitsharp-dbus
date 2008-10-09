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
		bool GetAmChoking ();
		bool GetAmInterested ();
		int GetAmRequestingPiecesCount ();
		EncryptionTypes GetActiveEncryption ();
		int GetHashFails ();
		bool GetIsChoking ();
		bool GetIsInterested ();
		int GetIsRequestingPiecesCount ();
		bool GetIsSeeder ();
		bool GetIsConnected ();
		string GetPeerId ();
		int GetPiecesSent ();
		bool GetSupportsFastPeer ();
		string GetUri (); 
	}
}
