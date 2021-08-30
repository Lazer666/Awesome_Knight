using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController_easy : MonoBehaviour
{
    public Transform[] walk_points;
    private int walk_index = 0;
    private Transform player_target;

    private Animator anim;
    private NavMeshAgent nav_agent;

    private float walk_distance = 8f, atk_distance = 2f;
    private float cur_atk_time, wait_atk_time = 1f;

    private Vector3 next_destination;

    private Enemy_Health enemy_health;
    // Start is called before the first frame update
    void Start()
    {
        player_target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        nav_agent = GetComponent<NavMeshAgent>();
        enemy_health = GetComponent<Enemy_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_health.health > 0f)
        {
            Move_and_Attack();
        }
        else
        {
            anim.SetBool("Death", true);
            nav_agent.enabled = false;

            if (!anim.IsInTransition(0)
             && anim.GetCurrentAnimatorStateInfo(0).IsName("Death")
             && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Destroy(gameObject, 2f);
            }
        }
    }
    void Move_and_Attack()
    {
        float distance = Vector3.Distance(transform.position, player_target.position);
        if (distance > walk_distance)
        {
            if (nav_agent.remainingDistance <= 0.5f)
            {
                nav_agent.isStopped = false;
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                anim.SetInteger("Atk", 0);

                next_destination = walk_points[walk_index].position;
                nav_agent.SetDestination(next_destination);

                if (walk_index == walk_points.Length - 1)
                    walk_index = 0;
                else
                    walk_index++;
            }
        }
        else
        {
            if (distance > atk_distance)
            {
                nav_agent.isStopped = false;
                anim.SetBool("Walk", false);
                anim.SetBool("Run", true);
                anim.SetInteger("Atk", 0);

                nav_agent.SetDestination(player_target.position);
            }
            else
            {
                nav_agent.isStopped = true;
                anim.SetBool("Run", false);
                Vector3 target_pos = new Vector3(player_target.position.x,
                                                 transform.position.y,
                                                 player_target.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(target_pos - transform.position),
                                                      5f * Time.deltaTime);
                if (cur_atk_time >= wait_atk_time)
                {
                    int atk_range = Random.Range(1, 3);
                    anim.SetInteger("Atk", atk_range);
                    cur_atk_time = 0f;
                }
                else
                {
                    anim.SetInteger("Atk", 0);
                    cur_atk_time += Time.deltaTime;
                }
            }
        }
    }
}
