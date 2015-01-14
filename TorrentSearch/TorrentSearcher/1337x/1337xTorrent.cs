namespace TorrentSearch.TorrentSearcher._1337x
{
    public class X1337XTorrent : Torrent
    {
        public X1337XTorrent(string title, string url, uint seed, uint leech, ulong size, string uploader, string magnetLink, uint downloads, TorrentCategory category, uint age, string torrentFile, string id)
        {
            Title = title;
            Seeders = seed;
            Leechers = leech;
            Size = size;
            Uploader = uploader;
            Magnet = magnetLink;
            Downloads = downloads;
            Category = category;
            Age = age;
            PageLink = url;
            TorrentFile = torrentFile;
            Id = id;
        }
    }
}
