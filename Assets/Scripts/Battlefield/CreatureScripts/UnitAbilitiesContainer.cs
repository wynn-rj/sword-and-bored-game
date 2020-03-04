using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CreaturScripts
{
    public class UnitAbilitiesContainer : MonoBehaviour
    {
        [Header("Ability Info")]
        public List<Ability> abilities = new List<Ability>();
        UniqueCreature unit;
        private AudioSource audioSource;
        public AudioClip audioClip;

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
                } else
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
            unit.animator.SetTrigger("Attack");
            abilities[i].TriggerAbility(target);
            unit.action = false;
            if (abilities[i].name == "Fire Ball")
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Pause();
                    audioSource.PlayOneShot(audioClip, 5);
                    audioSource.Play();
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
    }

}
