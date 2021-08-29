using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public float damage_amount = 10f;

    private Transform player_target;
    private Animator anim;
    private bool finished_attack = true;
    private float damage_distance = 2f;

    private PlayerHealth player_health;
    // Start is called before the first frame update
    void Start()
    {
        player_target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        player_health = player_target.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(finished_attack)
        {
            Deal_Damage(Check_Attack());
        }
        else
        {
            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                finished_attack = true;
            }
        }
    }
    void Deal_Damage(bool isAttacking)
    {
        if (isAttacking)
        {
            if (Vector3.Distance(transform.position, player_target.position) <= damage_distance)
            {
                if (Vector3.Distance(transform.position, player_target.position) <= damage_distance)
                {
                    player_health.Take_Damage(damage_amount);
                }
            }
        }
    }
    bool Check_Attack()
    {
        bool isAttack = false;

        if (!anim.IsInTransition(0)
          && anim.GetCurrentAnimatorStateInfo(0).IsName("Atk1")
          || anim.GetCurrentAnimatorStateInfo(0).IsName("Atk2"))
        {
            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                isAttack = true;
                finished_attack = false;
            }
        }
            return isAttack;
    }
}