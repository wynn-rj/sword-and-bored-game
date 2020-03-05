using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using SwordAndBored.Battlefield.MovementSystemScripts;
using UnityEngine.UI;
using SwordAndBored.UI.Battlefield;

namespace SwordAndBored.Battlefield.CreaturScripts {
    public class UniqueCreature : MonoBehaviour
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
        Outline outline;
        [HideInInspector]
        BrainManager brain;
        public bool isEnemy;
        MovementSystem ms;
        Text damageMessage;

        [Header("Creature Info")]
        public string creatureName;
        public int maxHealth;
        public int health;
        public Animator anim;

        [HideInInspector]
        public bool action = true;

        public int maxMovement;
        public int movementLeft;


        void Start()
        {
            //health = maxHealth;
            brain = GetComponent<BrainManager>();
            currentMat = GetComponent<Renderer>();
            abilityContainer = GetComponent<UnitAbilitiesContainer>();
            stats = GetComponent<UnitStats>();
            outline = GetComponent<Outline>();
            ms = GetComponent<MovementSystem>();
            damageMessage = GetComponent<HealthBar>().popup;

            health = maxHealth;
            movementLeft = stats.movement;
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
                outline.OutlineColor = Color.blue;
                outline.enabled = true;
            } else if (glow == 2)
            {
                outline.enabled = false;
            } else if (glow == 3) 
            {
                b = a + Time.time;
                outline.OutlineColor = Color.red;
                outline.enabled = true;
            }
        }

        public void hightlight()
        {
            b = a + Time.time;
            outline.OutlineColor = Color.red;
            outline.enabled = true;
        }
        

        private void Update()
        {
            if (!brain.isMyTurn && Time.time > b)
            {
                outline.enabled = false;
            }

            stats.health = health;
        
            animator.SetFloat("Speed", (ms.agent.velocity.magnitude / 3.5f));

            if (health <= 0)
            {
                Death();
            }
        }

        public void Damage(int damage)
        {
            health -= damage;
            Color newColor = Color.red;
            newColor.a = 255;
            damageMessage.text = "-" + damage;
            damageMessage.color = newColor;
            if (health <= 0)
            {
                // Destroy(transform.gameObject);
                Death();
            }
        }

        public void Miss()
        {
            Color newColor = Color.blue;
            newColor.a = 255;
            damageMessage.text = "Missed!";
            damageMessage.color = newColor;
        }

        public void Death()
        {
            if (isEnemy)
            {
                brain.manager.RemoveUnitFromEnemyList(gameObject);
            } else
            {
                brain.manager.RemoveUnitFromPlayerList(gameObject);
            }
            brain.manager.RemoveUnitFromList(gameObject);
            Destroy(gameObject);
        }

    }

}
