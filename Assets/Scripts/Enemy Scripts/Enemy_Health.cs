using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour
{
    public float health = 100f;
    public Image health_img;

    //void Awake()
    //{
    //    if (tag == "Boss")
    //    {
    //        health_img = GameObject.Find("Health Foreground Boss").GetComponent<Image>();
    //    }
    //    else
    //    {
    //        health_img = GameObject.Find("Health Foreground").GetComponent<Image>();
    //    }
    //}
    public void Take_Damage(float amount)
    {
        health -= amount;
        health_img.fillAmount = health / 100f; ;

        print("Enemy took damage, health is " + health);

        if(health <= 0)
        {

        }
    }
}
