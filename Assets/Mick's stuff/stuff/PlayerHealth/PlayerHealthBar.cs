using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth1(int player)
    {
        slider.maxValue = player;
        slider.value = player;
    }

    public void SetHealth1(int player)
    {
        slider.value = player;
    }

}
