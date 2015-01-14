namespace TorrentSearch
{
    public interface ITorrentSearcher
    {
        Torrent[] Search(string query, TorrentCategory category, TorrentSortOrder sortOrder = TorrentSortOrder.Title,
            TorrentSortOptions sortOptions = TorrentSortOptions.Ascending);
    }
}