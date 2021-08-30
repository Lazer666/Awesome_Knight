using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float heal_amount = 20f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Heal_Player(heal_amount);
        Debug.Log("Players health is " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health);
    }
}