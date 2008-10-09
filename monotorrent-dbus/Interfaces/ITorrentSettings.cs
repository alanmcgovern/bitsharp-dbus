// ITorrentSettings.cs
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
	[Interface ("org.monotorrent.torrentsettings")]
	public interface ITorrentSettings : IExportable
	{
		// Enable/disable InitialSeeding mode (also known as superseeding)
		bool GetInitialSeedingEnabled ();
		void SetInitialSeedingEnabled (bool initialSeeding);
		
		// The maximum download speed for the downloader
		int GetMaxDownloadSpeed ();
		void SetMaxDownloadSpeed (int maxSpeed);
		
		// The maximum upload speed for the downloader
		int GetMaxUploadSpeed ();
		void SetMaxUploadSpeed (int maxSpeed);
		
		// The maximum open connections for the downloader
		int GetMaxConnections ();
		void SetMaxConnections (int maxConnections);
		
		// The maximum number of available upload slots
		int GetUploadSlots ();
		void SetUploadSlots (int slots);
	}
}
