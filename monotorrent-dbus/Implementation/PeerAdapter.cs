// PeerAdapter.cs created with MonoDevelop
// User: alan at 15:58 05/06/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using MonoTorrent.Client;
using MonoTorrent.Client.Encryption;

using NDesk.DBus;

namespace MonoTorrent.DBus
{
	public class PeerAdapter : IPeer
	{
		private ObjectPath path;
		private PeerId id;
		
		public PeerAdapter(PeerId id, ObjectPath path)
		{
			this.id = id;
			this.path = path;
		}
		
		public bool AmChoking {
			get { return id.AmChoking; }
		}

		public bool AmInterested {
			get { return id.AmInterested; }
		}

		public int AmRequestingPiecesCount {
			get { return id.AmRequestingPiecesCount; }
		}

		public EncryptionTypes ActiveEncryption {
			get
			{
				if (id.Encryptor is RC4)
					return EncryptionTypes.RC4Full;
				if (id.Encryptor is PlainTextEncryption)
					return EncryptionTypes.PlainText;
				return EncryptionTypes.RC4Header;
			}
		}

		public int HashFails {
			get { return id.HashFails; }
		}
		
		public PeerId Id {
			get { return id; }
		}

		public bool IsChoking {
			get { return id.IsChoking; }
		}

		public bool IsInterested {
			get { return id.IsInterested; }
		}

		public int IsRequestingPiecesCount {
			get { return id.IsRequestingPiecesCount; }
		}

		public bool IsSeeder {
			get { return id.IsSeeder; }
		}

		public bool IsConnected {
			get { return id.IsValid; }
		}

		public string Location {
			get { return id.Location.ToString (); }
		}
		
		public ObjectPath Path {
			get { return path; }
		}

		public string PeerId {
			get { return id.Name; }
		}

		public int PiecesSent {
			get { return id.PiecesSent; }
		}

		public int SendQueueLength {
			get { return id.SendQueueLength; }
		}

		public bool SupportsFastPeer {
			get { return id.SupportsFastPeer; }
		}
	}
}
