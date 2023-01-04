using Mxbikes.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mxbikes.Data
{
    internal class InvAddService
    {
        /*Get items List*/
        public static List<InvAdd> GetAll()
        {
            string itemAddFilePath = Utils.GetItemsAddFilePath();
            if (!File.Exists(itemAddFilePath))
            {
                return new List<InvAdd>();
            }
            var json = File.ReadAllText(itemAddFilePath);
            return JsonSerializer.Deserialize<List<InvAdd>>(json);
        }
        /*Save items to json file*/
        private static void SaveItems(List<InvAdd> items)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string itemAddFilePath = Utils.GetItemsAddFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }
            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(itemAddFilePath, json);
        }
        public static List<InvAdd> Create(Guid itemId, Guid userId, int quantity)
        {
            List<InvAdd> items = GetAll();
            if (quantity <= 0)
            {
                throw new Exception("Invalid Quantity Found");
            }
            items.Add(new InvAdd
            {
                ItemId = itemId,
                QuantityToAdd = quantity,
                AddedBy = userId,
                DateAdded = DateTime.Now
            });
            SaveItems(items);
            return items;
        }
    }
}
