using UnityEngine;
using SwordAndBored.TurnMechanism;

namespace SwordAndBored.BattleMechanism
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }
        public TurnOrderController TurnOrderController;
        public GameObject[] Entities = new GameObject[2];

        public BattleManagerState State;

        public GameObject ActivePlayer;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            State = new BattleManagerInitializeState(this, Entities);
        }

    }
}