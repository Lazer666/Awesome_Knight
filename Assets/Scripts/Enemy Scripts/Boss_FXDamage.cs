using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_FXDamage : MonoBehaviour
{
    public LayerMask player_layer;
    public float radius = 0.3f;
    public float damage_count = 10f;

    private PlayerHealth player_health;
    private bool collided;

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, player_layer);
        foreach(Collider c in hits)
        {
            player_health = c.gameObject.GetComponent<PlayerHealth>();
            collided = true;
        }
        if(collided)
        {
            player_health.Take_Damage(damage_count);
            enabled = false;
        }
    }
}
