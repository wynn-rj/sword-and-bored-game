﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CreaturScripts { 
    public abstract class AbstractAbilitySO : ScriptableObject
    {
        public enum ActionTypes { Action, BonusAction, Reaction };

        public string AttackName;

        public ActionTypes TypeOfActionRequired;


        public abstract void Initialize(GameObject obj);
        public abstract void TriggerAbility(GameObject target);

        public int Roll(int dice)
        {
            return Random.Range(1, dice);
        }

        public int Roll()
        {
            return Roll(20);
        }

    }
}