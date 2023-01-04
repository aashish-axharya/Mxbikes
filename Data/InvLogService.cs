using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mxbikes.Data.Models;

namespace Mxbikes.Data
{
    internal class InvLogService
    {
        /*Get items List*/
        public static List<InvLog> GetAll()
        {
            string invLogFilePath = Utils.GetInvLogFilePath();
            if (!File.Exists(invLogFilePath))
            {
                return new List<InvLog>();
            }
            var json = File.ReadAllText(invLogFilePath);
            return JsonSerializer.Deserialize<List<InvLog>>(json);
        }
        /*Save items to json file*/
        private static void SaveLog(List<InvLog> items)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string invLogFilePath = Utils.GetInvLogFilePath();
            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }
            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(invLogFilePath, json);
        }
        public static List<InvLog> Create(Guid invLogId, Guid userId, int quantity)
        {
            List<InvLog> invLogs = GetAll();
            invLogs.Add(new InvLog
            {
                ID = invLogId,
                Staffid = userId,
                Quantity = quantity,
                ApprovedStatus = false,
                DateTaken = DateTime.Now
            });
            SaveLog(invLogs);
            return invLogs;
        }
        public static List<InvLog> Delete(Guid invLogId)
        {
            List<InvLog> invLogs = GetAll();
            InvLog log = invLogs.FirstOrDefault(x => x.Staffid == invLogId);
            invLogs.Remove(log);
            SaveLog(invLogs);
            return invLogs;
        }
        public static List<InvLog> Update(Guid logId, Guid staffId, Guid approvedBy, Guid itemId, bool status, int quantity)
        {
            List<InvLog> invLogs = GetAll();
            InvLog invLogUpdate = invLogs.FirstOrDefault(x => x.ID == logId);
            if (invLogUpdate == null)
            {
                throw new Exception("Error in processing request");
            }
            invLogUpdate.Staffid = staffId;
            invLogUpdate.ApprovedBy = approvedBy;
            invLogUpdate.ItemId = itemId;
            invLogUpdate.ApprovedStatus = status;
            invLogUpdate.Quantity = quantity;
            invLogUpdate.DateTaken = DateTime.Now;
            SaveLog(invLogs);
            return invLogs;
        }
    }
}
