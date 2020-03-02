using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Strategy.TimeSystem.Subscribers;
using SwordAndBored.Strategy.TimeSystem.TimeManager;

namespace SwordAndBored.Strategy.GameResources
{
    public class City : MainThreadPostTimeStepSubscriber
    {
        public ResourceManager resourceManager { get; set; }
        public bool underPlayerControl = true;

        void Awake()
        {
            underPlayerControl = true;
        }
        protected override void MainThreadPostTimeStepUpdate()
        {
            Debug.Log("City Update");
            Debug.Log("GoldAmount: " + resourceManager.GoldAmount);
            resourceManager.GoldAmount += 1;
        }
    }
}
