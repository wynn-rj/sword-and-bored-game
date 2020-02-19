using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield;
using Cinemachine;
using SwordAndBored.Battlefield.CameraUtilities;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.Database.Tables;


namespace SwordAndBored.Battlefield
{
    public class EnemyManager : MonoBehaviour
    {
        public GameObject playerPrefab;
       public CameraManager camManager;
        public TurnManager turnManager;
        public Transform unitHolder;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
