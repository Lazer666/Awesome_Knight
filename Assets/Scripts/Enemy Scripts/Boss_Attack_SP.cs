using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack_SP : MonoBehaviour
{
    public GameObject boss_fire;
    public GameObject boss_magic;

    private Transform player_target;
    // Start is called before the first frame update
    void Start()
    {
        player_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void BossFireTornado()
    {
        Instantiate(boss_fire, player_target.position, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
    }
    void BossSpell()
    {
        Vector3 temp = player_target.position;
        temp.y += 1.5f;
        Instantiate(boss_magic, temp, Quaternion.identity);
    }
}
