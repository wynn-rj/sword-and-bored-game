using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.GameData.Database.Tables
{
    public class DescriptorDatabase : MonoBehaviour
    {
        public int ID { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }
    }
}
