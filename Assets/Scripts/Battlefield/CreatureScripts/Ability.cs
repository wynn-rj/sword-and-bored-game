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
            animation = abilityStats.Animation;
            bleedChance = abilityStats.StatusConditionsAttack.Bleed_Chance;
            burnChance = abilityStats.StatusConditionsAttack.Fire_Chance;
            freezeChance = abilityStats.StatusConditionsAttack.Freeze_Chance;
            stunChance = abilityStats.StatusConditionsAttack.Stun_Chance;
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
        public string animation;
        public int stunChance;
        public int burnChance;
        public int freezeChance;
        public int bleedChance;
        public ParticleSystem particle;
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

        public override bool TriggerAbility(RaycastHit hit)
        {

            if (!aoe)
            {
                return pointAttack(hit);
            } else
            {
                aoeAttack(hit);
                return true;
            }
        }

        void aoeAttack(RaycastHit hit)
        {
            if (particle)
            {
                particle.transform.position = hit.transform.position;
                particle.Stop();
                particle.Play();
            }
            if (range == 0)
            {
                switch (aoeShape)
                {
                    case 1:
                        Vector3 point = calcPointRangeZero(hit);
                        Collider[] enemies = Physics.OverlapSphere(point, ((float)length) / 2f);
                        DamageList(enemies);
                        break;
                    case 2:
                        //Vector3 point3 = calcPointRangeZero(hit);
                        Collider[] enemies2 = Physics.OverlapBox(shape.transform.position, shape.transform.localScale / 2, shape.transform.rotation);
                        DamageList(enemies2);
                        break;
                    case 3:
                        //Vector3 point2 = calcPointRangeZero(hit);
                        Collider[] enemies3 = Physics.OverlapBox(shape.transform.position, shape.transform.localScale / 2, shape.transform.rotation);
                        DamageList(enemies3);
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
                            DamageList(enemies);
                        }

                        break;
                    case 2:

                        break;
                    case 3:
                        if (Vector3.Distance(user.transform.position, hit.point) <= range)
                        {
                            Collider[] enemies = Physics.OverlapBox(shape.transform.position, shape.transform.localScale / 2, shape.transform.rotation);
                            DamageList(enemies);
                        }
                        break;
                }
            }
        }

        private Vector3 calcPointRangeZero(RaycastHit hit)
        {
            Vector3 dir = hit.point - user.transform.position;
            dir = new Vector3(dir.x, 0, dir.z);
            dir.Normalize();
            Vector3 point = user.transform.position + dir * ((float)length / 2f);
            return point;
        }

        private void DamageList(Collider[] enemies)
        {
            foreach (Collider enemy in enemies)
            {
                if (enemy.GetComponent<UniqueCreature>() && enemy.GetComponent<UniqueCreature>() != user)
                {
                    UniqueCreature enemyCreature = enemy.GetComponent<UniqueCreature>();
                    // Accuracy Check
                    //Damage Equation
                    TryAbilityOn(enemyCreature);

                }
            }
        }

        public void TryAbilityOn(UniqueCreature target)
        {
            if (AccuracyCheck(target))
            {
                target.Damage(DamageEquation(target));
            }
            else
            {
                target.Miss();
            }
        }

        bool pointAttack(RaycastHit hit)
        {
            if (Vector3.Distance(user.transform.position, hit.point) <= range)
            {
                UniqueCreature enemy = getEnemy(hit);
                if (enemy)
                {
                    TryAbilityOn(enemy);
                    return true;
                } else
                {
                    return false;
                }
                //Debug.Log("Hit");
            }
            return false;
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
                    if (aoeShape == 2)
                    {
                        shapeRend.enabled = true;
                        shape.transform.localScale = new Vector3(width, 1, length);
                        //Vector3 dir = user.transform.position;
                        //dir = new Vector3(dir.x, 0, dir.z);
                        //dir.Normalize();
                        shape.transform.position = user.transform.position; //+ dir * ((float)length / 2f);
                        shape.transform.LookAt(user.transform.position);
                        Collider[] enemies3 = Physics.OverlapBox(shape.transform.position, shape.transform.localScale / 2, shape.transform.rotation);
                        highlightList(enemies3);
                    }
                    else
                    {
                        shapeRend.enabled = true;
                        shape.transform.localScale = new Vector3(width, 1, length);
                        Vector3 dir = hit.point - user.transform.position;
                        dir = new Vector3(dir.x, 0, dir.z);
                        dir.Normalize();
                        shape.transform.position = user.transform.position + dir * ((float)length / 2f);
                        shape.transform.LookAt(user.transform.position);
                        Collider[] enemies3 = Physics.OverlapBox(shape.transform.position, shape.transform.localScale / 2, shape.transform.rotation);
                        highlightList(enemies3);
                    }
                } else
                {

                    Collider[] enemies = Physics.OverlapSphere(hit.point, ((float)length) / 2f);
                    highlightList(enemies);

                    if (Vector3.Distance(user.transform.position, hit.point) <= range)
                    {
                        shapeRend.enabled = true;
                        shape.transform.localScale = new Vector3(length, length, width);
                        shape.transform.position = hit.point;
                    }
                }
            }
        }

        private void highlightList(Collider[] enemies)
        {
            foreach (Collider enemy in enemies)
            {

                if (enemy.GetComponent<UniqueCreature>() && enemy.GetComponent<UniqueCreature>() != user)
                {
                    enemy.GetComponent<UniqueCreature>().hightlight();
                } else if (enemy.GetComponent<Tile>())
                {
                    enemy.GetComponent<Tile>().Highlight(Color.red);
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
