// ITorrentAdapter.cs
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
using NDesk.DBus;
using MonoTorrent.Common;

namespace MonoTorrent.DBus
{
	internal class TorrentAdapter : ITorrent
	{
		private string filesPath;
		private ObjectPath[] files;
		private ObjectPath path;
		private Torrent torrent;
		
		public ObjectPath Path
		{
			get { return path; }
		}
		
		public Torrent Torrent
		{
			get { return torrent; }
		}
		
		
		public string[][] AnnounceUrls {
			get
			{
				string[][] announces = new string[torrent.AnnounceUrls.Count][];
				for (int i=0; i < announces.Length; i++)
				{
					announces[i] = new string[torrent.AnnounceUrls[i].Count];
					for (int j=0; j < announces[i].Length; j++)
						announces[i][j] = torrent.AnnounceUrls[i][j];
				}
				
				return announces;
			}
		}

		public string Comment {
			get { return torrent.Comment; }
		}

		public string CreatedBy {
			get { return torrent.CreatedBy; }
		}

		public DateTime CreationDate {
			get { return torrent.CreationDate; }
		}

		public ObjectPath[] Files {
			get { return files; }
		}

		public byte[] InfoHash {
			get { return torrent.InfoHash; }
		}

		public bool IsPrivate {
			get { return torrent.IsPrivate; }
		}

		public string Name {
			get { return torrent.Name; }
		}

		public int PieceLength {
			get { return torrent.PieceLength; }
		}

		public long Size {
			get { return torrent.Size; }
		}

		public string TorrentPath {
			get { return torrent.TorrentPath; }
		}
		
		public TorrentAdapter (Torrent torrent, ObjectPath path)
		{
			this.path = path;
			this.torrent = torrent;
			this.filesPath = path.ToString () + "/files/{0}";
			
			files = new ObjectPath[torrent.Files.Length];
			for (int i=0; i < files.Length; i++)
			{
				ObjectPath p = new ObjectPath (string.Format(filesPath, i));
				TorrentFileAdapter adapter = new TorrentFileAdapter (torrent.Files[i], p);
				files[i] = p;
				TorrentService.Bus.Register (p, adapter);
			}
		}
		
		public void Dispose ()
		{
			TorrentService.Bus.Unregister (Path);
			
			foreach (ObjectPath path in files)
				TorrentService.Bus.Unregister (path);
		}

		#region ITorrent implementation 

		ObjectPath IExportable.GetPath ()
		{
			return Path;
		}
		
		string[][] ITorrent.GetAnnounceUrls ()
		{
			return AnnounceUrls;
		}
		
		string ITorrent.GetComment ()
		{
			return Comment;
		}
		
		string ITorrent.GetCreatedBy ()
		{
			return CreatedBy;
		}
		
		DateTime ITorrent.GetCreationDate ()
		{
			return CreationDate;
		}
		
		ObjectPath[] ITorrent.GetFiles ()
		{
			return Files;
		}
		
		byte[] ITorrent.GetInfoHash ()
		{
			return InfoHash;
		}
		
		bool ITorrent.GetIsPrivate ()
		{
			return IsPrivate;
		}
		
		string ITorrent.GetName ()
		{
			return Name;
		}
		
		int ITorrent.GetPieceLength ()
		{
			return PieceLength;
		}
		
		long ITorrent.GetSize ()
		{
			return Size;
		}
		
		string ITorrent.GetTorrentPath ()
		{
			return TorrentPath;
		}
		
		#endregion 
		
	}
}
