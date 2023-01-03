using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mxbikes.Data.Models;

namespace Mxbikes.Data
{
    public static class InventoryService {
        private static void SaveItems(Guid userId, List<Items> items) {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appItemFilePath = Utils.GetItemFilePath(userId);

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(appItemFilePath, json);
        }
		public static List<Items> GetAll(Guid userId)
		{
			string inventoryFilePath = Utils.GetItemFilePath(userId);
			if (!File.Exists(inventoryFilePath))
			{
				return new List<Items>();
			}

			var json = File.ReadAllText(inventoryFilePath);

			return JsonSerializer.Deserialize<List<Items>>(json);
		}
		public static List<Items> Create(Guid userId, string Name, int Quantity)
		{

			List<Items> items = GetAll(userId);
			items.Add(new Items
			{
				ItemName = Name,
				Quantity = Quantity,

			});
			SaveItems(userId, items);
			return items;
		}
	}
}
