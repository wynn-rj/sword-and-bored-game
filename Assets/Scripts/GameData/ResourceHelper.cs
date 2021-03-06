﻿using SwordAndBored.GameData.Database;
using UnityEngine;

namespace SwordAndBored.GameData
{
    public static class ResourceHelper
    {
        public static int GetGoldAmount()
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.ExecuteQuery($"SELECT Gold From Resources WHERE ID = 1");
            reader.NextRow();
            int goldAmount = reader.GetIntFromCol("Gold");
            conn.CloseConnection();
            reader.CloseReader();

            return goldAmount;
        }

        public static void SetGoldAmount(int newAmount)
        {
            DatabaseConnection conn = new DatabaseConnection();
            conn.ExecuteNonQuery($"UPDATE Resources SET Gold = {newAmount};");
            conn.CloseConnection();
        }

        public static int GetTurnNumber()
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.ExecuteQuery($"SELECT Turn_Number From Resources WHERE ID = 1");
            reader.NextRow();
            int turnNum = reader.GetIntFromCol("Turn_Number");
            conn.CloseConnection();
            reader.CloseReader();

            return turnNum;
        }

        public static void SetTurnNumber(int turnNum)
        {
            DatabaseConnection conn = new DatabaseConnection();
            conn.ExecuteNonQuery($"UPDATE Resources SET Turn_Number = {turnNum};");
            conn.CloseConnection();
        }

        public static string GetCreepTiles()
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.ExecuteQuery($"SELECT Creep_Tiles From Resources WHERE ID = 1");
            reader.NextRow();
            string creepTiles = reader.GetStringFromCol("Creep_Tiles");
            conn.CloseConnection();
            reader.CloseReader();

            return creepTiles;
        }

        public static void SetCreepTiles(string CreepTiles)
        {
            DatabaseConnection conn = new DatabaseConnection();
            conn.ExecuteNonQuery($"UPDATE Resources SET Creep_Tiles = {DatabaseHelper.GetNullOrStringFromString(CreepTiles)};");
            conn.CloseConnection();
        }

        public static string GetRandomNameFromDatabase()
        {
            DatabaseConnection conn = new DatabaseConnection();
            int rInt = Random.Range(1, 30);
            DatabaseReader reader = conn.ExecuteQuery($"SELECT Name From Random_Names WHERE ID = {rInt}");
            reader.NextRow();
            string randName = reader.GetStringFromCol("Name");
            conn.CloseConnection();
            reader.CloseReader();

            return randName;
        }
    }
}
