// TorrentData.cs created with MonoDevelop
// User: alan at 13:49 03/06/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Text;
using System.Xml.Serialization;

using MonoTorrent.Client;
using MonoTorrent.Common;
using MonoTorrent.BEncoding;

namespace MonoTorrent.DBus
{
	public class TorrentData
	{
		private FastResume fastResume;
		private string savePath;
		private TorrentSettings settings;
		private string torrentPath;
		
		public FastResume FastResume
		{
			get { return fastResume; }
			set { fastResume = value; }
		}
		
		public string SavePath
		{
			get { return savePath; }
			set { savePath = value; }
		}
		
		public TorrentSettings Settings
		{
			get { return settings; }
			set { settings = value; }
		}
		
		public string TorrentPath
		{
			get { return torrentPath; }
			set { torrentPath = value; }
		}
		
		
		
		public BEncodedDictionary Serialize ()
		{
			BEncodedDictionary d = new BEncodedDictionary ();
			d.Add ("FastResume", FastResume.Encode ());
			d.Add ("SavePath", (BEncodedString) SavePath);
			d.Add ("TorrentPath", (BEncodedString) TorrentPath);
			
			
			StringBuilder sb = new System.Text.StringBuilder();
			XmlSerializer s = new XmlSerializer (typeof (TorrentSettings));
			using (System.IO.TextWriter writer = new System.IO.StringWriter (sb))
				s.Serialize (writer, Settings);

			d.Add ("Settings", (BEncodedString) sb.ToString ());
			
			return d;
		}
		
		public void Deserialize (BEncodedDictionary dict)
		{
			fastResume = new FastResume ((BEncodedDictionary) dict["FastResume"]);
			savePath = dict["SavePath"].ToString ();
			torrentPath = dict["TorrentPath"].ToString ();

			string sb = dict["Settings"].ToString ();
			XmlSerializer s = new XmlSerializer (typeof (TorrentSettings));
			using (System.IO.TextReader reader = new System.IO.StringReader (sb))
				settings = (TorrentSettings) s.Deserialize (reader);
		}
	}
}
