using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Image scared;
    private float speed = 55f;
    private float amount = 1f;    

    private void Update()
    {
        ShakeImage();
    }

    public void SetMaxHealth( float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void ShakeImage()
    {
        if(slider.value < slider.maxValue * .4)
        {
           scared.rectTransform.anchoredPosition = new Vector3(Mathf.Sin(Time.time * speed) * amount, scared.rectTransform.anchoredPosition.y);

        }
    }

   
}
