using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornado_Move : MonoBehaviour
{
    public LayerMask enemy_layer;
    public float radius = 0.5f, damage_count = 10f;

    public GameObject fire_explosion;

    private Enemy_Health enemy_health;
    private bool collided;

    private float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Check_Damage();
    }
    
    void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Check_Damage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemy_layer);
        foreach (Collider c in hits)
        {
            enemy_health = c.gameObject.GetComponent<Enemy_Health>();
            collided = true;
        }
        if (collided)
        {
            enemy_health.Take_Damage(damage_count);
            Vector3 temp = transform.position;
            temp.y += 2f;
            Instantiate(fire_explosion, temp, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}