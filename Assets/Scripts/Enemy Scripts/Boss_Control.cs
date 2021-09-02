using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Control : MonoBehaviour
{
    private Transform player_target;
    private BossStateChecker boss_state_checker;
    private NavMeshAgent nav_agent;
    private Animator anim;

    private bool finished_atk = true;
    private float current_atk_time;
    private float wait_atk_time = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player_target = GameObject.FindGameObjectWithTag("Player").transform;
        boss_state_checker = GetComponent<BossStateChecker>();
        nav_agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(finished_atk)
        {
            Get_State_Control();
        }
        else
        {
            anim.SetInteger("Atk", 0);
            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                finished_atk = true;
            }
        }
    }
    void Get_State_Control()
    {
        if(boss_state_checker.BossState == Boss_State.Death)
        {
            nav_agent.isStopped = true;
            anim.SetBool("Death", true);
            Destroy(gameObject, 3f);
        }
        else
        {
            if (boss_state_checker.BossState == Boss_State.Pause)
            {
                nav_agent.isStopped = false;
                anim.SetBool("Run", true);
                nav_agent.SetDestination(player_target.position);
            }
            else if (boss_state_checker.BossState == Boss_State.Attack)
            {
                anim.SetBool("Run", false);
                Vector3 target_pos = new Vector3(player_target.position.x,
                                                 transform.position.y,
                                                 player_target.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(target_pos - transform.position),
                                                      5f * Time.deltaTime);
                if (current_atk_time >= wait_atk_time)
                {
                    int atk_range = Random.Range(1, 5);
                    anim.SetInteger("Atk", atk_range);
                    current_atk_time = 0f;
                    finished_atk = false;
                }
                else
                {
                    anim.SetInteger("Atk", 0);
                    current_atk_time += Time.deltaTime;
                }
            }
            else
            {
                anim.SetBool("Run", false);
                nav_agent.isStopped = true;
            }
        }    
    }
}
