using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mxbikes.Data.Models;

namespace Mxbikes.Data
{
    public static class ItemService
    {
        /*Get items List*/
        public static List<Items> GetAll()
        {
            string itemFilePath = Utils.GetItemFilePath();
            if (!File.Exists(itemFilePath))
            {
                return new List<Items>();
            }
            var json = File.ReadAllText(itemFilePath);
            return JsonSerializer.Deserialize<List<Items>>(json);
        }
        /*Save items to json file*/
        private static void SaveItems(List<Items> items)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appItemFilePath = Utils.GetItemFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }
            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(appItemFilePath, json);
        }
        public static List<Items> Create(string name, int quantity)
        {

            List<Items> items = GetAll();
            if (quantity <= 0)
            {
                throw new Exception("Invalid Quantity Found");
            }
            items.Add(new Items
            {
                ItemName = name,
                Quantity = quantity,

            });
            SaveItems(items);
            return items;
        }
        public static List<Items> AddQuantity(Guid itemid, int quantity)
        {
            if (quantity <= 0)
            {
                throw new Exception("Invalid Quantity Found");
            }
            List<Items> items = GetAll();
            Items update = items.FirstOrDefault(x => x.ItemId == itemid);
            update.Quantity += quantity;
            SaveItems(items);
            return items;
        }
        public static List<Items> RemoveQuantity(Guid itemid, int quantity)
        {
            if (quantity <= 0)
            {
                throw new Exception("Invalid Quantity Found");
            }
            List<Items> items = GetAll();
            Items update = items.FirstOrDefault(x => x.ItemId == itemid);
            update.Quantity -= quantity;
            update.LastTaken = DateTime.Now;
            SaveItems(items);
            return items;
        }
    }
}
