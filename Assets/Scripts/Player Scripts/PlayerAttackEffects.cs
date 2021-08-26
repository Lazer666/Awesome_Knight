using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEffects : MonoBehaviour
{
    public GameObject groundimpact_spawn, kickFX_spawn, fire_tornado_spawn, fireshield_spawn;
    public GameObject groundimpact_prefab, kickFX_prefab, fire_tornado_prefab,
                      fireshield_prefab, healFX_prefab, thunderAttackFX_prefab;

    void GroundImpact()
    {
        Instantiate(groundimpact_prefab, groundimpact_spawn.transform.position, Quaternion.identity);
    }
    void Kick()  //Hit
    {
        Instantiate(kickFX_prefab, kickFX_spawn.transform.position, Quaternion.identity);
    }
    void FireTornado()
    {
        Instantiate(fire_tornado_prefab, fire_tornado_spawn.transform.position, Quaternion.identity);
    }
    void FireShield()
    {
        GameObject fire_obj = Instantiate(fireshield_prefab, fireshield_spawn.transform.position, Quaternion.identity) as GameObject;
        fire_obj.transform.SetParent(transform);
    }
    void Heal()
    {
        Vector3 temp = transform.position;
        temp.y += 2f;
        GameObject heal_obj = Instantiate(healFX_prefab, temp, Quaternion.identity) as GameObject;
        heal_obj.transform.SetParent(transform);
    }
    void ThunderAttack()    //Lightning
    {
        for (int i = 0; i < 8; i++)
        {
            Vector3 pos = Vector3.zero;
            if(i==0)
            {
                pos = new Vector3(transform.position.x - 4f, transform.position.y + 2f, transform.position.z);
            }
            else if (i == 1)
            {
                pos = new Vector3(transform.position.x + 4f, transform.position.y + 2f, transform.position.z);
            }
            else if (i == 2)
            {
                pos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z - 4f);
            }
            else if (i == 3)
            {
                pos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z + 4f);
            }
            else if (i == 4)
            {
                pos = new Vector3(transform.position.x + 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }
            else if (i == 5)
            {
                pos = new Vector3(transform.position.x - 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }
            else if (i == 6)
            {
                pos = new Vector3(transform.position.x - 2.5f, transform.position.y + 2f, transform.position.z - 2.5f);
            }
            else if (i == 7)
            {
                pos = new Vector3(transform.position.x + 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }

            Instantiate(thunderAttackFX_prefab, pos, Quaternion.identity);
        }
    }
}