using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image bar;
    int health, healthMaximum;

    void Update()
    {

    }

    public void SetMaximum(GameObject character)
    {
        healthMaximum = character.GetComponent<Character>().getHealth();
    }

    public void UpdateHealthBar(int health)
    {
        this.health = health;
        bar.fillAmount = Convert.ToSingle(this.health) / healthMaximum;
    }
}