using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SwordAndBored.BattleMechanism
{
    public class Interact : MonoBehaviour
    {
        public GameObject tileIndictor;
        public LayerMask tileMapLayerMask;
        public LayerMask selectingCreaturesLayerMask;

        public UniqueCreature activePlayer;

        public TurnManager turnManager;
        int abilityInUse = 0;

        bool moving = true;
        Renderer indicatorRend;


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
        }


        void Update()
        {
            activePlayer = turnManager.activePlayer;
            activePlayer.Glow(1);
            if (moving)
            {
                movePlayer();
            }
            else
            {
                useAbility(abilityInUse);
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

        void movePlayer()
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    abilityInUse = i + 1;
                    Debug.Log(i + 1);
                    moving = false;
                    indicatorRend.enabled = false;
                }
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, tileMapLayerMask))
            {
                Tile currentTile = hit.collider.GetComponent<Tile>();
                Vector3 spot = currentTile.GetPos();
                tileIndictor.transform.position = spot;
                if (Input.GetButtonDown("Fire1"))
                {
                    activePlayer.MoveTo(spot);
                    activePlayer.SetTile(currentTile);
                }
            }
        }

        void useAbility(int abilityNum)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
                    activePlayer.UseAbility(0, target);
                    moving = true;
                    indicatorRend.enabled = true;

                }

            }

        }
    }
}
