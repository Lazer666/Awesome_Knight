using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss_State
{
    None,
    Idle,
    Pause,
    Attack,
    Death
}
public class BossStateChecker : MonoBehaviour
{
    private Transform player_target;
    private Boss_State boss_state = Boss_State.None;
    private float distance_target;

    private Enemy_Health boss_health;
    // Start is called before the first frame update
    void Start()
    {
        player_target = GameObject.FindGameObjectWithTag("Player").transform;
        boss_health = GetComponent<Enemy_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        Set_State();
    }
    void Set_State()
    {
        distance_target = Vector3.Distance(transform.position, player_target.position);
        if(boss_state != Boss_State.Death)
        {
            if(distance_target > 3 && distance_target <= 15f)
            {
                boss_state = Boss_State.Pause;
            }
            else if(distance_target > 15f)
            {
                boss_state = Boss_State.Idle;
            }
            else if(distance_target <= 3f)
            {
                boss_state = Boss_State.Attack;
            }
            else
            {
                boss_state = Boss_State.None;
            }
            if (boss_health.health <= 0f)
            {
                boss_state = Boss_State.Death;
            }
        }
    }
    public Boss_State BossState
    {
        get { return boss_state; }
        set { boss_state = value; }
    }
}
