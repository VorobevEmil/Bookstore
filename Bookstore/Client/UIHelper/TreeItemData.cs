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
            foreach (var catalog in catalogs.Where(t => t.Catalogs?.Select(x => x.CatalogModelId).Contains(t.Id) ?? true))
            {
                if (catalog.Catalogs == null)
                {
                    treeItems.Add(new TreeItemData(catalog.Id, catalog.Title));
                }
                else
                {
                    var item = new TreeItemData(catalog.Id, catalog.Title)
                    {
                        TreeItems = new HashSet<TreeItemData>()
                    };
                    treeItems.Add(item);
                    SetTreeItems(catalog.Catalogs, item.TreeItems);
                }
            }
        }
    }
}
