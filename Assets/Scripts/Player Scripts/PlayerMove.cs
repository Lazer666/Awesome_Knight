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
    private bool fin_move = true;

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
        Move_Player();
        cha_con.Move(player_move);
    }
    void Move_Player()
    {
        if(Input.GetMouseButton(0))
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
}