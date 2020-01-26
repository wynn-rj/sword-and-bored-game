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

        public CycleTurnManager turnManager;
        int abilityInUse = 0;

        bool moving = true;

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

        void Update()
        {
            activePlayer = turnManager.activePlayer;

            if (moving)
            {
                movePlayer();
            } else
            {
                useAbility(abilityInUse);
            }
            



            if (Input.GetButtonDown("Fire2"))
            {
                turnManager.nextTurn();
            }

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

                
            }
        }
    }
}
