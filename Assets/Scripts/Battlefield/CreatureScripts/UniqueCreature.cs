using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SwordAndBored.Battlefield.CreaturScripts {
    public class UniqueCreature : CreatureBase
    {
        [Header("Material Info")]
        public Material[] mat;
        [HideInInspector]
        public Renderer currentMat;
        float a = .05f;
        float b;
        int highlightColor;
        [HideInInspector]
        public UnitAbilitiesContainer abilityContainer;
        [HideInInspector]
        public UnitStats stats;
        [Header("Virtual Camera Info")]
        public CinemachineVirtualCamera currentCamera;
        public Animator animator;
        public Transform target;
        

        void Start()
        {
            health = maxHealth;

            currentMat = GetComponent<Renderer>();
            abilityContainer = GetComponent<UnitAbilitiesContainer>();
            stats = GetComponent<UnitStats>();
        }


        /// <summary>
        /// This method chnages the highlighting of a unit based on an integr.  3 = no highlight, 1 = blue, 2 = red.
        /// When highlighting a unit dont forget to set their material back to normal using glow(3). 
        /// This function might be removed in the future in favor of using a free store asset. 
        /// </summary>
        public void Glow(int glow)
        {
            highlightColor = glow;
            if (glow == 1)
            {
                currentMat.material = mat[1];
            } else if (glow == 2)
            {
                currentMat.material = mat[0];
            } else if (glow == 3) 
            {
                b = a + Time.time;
                currentMat.material = mat[2];
            }
        }

        private void Update()
        {
            if (health <= 0)
            {
                Destroy(target.gameObject);
                Destroy(currentCamera.gameObject);
                Destroy(transform.gameObject);
            }


            if (highlightColor == 3 && Time.time > b)
            {
                Glow(2);
            }

            animator.SetFloat("Speed", (agent.velocity.magnitude / 3.5f));
        }

        /// <summary>
        /// This method is used to move a unit to a tile        
        /// </summary>
        public void Move(Tile goTile)
        {
                SetTile(goTile);
                MoveTo(goTile.GetPos());
        }
    }

}
