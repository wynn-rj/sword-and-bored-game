using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.Battlefield.CameraUtilities;
using Cinemachine;
using UnityEngine.EventSystems;

namespace SwordAndBored.Battlefield
{
    public class TurnBrain : AbstractTurnBrain
    {
        [Header("Tilemaps")]
        [HideInInspector]
        public GameObject tileIndictor;
        public LayerMask tileMapLayerMask;
        [Header("Player Selection")]
        public LayerMask selectingCreaturesLayerMask;

        int abilityInUse = 0;

        bool movingState = true;
        Renderer indicatorRend;
        Camera cam;

        [HideInInspector]
        public Vector2 startCoordinates;
        UniqueCreature creature;

        [HideInInspector]
        public bool isMyTurn = false;

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
        }


        public void Update()
        {
            if (isMyTurn)
            {
                creature.Glow(1);

                if (movingState)
                {
                    movementState();
                }
                else
                {
                    useAbilityState(abilityInUse);
                }

                if (Input.GetButtonDown("Next"))
                {
                    EndTurn();
                }
            }

        }

        public override void DoTurn()
        {
            isMyTurn = true;
            creature.StartTurn();
        }

        public override string GetName()
        {
            return creature.creatureName;
        }

        private void EndTurn()
        {
            creature.Glow(2);
            movingState = true;
            isMyTurn = false;
        }

        void movementState()
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    abilityInUse = i + 1;
                    movingState = false;
                    indicatorRend.enabled = false;
                }
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, tileMapLayerMask))
            {
                Tile currentTile = hit.collider.GetComponent<Tile>();
                tileIndictor.transform.position = currentTile.GetPos();

                if (currentTile.unitOnTile == null && Input.GetButtonDown("Fire1"))
                {
                    if (EventSystem.current.IsPointerOverGameObject()) return;
                    creature.Move(currentTile);
                }
            }
        }


        void useAbilityState(int abilityNum)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, selectingCreaturesLayerMask))
            {
                GameObject target = hit.collider.gameObject;
                UniqueCreature enem = target.GetComponent<UniqueCreature>();
                if (enem)
                {
                    enem.Glow(3);
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    creature.abilityContainer.UseAbility(0, target);
                    movingState = true;
                    indicatorRend.enabled = true;

                }

            }

        }

        public override bool HasActionLeft()
        {
            return creature.action;
        }

        public override CinemachineVirtualCamera GetCam()
        {
            return creature.currentCamera;
        }

        public override bool GetTurnEnd()
        {
            return isMyTurn;
        }

    }

}
