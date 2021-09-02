using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    private bool isShielded;

    private Animator anim;
    public Image health_img;
    void Awake()
    {
        anim = GetComponent<Animator>();
        health_img = GameObject.Find("Health Icon").GetComponent<Image>();
    }
    public bool Shielded
    {
        get { return isShielded; }
        set { isShielded = value; }
    }

    public void Take_Damage(float amount)
    {
        if (!isShielded)
        {
            health -= amount;

            health_img.fillAmount = health / 100f;
            Debug.Log("Player took damage,health is " + health);

            if(health <= 0f)
            {
                anim.SetBool("Death", true);
                if (!anim.IsInTransition(0)
                 && anim.GetCurrentAnimatorStateInfo(0).IsName("Death")
                 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95)
                {
                    //Player died
                    //Destroy Player
                    Destroy(gameObject, 2f);
                }
            }
        }
    }
    public void Heal_Player(float heal_amount)
    {
        health += heal_amount;
        if (health > 100f)
        {
            health = 100f;
        }
        health_img.fillAmount = health / 100f;
    }
}
