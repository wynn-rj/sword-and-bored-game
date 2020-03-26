using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SwordAndBored.GameData.Database
{
    public static class DatabaseManager
    {
        public static void SaveData(string fileName)
        {
            File.Copy(Application.streamingAssetsPath + "/GameSaves/GameData.db", Application.streamingAssetsPath + $"/UserSaves/{fileName}.db", true);
        }

        public static void LoadData(string fileName)
        {
            File.Copy(Application.streamingAssetsPath + $"/UserSaves/{fileName}.db", Application.streamingAssetsPath + "/GameSaves/GameData.db", true);
        }

        public static void LoadFromDefault()
        {
            File.Copy(Application.streamingAssetsPath + $"/GameSaves/Default.db", Application.streamingAssetsPath + "/GameSaves/GameData.db", true);
        }

        public static List<string> GetPreviousSaveNames()
        {
            List<string> result = new List<string>();
            List<string> files = Directory.GetFiles(Application.streamingAssetsPath + "/UserSaves", "*.db").Select(Path.GetFileName).ToList();
            foreach (string file in files)
            {
                result.Add(file.Substring(0, file.Length - 3));
            }
            return result;
        }

    }
}
