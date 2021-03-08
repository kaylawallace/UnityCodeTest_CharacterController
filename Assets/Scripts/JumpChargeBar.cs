using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpChargeBar : MonoBehaviour
{
  //  private int jumpCharge = 0;
    public Slider slider;

    public void SetMaxCharge(float maxCharge)
    {
        slider.maxValue = maxCharge;
        slider.value = 0;
    }

    public void SetCharge(float jumpCharge)
    {
        slider.value = jumpCharge;
    }
}
