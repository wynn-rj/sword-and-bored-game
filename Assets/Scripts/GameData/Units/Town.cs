﻿using System.Collections.Generic;

namespace SwordAndBored.GameData.Units
{
    public class Town : ITown
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<IUnit> Units { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        public int Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
