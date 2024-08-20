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
    public Transform skydisk;
    private void Update()
    {
        LampController();
        DayLight.color = DayLightGradient.Evaluate(GameManager.TimeController.DayProgress);
        SkyRotation();
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
    private void SkyRotation()
    {
        skydisk.rotation = Quaternion.Euler(0, 0, 360.0f * GameManager.TimeController.DayProgress);
    }
    
}
