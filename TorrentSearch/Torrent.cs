using System;
using System.Xml.Serialization;

namespace TorrentSearch
{
    public abstract class Torrent
    {
        /// <summary>
        /// This class must be inherited.
        /// </summary>
        [Obsolete]
        protected Torrent() {}

        [XmlIgnore]
        public string Id { get; protected set; }
        [XmlIgnore]
        public string Title { get; protected set; }
        [XmlIgnore]
        public bool Trusted { get; protected set; }
        [XmlIgnore]
        public string Uploader { get; protected set; }
        [XmlIgnore]
        public TorrentCategory Category { get; protected set; }
        [XmlIgnore]
        public ulong Size { get; protected set; }
        [XmlIgnore]
        public uint Seeders { get; protected set; }
        [XmlIgnore]
        public uint Leechers { get; protected set; }
        [XmlIgnore]
        public string Magnet { get; protected set; }
        [XmlIgnore]
        public string TorrentFile { get; protected set; }
        [XmlIgnore]
        public string[] Files { get; protected set; }
        [XmlIgnore]
        public string PageLink { get; protected set; }
        [XmlIgnore]
        public uint Page { get; protected set; }
        [XmlIgnore]
        public uint Age { get; protected set; }
        [XmlIgnore]
        public uint Downloads { get; protected set; }

        public override string ToString()
        {
            return Title;
        }

        public double ShareRatio()
        {
            return (double)Seeders/Leechers;
        }
    }
}
