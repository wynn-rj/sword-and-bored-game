using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.Battlefield.CameraUtilities;
using Cinemachine;
using SwordAndBored.Battlefield.MovementSystemScripts;

namespace SwordAndBored.Battlefield
{

    public class BrainManager : MonoBehaviour
    {
        [Header("Tilemaps")]
        [HideInInspector]
        public GameObject tileIndictor;
        public LayerMask tileMapLayerMask;
        [Header("Player Selection")]
        public LayerMask selectingCreaturesLayerMask;
        public LayerMask selectingGroundLayerMask;

        public TurnManager manager;

        bool movingState = true;
        [HideInInspector]
        public Renderer indicatorRend;
        [HideInInspector]
        public Camera cam;
        [HideInInspector]
        public Vector2 startCoordinates;
        [HideInInspector]
        public UniqueCreature creature;

        [HideInInspector]
        public bool isMyTurn = false;
        Animator anim;

        [HideInInspector]
        public Outline outline;

        MovementSystem ms;

        private KeyCode[] keyCodes = {
             KeyCode.Alpha1,
             KeyCode.Alpha2,
             KeyCode.Alpha3,
             KeyCode.Alpha4,
             KeyCode.Alpha5,
             KeyCode.Alpha6,
             KeyCode.Alpha7,
             KeyCode.Alpha8,
             KeyCode.Alpha9,
         };

        void Start()
        {
            ms = GetComponent<MovementSystem>();
            indicatorRend = tileIndictor.GetComponentInChildren<Renderer>();
            cam = Camera.main;
            creature = GetComponent<UniqueCreature>();
            ms.agent.destination = startCoordinates;
            anim = GetComponent<Animator>();
            outline = GetComponent<Outline>();
        }

        private void Update()
        {
            anim.SetBool("IsMyTurn", isMyTurn);
        }

        public bool GetTurnEnd()
        {
            return isMyTurn;
        }

        public string GetName()
        {
            if (creature.creatureName != null)
            {
                return creature.creatureName;

            } else
            {
                return "<Name Still Loading>";
            }
        }

        public bool HasActionLeft()
        {
            return creature.action;
        }

        public int MovementLeft()
        {
            return creature.movementLeft;
        }

        public CinemachineVirtualCamera GetCam()
        {
            return creature.currentCamera;
        }

    }
}
