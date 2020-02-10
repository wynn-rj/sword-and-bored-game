﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.Movement
{
    public class EnemyMove : MonoBehaviour
    {
        public GameObject player;

        void Update()
        {
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * 0.05f);
        }
    }
}
