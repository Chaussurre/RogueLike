using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Slider sliderHealth;
    [SerializeField]
    Image ImageHealth;

    private Color DefaultLifeColor;
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        DefaultLifeColor = ImageHealth.color;
    }

    public void SetMaxLife(int life)
    {
        sliderHealth.maxValue = life;
        if (sliderHealth.value < life)
            sliderHealth.value = life;
    }

    public void SetLife(int life)
    {
        if (sliderHealth.maxValue > life)
            sliderHealth.value = life;
        else
            sliderHealth.value = sliderHealth.maxValue;
    }

    public void SetLifeColor(Color color)
    {
        ImageHealth.color = color;
    }

    public void ResetLifeColot()
    {
        ImageHealth.color = DefaultLifeColor;
    }
}
