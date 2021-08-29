using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    private PlayerHealth player_health;
    // Start is called before the first frame update
    void Awake() //這次必須將Start()改成Awake()
    {
        player_health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void OnEnable() //因為OnEnable()在Start()之前執行
    {
        player_health.Shielded = true;
        Debug.Log("Player is shielded");
    }
    void OnDisable()
    {
        player_health.Shielded = false;
        Debug.Log("Player ISN'T shielded");
    }
}
