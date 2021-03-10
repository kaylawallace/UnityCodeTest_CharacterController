using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpChargeBar : MonoBehaviour
{
    public Slider slider;

    // Method to set maximum jump charge that UI bar can show 
    public void SetMaxCharge(float maxCharge)
    {
        slider.maxValue = maxCharge;
        slider.value = 0;
    }

    // Method to set current charge shown on UI bar
    public void SetCharge(float jumpCharge)
    {
        slider.value = jumpCharge;
    }
}
