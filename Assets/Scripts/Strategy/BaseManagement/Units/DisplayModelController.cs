using System.Collections.Generic;
using UnityEngine;


namespace SwordAndBored.StrategyView.BaseManagement.Units
{
    public class DisplayModelController : MonoBehaviour
    {
        private IDictionary<string, Transform> modelsDictionary;

        private void Awake()
        {
            modelsDictionary = new Dictionary<string, Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform model = transform.GetChild(i);
                modelsDictionary[model.name] = model;

                model.gameObject.SetActive(i == 0);
            }
        }

        public void EnableModel(Transform modelTransform)
        {
            foreach (KeyValuePair<string, Transform> modelPair in modelsDictionary)
            {
                Transform transformToToggle = modelPair.Value;
                bool shouldBeActive = transformToToggle == modelTransform;
                transformToToggle.gameObject.SetActive(shouldBeActive);
            }
        }

        public Transform GetModel(string modelName)
        {
            if (modelsDictionary.ContainsKey(modelName))
            {
                return modelsDictionary[modelName];
            }

            return modelsDictionary["Empty"];
        }
    }
}
