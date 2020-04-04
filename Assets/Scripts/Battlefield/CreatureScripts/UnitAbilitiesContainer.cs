using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SwordAndBored.Battlefield.CreaturScripts
{
    public class UnitAbilitiesContainer : MonoBehaviour
    {
        [Header("Ability Info")]
        public List<Ability> abilities = new List<Ability>();
        UniqueCreature unit;
        private AudioSource audioSource;
        public AudioClip fireballSound, magicSound;
        public AudioMixerGroup music, soundEffects;

        [Header("Weapon")]
        public GameObject sword;
        public GameObject book;
        public GameObject bow;

        void Start()
        {
            audioSource = GetComponent<BrainManager>().manager.AudioSource;
            unit = GetComponent<UniqueCreature>();
            foreach (Ability ability in abilities)
            {
                if (ability.aoe)
                {
                    switch (ability.aoeShape)
                    {
                        case 1:
                            GameObject sphereFile = Resources.Load<GameObject>("AOE/AoeSphere");
                            GameObject sphere = Instantiate(sphereFile, Vector3.zero, Quaternion.identity);
                            sphere.GetComponent<Renderer>().enabled = false;
                            ability.Initialize(this, transform.gameObject, sphere);
                            break;
                        case 2:

                            break;
                        case 3:
                            GameObject cubeFile = Resources.Load<GameObject>("AOE/AoeCube");
                            GameObject cube = Instantiate(cubeFile, Vector3.zero, Quaternion.identity);
                            cube.GetComponent<Renderer>().enabled = false;
                            ability.Initialize(this, transform.gameObject, cube);
                            break;
                        default:
                            Debug.Log("Something broke in unit abilities container");
                            break;
                    }
                }
                else
                {
                    ability.Initialize(this, transform.gameObject);
                }
            }
        }

        public bool IsAoe(int i)
        {
            return abilities[i].aoe;
        }

        public void UseAbility(int i, RaycastHit target)
        {
            transform.LookAt(target.transform, Vector3.up);
            abilities[i].TriggerAbility(target);
            unit.action = false;
            
            if(abilities[i].animation == "Sword")
            {
                sword.SetActive(true);
                book.SetActive(false);
                //bow.SetActive(false);
                unit.animator.SetTrigger("SwordAttack");
            } else if (abilities[i].animation == "Magic")
            {
                sword.SetActive(false);
                book.SetActive(true);
                //bow.SetActive(false);
                unit.animator.SetTrigger("MagicAttack");
            } else if (abilities[i].animation == "Bow")
            {
                sword.SetActive(false);
                book.SetActive(false);
                //bow.SetActive(true);
                unit.animator.SetTrigger("BowAttack");
            }
            
            // Sound Effects
            if (abilities[i].name == "Fire Ball")
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Pause();
                    audioSource.outputAudioMixerGroup = soundEffects;
                    audioSource.PlayOneShot(fireballSound, 5);
                    audioSource.outputAudioMixerGroup = music;
                    audioSource.Play();
                }
                else
                {
                    audioSource.outputAudioMixerGroup = soundEffects;
                    audioSource.PlayOneShot(fireballSound, 5);
                    audioSource.outputAudioMixerGroup = music;
                }
            }
            else if (!abilities[i].isPhysical)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Pause();
                    audioSource.outputAudioMixerGroup = soundEffects;
                    audioSource.PlayOneShot(magicSound, 5);
                    audioSource.outputAudioMixerGroup = music;
                    audioSource.Play();
                }
                else
                {
                    audioSource.outputAudioMixerGroup = soundEffects;
                    audioSource.PlayOneShot(magicSound, 5);
                    audioSource.outputAudioMixerGroup = music;
                }
            }
        }

        public void HighlightTarget(int i, RaycastHit hit)
        {
            abilities[i].ShowTarget(hit);
        }

        public void StopAoe()
        {
            for (int i = 0; i < abilities.Count; i++)
            {
                abilities[i].StopShowAoe();
            }
        }

        public void ShowAbilityAnimation()
        {
            unit.animator.SetTrigger("Attack");
        }
    }

}
