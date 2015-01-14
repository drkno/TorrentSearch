using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace TorrentSearch.TorrentSearcher.KickassTorrents
{
    public class KickassTorrentSearcher : ITorrentSearcher
    {
        private const string BaseUrl = "https://kickass.so/usearch/";

        public Torrent[] Search(string query, TorrentCategory category, TorrentSortOrder sortOrder = TorrentSortOrder.Title,
            TorrentSortOptions sortOptions = TorrentSortOptions.Ascending)
        {
            var url = BuildUrlString(query, category, sortOrder, sortOptions);
            Debug.WriteLine("URL = " + url);
            var xmlStream = PerformKickassRequest(url);
            var xmlSerializer = new XmlSerializer(typeof(KickassRssContainer));
            var torrents = (KickassRssContainer)xmlSerializer.Deserialize(xmlStream);
            xmlStream.Close();

            return torrents.Channel.Torrents;
        }

        private static Stream PerformKickassRequest(string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:34.0) Gecko/20100101 Firefox/34.0";
            webRequest.AllowAutoRedirect = true;
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate |
                                                DecompressionMethods.GZip |
                                                DecompressionMethods.None;
            var stream = new MemoryStream();
            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                var responseStream = webResponse.GetResponseStream();
                if (responseStream == null)
                {
                    Debug.WriteLine("Response from kickass torrents server was null.");
                    return null;
                }
                var reader = new StreamReader(responseStream);
                var text = reader.ReadToEnd();
                Debug.WriteLine("---- Response ----\n" + text + "\n----   Halt   ----");
                reader.Close();
                var bytes = Encoding.UTF8.GetBytes(text);
                stream.Write(bytes, 0, bytes.Length);
            }
            stream.Position = 0;
            return stream;
        }

        private static string BuildUrlString(string query, TorrentCategory category, TorrentSortOrder sortOrder,
            TorrentSortOptions sortOptions)
        {
            string sortOpt = null, sortOrd = null;
            switch (sortOrder)
            {
                case TorrentSortOrder.Title:        sortOrd = "name"; break;
                case TorrentSortOrder.Size:         sortOrd = "size"; break;
                case TorrentSortOrder.Seeders:      sortOrd = "seeders"; break;
                case TorrentSortOrder.Leechers:     sortOrd = "leechers"; break;
                case TorrentSortOrder.Files:        sortOrd = "files_count"; break;
                case TorrentSortOrder.Age:          sortOrd = "time_add"; break;
            }
            switch (sortOptions)
            {
                case TorrentSortOptions.Ascending:  sortOpt = "asc"; break;
                case TorrentSortOptions.Decending:  sortOpt = "desc"; break;
            }
            return String.Format("{0}{1} category:{2}/?field={3}&sorder={4}&rss=1", BaseUrl, query, category.ToString().ToLower(), sortOrd, sortOpt);
        }
    }
}
