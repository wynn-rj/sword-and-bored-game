using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SwordAndBored.Battlefield
{
    public class DisplayPath
    {

        public void Display(LineRenderer lr, List<Tile> path)
        {
            lr.positionCount = path.Count;
            for (int i = 0; i < path.Count; i++)
            {
                lr.SetPosition(i, path[i].GetCenterOfTile() + Vector3.up * .1f);
            }
        }

    }
}
