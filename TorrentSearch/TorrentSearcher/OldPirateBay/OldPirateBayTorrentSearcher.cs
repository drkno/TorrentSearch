using System;
using System.IO;
using System.Linq;
using System.Net;

namespace TorrentSearch.TorrentSearcher.OldPirateBay
{
    public class OldPirateBayTorrentSearcher : ITorrentSearcher
    {
        private const string OpenBayUrl = "https://oldpiratebay.org/";
        public Torrent[] Search(string query, TorrentCategory category, TorrentSortOrder sortOrder = TorrentSortOrder.Title,
            TorrentSortOptions sortOptions = TorrentSortOptions.Ascending)
        {
            var url = BuildSearchUrl(query, category, sortOrder, sortOptions);
            var html = GetHtmlData(url);
            return ParseHtmlData(html);
        }

        private static string BuildSearchUrl(string query, TorrentCategory category, TorrentSortOrder sortOrder,
            TorrentSortOptions sortOptions)
        {
            query = query.Replace(' ', '+');
            int iht;
            switch (category)
            {
                case TorrentCategory.All:           iht = 0; break;
                case TorrentCategory.Adult:         iht = 4; break;
                case TorrentCategory.Anime:         iht = 1; break;
                case TorrentCategory.Application:   iht = 2; break;
                case TorrentCategory.Book:          iht = 9; break;
                case TorrentCategory.Game:          iht = 3; break;
                case TorrentCategory.Movie:         iht = 5; break;
                case TorrentCategory.Music:         iht = 6; break;
                case TorrentCategory.Tv:            iht = 8; break;
                default:                            goto case TorrentCategory.All;
            }
            string opt;
            switch (sortOptions)
            {
                case TorrentSortOptions.Ascending: opt = "asc"; break;
                case TorrentSortOptions.Decending: opt = "desc"; break;
                default: goto case TorrentSortOptions.Ascending;
            }
            string so;
            switch (sortOrder)
            {
                case TorrentSortOrder.Age:          so = "&Torrent_sort=created_at." + opt; break;
                case TorrentSortOrder.Files:        goto default;
                case TorrentSortOrder.Leechers:     so = "&Torrent_sort=leechers." + opt; break;
                case TorrentSortOrder.Seeders:      so = "&Torrent_sort=seeders." + opt; break;
                case TorrentSortOrder.Size:         so = "&Torrent_sort=size." + opt; break;
                case TorrentSortOrder.Title:        so = ""; break;
                default:                            goto case TorrentSortOrder.Title;
            }

            return string.Format("{0}search.php?q={1}{2}&iht={3}", OpenBayUrl, query, so, iht);
        }

        private static string GetHtmlData(string url)
        {
            var webRequest = (HttpWebRequest) WebRequest.Create(url);
            webRequest.AllowAutoRedirect = true;
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate |
                                                DecompressionMethods.GZip |
                                                DecompressionMethods.None;
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:34.0) Gecko/20100101 Firefox/34.0";

            var html = "";
            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                var stream = webResponse.GetResponseStream();
                if (stream == null) return html;
                var streamReader = new StreamReader(stream);
                html = streamReader.ReadToEnd();
                streamReader.Close();
            }
            return html;
        }

        private static Torrent[] ParseHtmlData(string htmlData)
        {
            var lines = htmlData.Split(new []{'\r','\n'}, StringSplitOptions.RemoveEmptyEntries);
            return lines.Where(line => line.StartsWith("<td class=\"title-row\">")).Select(line => new OldPirateBayTorrent(line, OpenBayUrl)).Cast<Torrent>().ToArray();
        }
    }
}
