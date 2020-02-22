using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.Battlefield.CameraUtilities;
using Cinemachine;

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
            indicatorRend = tileIndictor.GetComponentInChildren<Renderer>();
            cam = Camera.main;
            creature = GetComponent<UniqueCreature>();
            creature.agent.destination = startCoordinates;
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
            return creature.creatureName;
        }

        public bool HasActionLeft()
        {
            return creature.action;
        }

        public CinemachineVirtualCamera GetCam()
        {
            return creature.currentCamera;
        }

    }
}
