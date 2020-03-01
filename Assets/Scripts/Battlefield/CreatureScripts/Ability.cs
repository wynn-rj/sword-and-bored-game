using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.Battlefield.CreaturScripts {
    public class Ability : AbstractAbility
    {
        
        public Ability(IAbility abilityStats)
        {
            name = abilityStats.Name;
            damage = abilityStats.Damage;
            range = abilityStats.Range;
            accuraccy = abilityStats.Accuracy;
            length = abilityStats.Length;
            width = abilityStats.Width;
            aoe = abilityStats.Shape > 0;
            description = abilityStats.Description;
            isPhysical = abilityStats.IsPhysical;
        }

        UnitAbilitiesContainer container;
        public string description;
        public bool aoe;
        public int damage;
        public int range;
        public int accuraccy;
        public int length;
        public int width;
        public bool isPhysical;
        GameObject shape;
        Renderer shapeRend;

        UniqueCreature user;

        public override void Initialize(UnitAbilitiesContainer container, GameObject obj, GameObject shape)
        {
            user = obj.GetComponent<UniqueCreature>();
            this.container = container;
            if (aoe)
            {
                this.shape = shape;
                shapeRend = this.shape.GetComponent<Renderer>();
            }
        }

        public void Initialize(UnitAbilitiesContainer container, GameObject obj)
        {
            Initialize(container, obj, null);
        }

        public override void TriggerAbility(RaycastHit hit)
        {

            if (!aoe)
            {
                pointAttack(hit);
            } else
            {
                aoeAttack(hit);
            }
        }

        void aoeAttack(RaycastHit hit)
        {
            if (Vector3.Distance(user.transform.position, hit.point) <= range)
            {
                Collider[] enemies = Physics.OverlapSphere(hit.point, ((float)length) / 2f);
                foreach (Collider enemy in enemies)
                {
                    if (enemy.GetComponent<UniqueCreature>())
                    {
                        Debug.Log(enemy.GetComponent<UniqueCreature>().creatureName);
                        //Damage Equation
                        enemy.GetComponent<UniqueCreature>().Damage(DamageEquation(enemy.GetComponent<UniqueCreature>()));
                    }
                }
            }
        }

        void pointAttack(RaycastHit hit)
        {
            if (Vector3.Distance(user.transform.position, hit.point) <= range)
            {
                UniqueCreature enemy = getEnemy(hit);
                if (true)
                {
                    //Run damage equation
                    enemy.Damage(DamageEquation(enemy));
                    //Debug.Log("Hit");
                }
                else
                {
                    //Debug.Log("Miss");
                }
            }
        }

        public override void ShowTarget(RaycastHit hit)
        {
            if (!aoe)
            {
                StopShowAoe();
                if (Vector3.Distance(user.transform.position, hit.point) <= range)
                {
                    UniqueCreature enem = getEnemy(hit);
                    if (enem && enem != user)
                    {
                        enem.hightlight();
                    }
                }
            } else {
                if (range == 0)
                {
                    
                } else
                {

                    Collider[] enemies = Physics.OverlapSphere(hit.point, ((float) length) / 2f);
                    foreach (Collider enemy in enemies)
                    {
                        
                        if (enemy.GetComponent<UniqueCreature>() && enemy.GetComponent<UniqueCreature>() != user)
                        {
                            enemy.GetComponent<UniqueCreature>().hightlight();
                        }
                    }

                    if (Vector3.Distance(user.transform.position, hit.point) <= range)
                    {
                        shapeRend.enabled = true;
                        shape.transform.localScale = new Vector3(length, length, width);
                        shape.transform.position = hit.point;
                    }
                }
            }
        }

        public int DamageEquation(UniqueCreature enemy)
        {
            int levelStaticForNow = 50;
            float levelMult = (4 * levelStaticForNow / 5f);
            float topPart;
            if (isPhysical)
            {
                topPart = levelMult * damage * (user.stats.physicalAttack * 1.0f / enemy.stats.physicalDefense);
            } else
            {
                topPart = levelMult * damage * (user.stats.magicAttack  * 1.0f / enemy.stats.magicDefense);
            }
            float preMods = (topPart / 50f) + 2;

            float randomMod = Random.Range(.85f, 1f);
            int finalDamage = Mathf.RoundToInt(preMods * randomMod);
            Debug.Log(finalDamage);
            return finalDamage;
        }

        public void StopShowAoe()
        {
            if (shapeRend)
            {
                shapeRend.enabled = false;
            }
        }

        UniqueCreature getEnemy(RaycastHit hit)
        {
            GameObject target = hit.collider.gameObject;
            UniqueCreature enem = target.GetComponent<UniqueCreature>();
            return enem;
        }
    }
}
