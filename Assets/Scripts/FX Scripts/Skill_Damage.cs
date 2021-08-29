using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Damage : MonoBehaviour
{
    public LayerMask enemy_layer;
    public float radius = 0.5f, damage_count = 10f;

    private Enemy_Health enemy_health;
    private bool collided;

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemy_layer);
        foreach(Collider c in hits)
        {
            enemy_health = c.gameObject.GetComponent<Enemy_Health>();
            collided = true;
        }
        if(collided)
        {
            enemy_health.Take_Damage(damage_count);
            enabled = false;
        }
    }
}
