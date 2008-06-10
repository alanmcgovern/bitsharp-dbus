// EnumAdapter.cs created with MonoDevelop
// User: alan at 16:38 10/06/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace MonoTorrent.DBus
{
	public static class EnumAdapter
	{
				
		public static TorrentState Adapt (MonoTorrent.Common.TorrentState state)
		{
			switch (state)
			{
				case MonoTorrent.Common.TorrentState.Downloading:
					return TorrentState.Downloading;
					
				case MonoTorrent.Common.TorrentState.Hashing:
					return TorrentState.Hashing;
					
				case MonoTorrent.Common.TorrentState.Paused:
					return TorrentState.Paused;
					
				case MonoTorrent.Common.TorrentState.Seeding:
					return TorrentState.Seeding;
					
				case MonoTorrent.Common.TorrentState.Stopped:
					return TorrentState.Stopped;
					
				default:
					return TorrentState.Stopped;
			}
		}
				
		public static MonoTorrent.DBus.Priority Adapt (MonoTorrent.Common.Priority priority)
		{
			switch (priority)
			{
				case MonoTorrent.Common.Priority.Immediate:
					return MonoTorrent.DBus.Priority.Immediate;
					
				case MonoTorrent.Common.Priority.Highest:
					return MonoTorrent.DBus.Priority.Highest;
					
				case MonoTorrent.Common.Priority.High:
					return MonoTorrent.DBus.Priority.High;
					
				case MonoTorrent.Common.Priority.Normal:
					return MonoTorrent.DBus.Priority.Normal;
					
				case MonoTorrent.Common.Priority.Low:
					return MonoTorrent.DBus.Priority.Low;
					
				case MonoTorrent.Common.Priority.Lowest:
					return MonoTorrent.DBus.Priority.Lowest;
					
				case MonoTorrent.Common.Priority.DoNotDownload:
					return MonoTorrent.DBus.Priority.DoNotDownload;
			}
			
			return MonoTorrent.DBus.Priority.Normal;
		}
		
		public static MonoTorrent.Common.Priority Adapt (MonoTorrent.DBus.Priority priority)
		{
			switch (priority)
			{
				case MonoTorrent.DBus.Priority.Immediate:
					return MonoTorrent.Common.Priority.Immediate;
					
				case MonoTorrent.DBus.Priority.Highest:
					return MonoTorrent.Common.Priority.Highest;
					
				case MonoTorrent.DBus.Priority.High:
					return MonoTorrent.Common.Priority.High;
					
				case MonoTorrent.DBus.Priority.Normal:
					return MonoTorrent.Common.Priority.Normal;
					
				case MonoTorrent.DBus.Priority.Low:
					return MonoTorrent.Common.Priority.Low;
					
				case MonoTorrent.DBus.Priority.Lowest:
					return MonoTorrent.Common.Priority.Lowest;
					
				case MonoTorrent.DBus.Priority.DoNotDownload:
					return MonoTorrent.Common.Priority.DoNotDownload;
			}
			
			return MonoTorrent.Common.Priority.Normal;
		}
	}
}
