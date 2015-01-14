using System.Xml.Serialization;

namespace TorrentSearch.TorrentSearcher.KickassTorrents
{
    [XmlType(AnonymousType = true)]
    [XmlRoot("rss", Namespace = "", IsNullable = false)]
    public class KickassRssContainer
    {
        [XmlAttribute("version")]
        public double Version { get; set; }

        [XmlElement("channel")]
        public KickassChannelContainer Channel { get; set; }

        [XmlType(AnonymousType = true)]
        public class KickassChannelContainer
        {
            [XmlElement("title")]
            public string Title { get; set; }

            [XmlElement("link")]
            public string Link { get; set; }

            [XmlElement("description")]
            public string Description { get; set; }

            [XmlElement("item")]
            public KickassTorrent[] Torrents { get; set; }
        }
    }
}
