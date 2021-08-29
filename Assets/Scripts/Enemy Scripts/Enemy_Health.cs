using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float health = 100f;
    public void Take_Damage(float amount)
    {
        health -= amount;
        
        print("Enemy took damage, health is " + health);

        if(health <= 0)
        {

        }
    }
}
