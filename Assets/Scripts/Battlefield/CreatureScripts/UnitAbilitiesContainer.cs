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
        [HideInInspector]
        public GameObject sphereFile;

        void Start()
        {
            sphereFile = Resources.Load<GameObject>("AOE/AoeSphere");
            unit = GetComponent<UniqueCreature>();
            foreach (Ability ability in abilities)
            {
                if (ability.aoe)
                {
                    GameObject sphere = Instantiate(sphereFile, Vector3.zero, Quaternion.identity);
                    sphere.GetComponent<Renderer>().enabled = false;
                    ability.Initialize(this, transform.gameObject, sphere);
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
            Debug.Log(abilities[i].AttackName);
            unit.action = false;
        }

        public void HighlightTarget(int i, RaycastHit hit)
        {
            abilities[i].ShowTarget(hit);
        }
    }

}
