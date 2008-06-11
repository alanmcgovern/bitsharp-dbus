// IEngineSettings.cs
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
using MonoTorrent.Client.Encryption;

namespace MonoTorrent.DBus
{
	[Interface ("org.monotorrent.enginesettings")]
	public interface IEngineSettings : IExportable
	{
        EncryptionTypes AllowedEncryption { get; set; }
				
        bool HaveSupressionEnabled { get; set; }

        int GlobalMaxConnections { get; set; }

        int GlobalMaxHalfOpenConnections { get; set; }

        int GlobalMaxDownloadSpeed { get; set; }

        int GlobalMaxUploadSpeed { get; set; }

        int ListenPort { get; set; }

        int MaxOpenFiles { get; set; }

        int MaxReadRate { get; set; }

        int MaxWriteRate { get; set; }
		
        bool PreferEncryption { get; set; }
	}
}
