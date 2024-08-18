using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayAndNightControl : MonoBehaviour
{

    public Light2D DayLight;
    public Gradient DayLightGradient;
    public GameObject[] lamps; 

    private void Update()
    {
        LampController();
        DayLight.color = DayLightGradient.Evaluate(GameManager.TimeController.DayProgress);
    }

    private void LampController()
    {
        if (GameManager.TimeController.DayProgress < 0.40)
        {
            for(int i = 0; i < lamps.Length; i++)
            {
                lamps[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0;i < lamps.Length; i++)
            {
                lamps[i].SetActive(false);
            }
        }
    }
    
    
}
