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


        bool CanAnnounce { get; }

        bool CanScrape { get; }

        int Complete { get; }

        int Downloaded { get; }

        string FailureMessage { get; }

        int Incomplete { get; }

        string WarningMessage { get; }


        void Announce();

        void Scrape();
    }
}
