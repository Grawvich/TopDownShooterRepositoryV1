using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform groundcheckTransform;
    [SerializeField] private LayerMask enemyMask;
    public PlayerController player;
    private NavMeshAgent theAgent; 
    private GameObject playerTarget;
    private float attackRange = 2f;
    private bool playerInRange;
    //declare animator variable
    private Animator enemyAnim;

    private float damage = 1f;


    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        theAgent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        ZombieFollow();
    }


    void FixedUpdate()
    { 
        if (Physics.OverlapSphere(groundcheckTransform.position, 0.1f, enemyMask).Length == 0)
        {
            return;
        }       
    }
    void ZombieFollow()
    {
        //follow player with navMesh
        theAgent.SetDestination(playerTarget.transform.position);

        //if player is within distance attack
        float dist = Vector3.Distance(transform.position, playerTarget.transform.position);

        bool playerInRange = dist < attackRange;

        enemyAnim.SetTrigger("Attack");
        enemyAnim.SetBool("isWalking", false);
       
        if (playerInRange)
        {
            print("player is within range and attacks player");
            //enemyAnim.SetBool("isAttacking", true);
            enemyAnim.SetTrigger("Attack");
        }
        else if (!playerInRange)
        {
            //enemyAnim.SetBool("isAttacking", false);
            enemyAnim.SetBool("isWalking", true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("TakeDamage", damage);
        }
    }
}
