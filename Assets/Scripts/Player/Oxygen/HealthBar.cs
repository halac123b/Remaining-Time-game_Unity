using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

  [SerializeField] private Slider slider;
  [SerializeField] private Gradient gradient;
  [SerializeField] private Image fill;
  [SerializeField] private OxyStatus oxyStatus;

  public void Start()
  {
    oxyStatus.OnProcessing += SetHealth;
  }
  public void SetMaxHealth(int health)
  {
    slider.maxValue = health;
    slider.value = health;

    fill.color = gradient.Evaluate(1f);
  }

  public void SetHealth(object sender, OxyStatus.IntEventArg arg)
  {
    slider.value = arg.value / slider.maxValue * 100;

    fill.color = gradient.Evaluate(slider.normalizedValue);
  }

}
