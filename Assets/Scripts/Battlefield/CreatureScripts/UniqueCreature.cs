﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using SwordAndBored.Battlefield.MovementSystemScripts;
using UnityEngine.UI;
using SwordAndBored.UI.Battlefield;
using UnityEngine.Audio;
using SwordAndBored.GameData.Units;

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
        [HideInInspector]
        public IUnit myUnit;

        [Header("Creature Info")]
        public string creatureName;
        public int maxHealth;
        public int health;
        public Animator anim;

        public AudioClip deathSound;
        public AudioMixerGroup music, soundEffects;
        private AudioSource audioSource;

        [HideInInspector]
        public bool action = true;

        public int maxMovement;
        public int movementLeft;


        public GameObject skeleton;


        [Header("Effects Objects")]
        public GameObject burningEffect;
        public GameObject frozenEffect;
        public GameObject stunnedEffect;
        public GameObject bleedingEffect;


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
            audioSource = GetComponent<BrainManager>().manager.AudioSource;

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

        public void hightlight(Color color)
        {
            b = a + Time.time;
            outline.OutlineColor = color;
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

            SetStatusEffectsObject();
        }

        public void SetStatusEffectsObject()
        {
            if (stats.IsBleeding)
            {
                bleedingEffect.SetActive(true);

                stunnedEffect.SetActive(false);
                frozenEffect.SetActive(false);
                burningEffect.SetActive(false);
            }
            if (stats.IsFrozen)
            {
                frozenEffect.SetActive(true);

                stunnedEffect.SetActive(false);
                burningEffect.SetActive(false);
                bleedingEffect.SetActive(false);
            }
            if (stats.IsBurning)
            {
                burningEffect.SetActive(true);

                stunnedEffect.SetActive(false);
                frozenEffect.SetActive(false);
                bleedingEffect.SetActive(false);
            }
            if (stats.IsStunned)
            {
                stunnedEffect.SetActive(true);

                burningEffect.SetActive(false);
                frozenEffect.SetActive(false);
                bleedingEffect.SetActive(false);
            }
            if (!stats.HasStatus())
            {
                stunnedEffect.SetActive(false);
                burningEffect.SetActive(false);
                frozenEffect.SetActive(false);
                bleedingEffect.SetActive(false);
            }
        }

        public void Damage(int damage)
        {
            Color newColor;
            if (damage > 0)
            {
                newColor = Color.red;
                damageMessage.text = "-" + damage;
            } else
            {
                newColor = Color.yellow;
                damageMessage.text = "+" + -damage;
            }
            health -= damage;
            newColor.a = 255;
            
            damageMessage.color = newColor;
            if (health <= 0)
            {
                // Destroy(transform.gameObject);
                Death();
            }
            if (health > maxHealth)
            {
                health = maxHealth;
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
            burningEffect.SetActive(false);
            frozenEffect.SetActive(false);
            bleedingEffect.SetActive(false);
            stunnedEffect.SetActive(false);
            if (myUnit != null)
            {
                myUnit.IsDead = true;
            }
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                audioSource.outputAudioMixerGroup = soundEffects;
                audioSource.PlayOneShot(deathSound, 5);
                audioSource.outputAudioMixerGroup = music;
                audioSource.Play();
            }
            else
            {
                audioSource.outputAudioMixerGroup = soundEffects;
                audioSource.PlayOneShot(deathSound, 5);
                audioSource.outputAudioMixerGroup = music;
            }

            if (isEnemy)
            {
                brain.manager.RemoveUnitFromEnemyList(gameObject);
            } else
            {
                brain.manager.RemoveUnitFromPlayerList(gameObject);
            }
            brain.manager.RemoveUnitFromList(gameObject);
            if (brain.isMyTurn)
            {
                brain.isMyTurn = false;
                brain.manager.nextTurn();
            }
            GetComponent<Collider>().enabled = false;
            skeleton.transform.parent = null;
            skeleton.GetComponent<Animator>().SetTrigger("Death");
            Destroy(currentCamera);
            Destroy(gameObject);
        }

    }

}
