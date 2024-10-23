using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthBar : MonoBehaviour
{
	public Slider slider;

	public Image sliderFill;
	public Color[] colors;

	public void SetMaxHealth(int health) 
	{
		slider.maxValue = health;
		slider.value = health; 
	}

	public void SetHealth(int health)
	{
		slider.value = health; 

		if(slider.value > 0)
			sliderFill.color = colors[(int)slider.value - 1];
	}

}
