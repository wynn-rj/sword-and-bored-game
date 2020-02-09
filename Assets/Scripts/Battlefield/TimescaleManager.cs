using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimescaleManager : MonoBehaviour
{
   public void SpeedUpTime()
    {
        Time.timeScale = 4;
    }
    public void SlowDownTime()
    {
        Time.timeScale = .25f;
    }
}
