namespace Bookstore.Client.UIHelper
{
    public class TreeItemData
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public HashSet<TreeItemData>? TreeItems { get; set; }

        public TreeItemData(int id, string? title)
        {
            Id = id;
            Title = title;
        }
    }
}
