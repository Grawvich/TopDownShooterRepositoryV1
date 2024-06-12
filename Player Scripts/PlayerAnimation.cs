using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Run(bool isRunning)
    {
        anim.SetBool("isRunning", isRunning);
    }

    public void Idle(bool isIdle)
    {
        anim.SetBool("isIdle", isIdle);
    }

    public void isShooting(bool isShooting)
    {
        anim.SetBool("isShooting", isShooting);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Die()
    {
        anim.SetTrigger("isDead");
    }


}
