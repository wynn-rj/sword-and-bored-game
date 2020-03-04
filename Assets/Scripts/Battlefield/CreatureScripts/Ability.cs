using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.Battlefield.CreaturScripts {
    public class Ability : AbstractAbility
    {
        public enum ShapeEnum { Point = 0, Sphere = 1, Cross = 2, Line = 3 }

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
            aoeShape = abilityStats.Shape;
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
        public int aoeShape;
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
            if (range == 0)
            {
                switch (aoeShape)
                {
                    case 1:
                        Vector3 dir = hit.point - user.transform.position;
                        dir = new Vector3(dir.x, 0, dir.z);
                        dir.Normalize();
                        Vector3 point = user.transform.position + dir * ((float)length / 2f);
                        Collider[] enemies = Physics.OverlapSphere(point, ((float)length) / 2f);
                        foreach (Collider enemy in enemies)
                        {
                            if (enemy.GetComponent<UniqueCreature>())
                            {
                                UniqueCreature enemyCreature = enemy.GetComponent<UniqueCreature>();
                                // Accuracy Check
                                //Damage Equation
                                if (AccuracyCheck(enemyCreature))
                                {
                                    enemyCreature.Damage(DamageEquation(enemyCreature));
                                } else
                                {
                                    enemyCreature.Miss();
                                }
                        
                            }
                        }
                        

                        break;
                    case 2:

                        break;
                    case 3:
                        Vector3 dir3 = hit.point - user.transform.position;
                        dir = new Vector3(dir3.x, 0, dir3.z);
                        dir.Normalize();
                        Vector3 point3 = user.transform.position + dir * ((float)length / 2f);
                        Collider[] enemies3 = Physics.OverlapBox(shape.transform.position, shape.transform.localScale / 2, shape.transform.rotation);
                        foreach (Collider enemy in enemies3)
                        {
                            if (enemy.GetComponent<UniqueCreature>() && enemy.GetComponent<UniqueCreature>() != user)
                            {
                                UniqueCreature enemyCreature = enemy.GetComponent<UniqueCreature>();
                                // Accuracy Check
                                //Damage Equation
                                if (AccuracyCheck(enemyCreature))
                                {
                                    enemyCreature.Damage(DamageEquation(enemyCreature));
                                }
                                else
                                {
                                    enemyCreature.Miss();
                                }

                            }
                        }
                        break;
                }
            } else
            {
                switch (aoeShape)
                {
                    case 1:
                        if (Vector3.Distance(user.transform.position, hit.point) <= range)
                        {
                            Collider[] enemies = Physics.OverlapSphere(hit.point, ((float)length) / 2f);
                            foreach (Collider enemy in enemies)
                            {
                                if (enemy.GetComponent<UniqueCreature>())
                                {
                                    UniqueCreature enemyCreature = enemy.GetComponent<UniqueCreature>();
                                    // Accuracy Check
                                    //Damage Equation
                                    if (AccuracyCheck(enemyCreature))
                                    {
                                        enemyCreature.Damage(DamageEquation(enemyCreature));
                                    }
                                    else
                                    {
                                        enemyCreature.Miss();
                                    }

                                }
                            }
                        }

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
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
                    // Accuracy Check
                    //Run damage equation
                    if (AccuracyCheck(enemy))
                    {
                        enemy.Damage(DamageEquation(enemy));
                    }
                    else
                    {
                        enemy.GetComponent<UniqueCreature>().Miss();
                    }
                    //Debug.Log("Hit");
                }
                else
                {
                    //Debug.Log("Miss");
                }
            }
        }

        public void EnemyAttackNonAOE(UniqueCreature unitHit)
        {
            // Accuracy Check
            //Run damage equation
            if (AccuracyCheck(unitHit))
            {
                unitHit.Damage(DamageEquation(unitHit));
            }
            else
            {
                unitHit.GetComponent<UniqueCreature>().Miss();
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
                    shapeRend.enabled = true;
                    shape.transform.localScale = new Vector3(1, 1, length);
                    Vector3 dir = hit.point - user.transform.position;
                    dir = new Vector3(dir.x, 0, dir.z);
                    dir.Normalize();
                    shape.transform.position = user.transform.position + dir * ((float)length / 2f);
                    shape.transform.LookAt(user.transform.position);
                    Collider[] enemies3 = Physics.OverlapBox(shape.transform.position, shape.transform.localScale / 2, shape.transform.rotation);
                    foreach (Collider enemy in enemies3)
                    {
                        if (enemy.GetComponent<UniqueCreature>() && enemy.GetComponent<UniqueCreature>() != user)
                        {
                            UniqueCreature enemyCreature = enemy.GetComponent<UniqueCreature>();
                            // Accuracy Check
                            //Damage Equation
                            enemyCreature.hightlight();

                        }
                    }
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
            return finalDamage;
        }

        public bool AccuracyCheck(UniqueCreature enemy)
        {
            //int extraMods = (user.stats.accuracy - enemy.stats.evasion) * 3;
            return accuraccy > Random.Range(0,100);
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
