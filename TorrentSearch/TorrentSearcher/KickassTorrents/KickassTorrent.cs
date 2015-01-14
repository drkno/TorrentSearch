using System;
using System.Threading;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TorrentSearch.TorrentSearcher.KickassTorrents
{
    [XmlType(AnonymousType = true, Namespace = "http://xmlns.ezrss.it/0.1/"), XmlRoot("item")]
    public class KickassTorrent : Torrent
    {
        [XmlElement("title", Form = XmlSchemaForm.Unqualified)]
        public string KickassTitle { set { Title = value; } get { return Title; } }

        [XmlElement("category", Form = XmlSchemaForm.Unqualified)]
        public string KickassCategory
        {
            set
            {
                var cultureInfo = Thread.CurrentThread.CurrentCulture;
                var textInfo = cultureInfo.TextInfo;
                value = textInfo.ToTitleCase(value.ToLower());
                TorrentCategory category;
                if (Enum.TryParse(value, out category))
                {
                    Category = category;
                }
            }
            get { return Category.ToString(); }
        }

        [XmlElement("author", Form = XmlSchemaForm.Unqualified)]
        public string KickassAuthor { set { Uploader = value; } get { return Uploader; } }

        [XmlElement("link", Form = XmlSchemaForm.Unqualified)]
        public string KickassLink { set { PageLink = value; } get { return PageLink; } }

        [XmlElement("guid", Form = XmlSchemaForm.Unqualified)]
        public string KickassGuid { get; set; }

        [XmlElement("pubDate", Form = XmlSchemaForm.Unqualified)]
        public string KickassPublicationDate
        {
            set
            {
                DateTime result;
                if (DateTime.TryParse(value, out result))
                {
                    Age = (uint)(DateTime.Now - result).TotalSeconds;
                }
            }
            get { return Age.ToString(); }
        }

        [XmlElement("contentLength")]
        public ulong KickassContentLength
        {
            set { Size = value; }
            get { return Size; }
        }

        [XmlElement("infoHash")]
        public string KickassInfoHash { set { Id = value; } get { return Id; } }

        [XmlElement("magnetURI")]
        public string KickassMagnetUri { set { Magnet = value; } get { return Magnet; } }

        [XmlElement("seeds")]
        public uint KickassSeeds { set { Seeders = value; } get { return Seeders; } }

        [XmlElement("peers")]
        public uint KickassPeers { set { Leechers = value - Seeders; } get { return Leechers + Seeders; } }

        [XmlElement("verified")]
        public bool KickassVerified { set { Trusted = value; } get { return Trusted; } }

        [XmlElement("fileName")]
        public string KickassFileName { set { TorrentFile = value; } get { return TorrentFile; }}

        [XmlElement("enclosure", Form = XmlSchemaForm.Unqualified)]
        public KickassTorrentEnclosure KickassEnclosure { get; set; }
    }
}
