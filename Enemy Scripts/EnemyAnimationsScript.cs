using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationsScript : MonoBehaviour
{
    public Animator enemyAnim;

    void Awake()
    {
        enemyAnim = GetComponent<Animator>();
    }
    public void Walk(bool isWalking)
    {
        enemyAnim.SetBool("isWalking", isWalking);
    }

    public void Attack()
    {
        enemyAnim.SetTrigger("Attack");
    }

}
