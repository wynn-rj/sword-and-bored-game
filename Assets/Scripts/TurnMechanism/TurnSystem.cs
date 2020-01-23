using UnityEngine;

namespace SwordAndBored.TurnMechanism
{
    public class TurnSystem : MonoBehaviour
    {
        public static TurnSystem Instance { get; private set; }
        public GameObject activeGameObject;
        public delegate void BattleManagerCallback();
        public event BattleManagerCallback BattleManagerCallbackEvent = delegate { };
        public MoveEventHandler EventHandler;
        public TurnSystemState State;

        private void Awake()
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

        private void Start()
        {
            EventHandler = GetComponent<MoveEventHandler>();
        }

        public void SetupSystem(GameObject entity, BattleManagerCallback action)
        {
            activeGameObject = entity;
            BattleManagerCallbackEvent += action;
        }

        public void RunSystem()
        {
            State = new TurnSystemInitializeState(this);
        }

        public void CleanUpSystem(BattleManagerCallback action)
        {
            BattleManagerCallbackEvent -= action;
        }

        public void EndStateMachine()
        {
            BattleManagerCallbackEvent();
        }

    }
}