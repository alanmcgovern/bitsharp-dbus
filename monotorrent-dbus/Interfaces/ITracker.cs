using System;
using NDesk.DBus;
using MonoTorrent.Client.Tracker;
using MonoTorrent.Client;

namespace MonoTorrent.DBus
{
	[Interface ("org.monotorrent.tracker")]
    interface ITracker : IExportable
    {
        event AnnounceHandler AnnounceReceived;

        event ScrapeHandler ScrapeReceived;

        event StateChangedHandler StateChanged;


		// True if the announcing is supported
        bool CanAnnounce { get; }

		// True if scraping is supported
        bool CanScrape { get; }

		// The number of seeds
        int Complete { get; }

		// The number of time the torrent has been downloaded
        int Downloaded { get; }

		// The failure message if the tracker could not be contacted
        string FailureMessage { get; }

		// The number of leeches
        int Incomplete { get; }

		// The warning message (if any)
        string WarningMessage { get; }

		
		// Announce to the tracker
        void Announce();

		// Scrape the tracker
        void Scrape();
    }
}
