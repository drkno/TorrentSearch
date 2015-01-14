using System.Xml.Serialization;

namespace TorrentSearch.TorrentSearcher.KickassTorrents
{
    [XmlType(AnonymousType = true)]
    public class KickassTorrentEnclosure
    {
        [XmlAttribute("url")]
        public string TorrentFileUrl { get; set; }

        [XmlAttribute("length")]
        public ulong Length { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}
