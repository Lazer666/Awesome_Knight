using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator anim;
    private CharacterController cha_con;
    private CollisionFlags col_flags = CollisionFlags.None;

    private float move_sp = 5f;
    private bool can_move;
    public bool fin_move = true;

    private Vector3 tar_pos = Vector3.zero;
    private Vector3 player_move = Vector3.zero;

    private float player_distance_pt;

    private float gravity = 9.8f;
    private float height;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        cha_con = GetComponent<CharacterController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Calcul_Hei();
        Check_fin_move();
    }

    void Check_fin_move()
    {
        if(!fin_move)
        {
            if(!anim.IsInTransition(0)
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Stand")
            &&  anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                fin_move = true;
            }
        }
        else
        {
            Move_Player();
            player_move.y = height * Time.deltaTime;
            col_flags = cha_con.Move(player_move);
        }
    }

    void Move_Player()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider is TerrainCollider)
                {
                    player_distance_pt = Vector3.Distance(transform.position, hit.point);
                    if(player_distance_pt >= 1f)
                    {
                        can_move = true;
                        tar_pos = hit.point;
                    }
                }
            }
        }
        if(can_move)
        {
            anim.SetFloat("Walk", 1f);
            Vector3 tar_temp = new Vector3(tar_pos.x, transform.position.y, tar_pos.z);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(tar_temp-transform.position),
                                                  15f * Time.deltaTime);
            player_move = transform.forward * move_sp * Time.deltaTime;
            if(Vector3.Distance(transform.position, tar_pos) <= 0.5f)
            {
                can_move = false;
            }
        }
        else
        {
            player_move.Set(0f, 0f, 0f);
            anim.SetFloat("Walk", 0f);
        }
    }
    void Calcul_Hei()
    {
        if(Is_Grounded())
        {
            height = 0f;
        }
        else
        {
            height -= gravity * Time.deltaTime;
        }
    }
    bool Is_Grounded()
    {
        if(col_flags == CollisionFlags.CollidedBelow)
        {
            return true;
        }
        return false;
    }
}