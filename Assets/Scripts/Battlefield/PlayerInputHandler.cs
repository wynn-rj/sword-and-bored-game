using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.Battlefield.CameraUtilities;

namespace SwordAndBored.Battlefield
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Header("Tilemaps")]
        public GameObject tileIndictor;
        public LayerMask tileMapLayerMask;
        [Header("Player Selection")]
        public LayerMask selectingCreaturesLayerMask;

        public UniqueCreature activePlayer;

        public TurnManager turnManager;
        int abilityInUse = 0;

        bool movingState = true;
        Renderer indicatorRend;
        Camera cam;


        public Vector2 startCoordinates;

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
        }


        void Update()
        {
            activePlayer = turnManager.activePlayer;
            activePlayer.Glow(1);

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

        private void EndTurn()
        {
            activePlayer.Glow(2);
            turnManager.nextTurn();
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
                    activePlayer.Move(currentTile);
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
                    activePlayer.abilityContainer.UseAbility(0, target);
                    movingState = true;
                    indicatorRend.enabled = true;

                }

            }

        }
    }
}
