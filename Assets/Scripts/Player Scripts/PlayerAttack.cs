using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Image img_fillwait_1;
    public Image img_fillwait_2;
    public Image img_fillwait_3;
    public Image img_fillwait_4;
    public Image img_fillwait_5;
    public Image img_fillwait_6;

    private int[] img_fade = new int[] { 0, 0, 0, 0, 0, 0 };

    private Animator anim;
    private bool can_atk = true;

    private PlayerMove player_move;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player_move = GetComponent<PlayerMove>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        {
            can_atk = true;
        }
        else
        {
            can_atk = false;
        }
        Check_Fade();
        Check_Ipt();
    }

    void Check_Fade()
    {
        if(img_fade[0] == 1)
        {
            if(Fade_and_Wait(img_fillwait_1,1f))
            {
                img_fade[0] = 0;
            }
        }
        if (img_fade[1] == 1)
        {
            if (Fade_and_Wait(img_fillwait_2, 0.7f))
            {
                img_fade[1] = 0;
            }
        }
        if (img_fade[2] == 1)
        {
            if (Fade_and_Wait(img_fillwait_3, 0.1f))
            {
                img_fade[2] = 0;
            }
        }
        if (img_fade[3] == 1)
        {
            if (Fade_and_Wait(img_fillwait_4, 0.2f))
            {
                img_fade[3] = 0;
            }
        }
        if (img_fade[4] == 1)
        {
            if (Fade_and_Wait(img_fillwait_5, 0.3f))
            {
                img_fade[4] = 0;
            }
        }
        if (img_fade[5] == 1)
        {
            if (Fade_and_Wait(img_fillwait_6, 0.07f))
            {
                img_fade[5] = 0;
            }
        }
    }
    bool Fade_and_Wait(Image img_fade, float time_fade)
    {
        bool faded = false;

        if (img_fade == null)
            return faded;

        if(!img_fade.gameObject.activeInHierarchy)
        {
            img_fade.gameObject.SetActive(true);
            img_fade.fillAmount = 1f;
        }

        img_fade.fillAmount -= time_fade * Time.deltaTime;

        if(img_fade.fillAmount <= 0f)
        {
            img_fade.gameObject.SetActive(false);
            faded = true;
        }
        return faded;
    }
    void Check_Ipt()
    {
        if (anim.GetInteger("Atk") == 0)
        {
            player_move.Fin_Move = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
            {
                player_move.Fin_Move = true;
            }
        }
        //img_fade[0] meaning image thats at index 0 e.g. the first image
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player_move.Tar_Pos = transform.position;
            if (player_move.Fin_Move && img_fade[0] != 1 && can_atk)
            {
                img_fade[0] = 1;
                anim.SetInteger("Atk", 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player_move.Tar_Pos = transform.position;
            if (player_move.Fin_Move && img_fade[1] != 1 && can_atk)
            {
                img_fade[1] = 1;
                anim.SetInteger("Atk", 2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player_move.Tar_Pos = transform.position;
            if (player_move.Fin_Move && img_fade[2] != 1 && can_atk)
            {
                img_fade[2] = 1;
                anim.SetInteger("Atk", 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player_move.Tar_Pos = transform.position;
            if (player_move.Fin_Move && img_fade[3] != 1 && can_atk)
            {
                img_fade[3] = 1;
                anim.SetInteger("Atk", 4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player_move.Tar_Pos = transform.position;
            if (player_move.Fin_Move && img_fade[4] != 1 && can_atk)
            {
                img_fade[4] = 1;
                anim.SetInteger("Atk", 5);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            player_move.Tar_Pos = transform.position;
            if (player_move.Fin_Move && img_fade[5] != 1 && can_atk)
            {
                img_fade[5] = 1;
                anim.SetInteger("Atk", 6);
            }
        }
        else
        {
            anim.SetInteger("Atk", 0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 tar_pos = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                tar_pos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(tar_pos - transform.position),
                                                  15f * Time.deltaTime);
        }
    }

}