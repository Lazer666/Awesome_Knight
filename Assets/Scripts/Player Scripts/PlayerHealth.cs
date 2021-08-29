using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    private bool isShielded;
    
    public bool Shielded
    {
        get { return isShielded; }
        set { isShielded = value; }
    }

    public void Take_Damage(float amount)
    {
        if (!isShielded)
        {
            health -= amount;
            Debug.Log("Player took damage,health is " + health);

            if(health <= 0f)
            {
                // player dies
            }
        }
    }
}
