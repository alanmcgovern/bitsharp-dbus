// ITorrentFile.cs
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
	[Interface ("org.monotorrent.torrentfile")]
	public interface ITorrentFile : IExportable
	{
		// The index of the last piece which contains data for this file
		int EndPieceIndex { get; }
		
		// The length of the file in bytes
		long Length { get; }
		
		// The path where the file will be saved to. This is relative
		// to the 'savePath' supplied to the engine
		string FilePath { get; }
		
		// The progress of the download (between 0 and 1)
		double Progress { get; }
		
		// The priority of this file
		Priority Priority { get; set; }
		
		// The index of the first piece which contains data for this file
		int StartPieceIndex { get; }
	}
}
