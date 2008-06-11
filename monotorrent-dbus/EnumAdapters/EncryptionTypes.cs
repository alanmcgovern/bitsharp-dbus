// EncryptionTypes.cs created with MonoDevelop
// User: alan at 16:37 10/06/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace MonoTorrent.DBus
{
	[Flags]
	public enum EncryptionTypes
	{
		None = 0,
		PlainText = 1,
		RC4Header = 2,
		RC4Full = 4,
		All = PlainText | RC4Header | RC4Full
	}
}
