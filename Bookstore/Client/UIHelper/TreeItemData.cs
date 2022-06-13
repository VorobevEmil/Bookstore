using Bookstore.Shared.DbModels;

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

        public static void SetTreeItems(List<CatalogModel> catalogs, HashSet<TreeItemData> treeItems)
        {
            foreach (var catalog in catalogs)
            {
                treeItems.Add(new TreeItemData(catalog.Id, catalog.Title));
            }
        }
    }
}
