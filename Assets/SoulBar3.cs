using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoulBar3 : MonoBehaviour
{
    public Slider slider;

    public void SetMax(int maxvalue)
    {
        slider.maxValue = maxvalue;
        slider.value = maxvalue;
    }

    public void Setcurrent(int value)
    {
        slider.value = value;
    }
}
