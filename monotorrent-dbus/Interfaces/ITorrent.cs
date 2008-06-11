// ITorrent.cs
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

namespace MonoTorrent.DBus
{
	[Interface ("org.monotorrent.torrent")]
	public interface ITorrent : IExportable
	{
		// This represents all the trackers in their tiers. There are N rows of
		// tiers each of which contains M tracker urls
        string[][] AnnounceUrls { get; }
		
        string Comment { get; }
		
        string CreatedBy { get; }
		
        DateTime CreationDate { get; }
		
        ObjectPath[] Files { get; }
		
        byte[] InfoHash { get; }
		
        bool IsPrivate { get; }
		
        string Name { get; }
		
        int PieceLength { get; }
		
        long Size { get; }
		
        string TorrentPath { get; }
	}
}
