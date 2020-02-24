using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class SetInactive : MonoBehaviour
    {
        public void ToggleOwnActivity()
        {
            gameObject.SetActive(false);
        }
    }
}
