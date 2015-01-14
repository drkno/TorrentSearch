using System.Text.RegularExpressions;

namespace TorrentSearch.TorrentSearcher.OldPirateBay
{
    public class OldPirateBayTorrent : Torrent
    {
        public OldPirateBayTorrent(string line, string baseUrl)
        {
            Magnet = Regex.Match(line, "(?<=(<a href='))magnet:[?]xt=.*(?=(' title='MAGNET LINK'>))").Value;
            Title = Regex.Match(line, "(?<=(\"><span>))[^\"><]+(?=(</span></a>))").Value;
            var url = Regex.Match(line, "(?<=(<a href=\"))/torrent/[0-9]+/[^\"><]+(?=(\"><span>))").Value;
            Id = Regex.Match(url, "(?<=(/torrent/))[0-9]+").Value;
            PageLink = baseUrl + url.Substring(1);

            var category = Regex.Match(line, "(?<=( torrents\">))[a-zA-Z& ]+(?=(</a>))").Value;
            switch (category)
            {
                case "Anime": Category = TorrentCategory.Anime; break;
                case "Adult": Category = TorrentCategory.Adult; break;
                case "Software": Category = TorrentCategory.Application; break;
                case "Books": Category = TorrentCategory.Book; break;
                case "Games": Category = TorrentCategory.Game; break;
                case "Movies": Category = TorrentCategory.Movie; break;
                case "Music": Category = TorrentCategory.Music; break;
                case "Series & tv": Category = TorrentCategory.Tv; break;
                default: Category = TorrentCategory.All; break;
            }

            var age = Regex.Match(line, "(?<=(date-row\">))[0-9]+ [a-zA-Z]+(?=(</td>))").Value;
            var ageSpl = age.Split(' ');
            var ageL = uint.Parse(ageSpl[0]);
            switch (ageSpl[1])
            {
                case "year":
                case "years":   ageL *= 12; goto case "months";
                case "month":
                case "months":  ageL = (uint)(ageL * 30.4); goto case "days";
                case "day":
                case "days":    ageL *= 86400; break;
            }
            Age = ageL;

            var size = Regex.Match(line, "(?<=(size-row\">))[0-9.]+ ([kKgGmMtT])?B(?=(</td>))").Value;
            var sizeSpl = size.Split(' ');
            var sized = double.Parse(sizeSpl[0]);
            switch (sizeSpl[1])
            {
                case "TB": sized *= 1024; goto case "GB";
                case "GB": sized *= 1024; goto case "MB";
                case "MB": sized *= 1024; goto case "KB";
                case "KB": sized *= 1024; break;
            }
            Size = (ulong) sized;
            Seeders = uint.Parse(Regex.Match(line, "(?<=(seeders-row [a-z]{2}\">))[0-9]+(?=(</td>))").Value);
            Leechers = uint.Parse(Regex.Match(line, "(?<=(leechers-row [a-z]{2}\">))[0-9]+(?=(</td>))").Value);
        }
    }
}
