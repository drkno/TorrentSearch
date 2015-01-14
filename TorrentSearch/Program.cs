using System;
using System.Collections.Generic;
using TorrentSearch.TorrentSearcher.KickassTorrents;
using TorrentSearch.TorrentSearcher.OldPirateBay;
using TorrentSearch.TorrentSearcher._1337x;

namespace TorrentSearch
{
    public static class Program
    {
        static void Main()
        {
            ITorrentSearcher[] searchers =
            {
                new KickassTorrentSearcher(), new OldPirateBayTorrentSearcher(),
                new X1337XTorrentSearcher(),
            };

            var query = "the newsroom s03e01";
            var category = TorrentCategory.Tv;
            var sort = TorrentSortOrder.Title;
            var ord = TorrentSortOptions.Ascending;

            var results = new List<Torrent>();
            foreach (var torrentSearcher in searchers)
            {
                results.AddRange(torrentSearcher.Search(query, category, sort, ord));
            }

            foreach (var torrent in results)
            {
                Console.WriteLine(torrent);
            }

            Console.ReadKey();
        }
    }
}
