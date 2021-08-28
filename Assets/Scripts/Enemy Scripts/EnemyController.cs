using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Enemy_State
{
    Idle,
    Walk,
    Run,
    Pause,
    Goback,
    Attack,
    Death
}

public class EnemyController : MonoBehaviour
{
    private float atk_distance = 1.5f,
                  alert_atk_distance = 8f,
                  follow_distance = 15f,
                  enemy_player_distance;

    [HideInInspector]
    public Enemy_State enemy_current_state = Enemy_State.Idle,
                       enemy_last_state = Enemy_State.Idle;

    private Transform player_target;
    private Vector3 initial_pos;

    private float move_speed = 2f,
                  walk_speed = 1f;

    private CharacterController char_controller;
    private Vector3 where_move = Vector3.zero;

    // Attack variable
    private float current_atk_time,
                  wait_atk_time = 1f;

    private Animator anim;
    private bool fin_animation = true,
                 fin_movement = true;

    private NavMeshAgent nav_agent;
    private Vector3 where_navigate;

    //Health Script

    // Start is called before the first frame update
    void Start()
    {
        player_target = GameObject.FindGameObjectWithTag("Player").transform;
        nav_agent = GetComponent<NavMeshAgent>();
        char_controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        initial_pos = transform.position;
        where_navigate = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if HP <=0, State become death.
        if(enemy_current_state != Enemy_State.Death)
        {
            enemy_current_state = SetEnemy_State(enemy_current_state,enemy_last_state,enemy_player_distance);
            if (fin_movement)
            {
                GetState_Control(enemy_current_state);
            }
            else
            {
                if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    fin_movement = true;
                }
                else if(!anim.IsInTransition(0)
                      && anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk1")
                      || anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk2"))
                {
                    anim.SetInteger("Atk", 0);
                }
            }
        }
        else
        {
            anim.SetBool("Death", true);
            char_controller.enabled = false;
            nav_agent.enabled = false;

            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death")
             && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Destroy(gameObject, 2f);
            }
        }
    }
    Enemy_State SetEnemy_State(Enemy_State cur_state,Enemy_State last_state,float enemy_player_distance)
    {
        enemy_player_distance = Vector3.Distance(transform.position, player_target.position);

        float initial_distance = Vector3.Distance(initial_pos, transform.position);
        if(initial_distance > follow_distance)
        {
            last_state = cur_state;
            cur_state = Enemy_State.Goback;
        }
        else if(enemy_player_distance <= atk_distance)
        {
            last_state = cur_state;
            cur_state = Enemy_State.Attack;
        }
        else if(enemy_player_distance >= alert_atk_distance
             && last_state == Enemy_State.Pause
             || last_state == Enemy_State.Attack)
        {
            last_state = cur_state;
            cur_state = Enemy_State.Pause;
        }
        else if(enemy_player_distance <= alert_atk_distance
             && enemy_player_distance > atk_distance)
        {
            if(cur_state != Enemy_State.Goback || last_state == Enemy_State.Walk)
            {
                last_state = cur_state;
                cur_state = Enemy_State.Pause;
            }
        }
        else if(enemy_player_distance > alert_atk_distance
             && last_state != Enemy_State.Goback
             && last_state != Enemy_State.Pause)
        {
            last_state = cur_state;
            cur_state = Enemy_State.Walk;
        }
        return cur_state;
    }
    void GetState_Control(Enemy_State cur_state)
    {
        if (cur_state == Enemy_State.Run || cur_state == Enemy_State.Pause)
        {
            if (cur_state != Enemy_State.Attack)
            {
                Vector3 target_pos = new Vector3(player_target.transform.position.x, transform.position.y, player_target.position.z);
                if (Vector3.Distance(transform.position, target_pos) >= 2.1f)
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", true);
                    nav_agent.SetDestination(target_pos);
                }
            }
        }
        else if(cur_state == Enemy_State.Attack)
        {
            anim.SetBool("Run", false);
            where_move.Set(0f, 0f, 0f);
            nav_agent.SetDestination(transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player_target.position - transform.position), 5f * Time.deltaTime);
            if(current_atk_time >= wait_atk_time)
            {
                int atk_range = Random.Range(1, 3);
                anim.SetInteger("Atk", atk_range);
                fin_animation = false;
                current_atk_time = 0f;
            }
            else
            {
                anim.SetInteger("Atk", 0);
                current_atk_time += Time.deltaTime;
            }
        }
        else if(cur_state == Enemy_State.Goback)
        {
            anim.SetBool("Run", true);
            Vector3 target_pos = new Vector3(initial_pos.x, transform.position.y, initial_pos.z);
            nav_agent.SetDestination(target_pos);
            if(Vector3.Distance(target_pos, initial_pos) <= 3.5f)
            {
                enemy_last_state = cur_state;
                cur_state = Enemy_State.Walk;
            }
        }
        else if(cur_state == Enemy_State.Walk)
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", true);
            if(Vector3.Distance(transform.position,where_navigate) <= 2f)
            {
                where_navigate.x = Random.Range(initial_pos.x - 5f, initial_pos.x + 5f);
                where_navigate.z = Random.Range(initial_pos.z - 5f, initial_pos.z + 5f);
            }
            else
            {
                nav_agent.SetDestination(where_navigate);
            }
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
            where_move.Set(0f, 0f, 0f);
            nav_agent.isStopped = true;
        }
    }
}