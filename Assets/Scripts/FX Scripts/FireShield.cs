using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    private PlayerHealth player_health;
    // Start is called before the first frame update
    void Awake() //�o�������NStart()�令Awake()
    {
        player_health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void OnEnable() //�]��OnEnable()�bStart()���e����
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
