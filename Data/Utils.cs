using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Maui.HotReload;
using Mxbikes.Data.Models;
using Microsoft.AspNetCore.Hosting;

namespace Mxbikes.Data
{
    public static class Utils
    {
        private const char _segmentDelimiter = ':';
        public static string HashSecret(string input)
        {
            var saltSize = 8;
            var iterations = 100_000;
            var keySize = 16;
            HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algorithm, keySize);
            return string.Join(
                _segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                iterations,
                algorithm
            );
        }

        public static bool VerifyHash(string input, string hashString)
        {
            string[] segments = hashString.Split(_segmentDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );
            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }

        public static string GetAppDirectoryPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Coursework1"
            );
        }

        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "users.json");
        }
        public static string GetItemFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(),"items.json");
        }
		public static string GetItemsAddFilePath()
		{
			return Path.Combine(GetAppDirectoryPath(), "itemsAdded.json");
		}
		public static string GetInvLogFilePath()
		{
			return Path.Combine(GetAppDirectoryPath(), "inventoryLog.json");
		}
	}
}
