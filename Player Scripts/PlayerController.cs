using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
 #region PlayerInfo
    [SerializeField] private Transform groundcheckTransform;
    [SerializeField] private LayerMask playerMask;

    public float _movementSpeed;
    [SerializeField] private int maxHealth = 4;
    [SerializeField] private int currentHealth;
    private int startingHealth = 1;
    public HealthBarScript healthBar;

    public Transform partToRotate;
    public float turnSpeed;

    private Joystick joystick;

    private float horizontalInput;
    private float verticalInput;

    private Rigidbody myBody;
    private PlayerAnimation playerAnim;
    //private Invulnerable invulnerable; //MIGHT DELETE LATER
 #endregion

 #region TargetIdentification
    public Transform playerTarget;

    // FIND TARGET WITHIN RANGE
    private Transform target;
    public float range = 50;

    //public tag for update target method
    public string enemyTag = "Enemy";
    private bool isRunning;
    private bool enemiesDetected;
 #endregion

 #region MixinReferences
    MixinBase fireWeapon;
    //public KeyCode fire1;
 #endregion

 #region StateMachine
    private PlayerBaseState currentState; //hold reference to an instance of the PlayerBaseState, a concrete state, as the CONTEXT's current state.

    public PlayerBaseState CurrentState
    {
        get { return currentState; }
    }

    public readonly PlayerIdleState IdleState = new PlayerIdleState();
    public readonly PlayerRunState RunState = new PlayerRunState();
    public readonly PlayerShootState ShootState = new PlayerShootState();
 #endregion

 #region WeaponSwitching

    public GameObject weapon01;
    public MixinBase fireWeapon01;
    public GameObject weapon02;
    public MixinBase fireWeapon02;
    //private GameObject selectedWeapon;
    
    
 #endregion

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimation>();
        joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>();
    }

    void Start()
    {
        currentHealth = startingHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(startingHealth);
        TransitionToState(IdleState);
        fireWeapon = fireWeapon01; 
        weapon01.SetActive(true);
        weapon02.SetActive(false);
        
    }

    //method to update target with nearest target using tagging system
    void UpdateTarget()
    {
        // create list of enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //find nearest Enemy within Range
        float shortestDistance = Mathf.Infinity; 
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }    

    void Update()
    {
        if (!PauseMenuScript.isPaused)
        {
            //call update method in the concrete state
            currentState.Update(this);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // STATES
            if (currentState == IdleState)
            {

                isRunning = false;

                //set animator values
                playerAnim.Run(false);
                playerAnim.isShooting(false);
                playerAnim.Idle(true);

                //set velocity of rigidbody to correct BUG of player sliding after joystick input
                myBody.velocity = new Vector3(0, 0, 0);
            }

            // Run when input used
            if (currentState == RunState)
            {
                isRunning = true;

                //Set animator values
                playerAnim.Run(true);
                playerAnim.isShooting(false);
                playerAnim.Idle(false);

                //make player face playerTarget when in runState
                target = playerTarget;
                var dir = playerTarget.position - partToRotate.position;
                dir.Normalize();
                dir.y = 0.0f;
                partToRotate.rotation = Quaternion.LookRotation(dir);

                // Joystick operation
                UpdateMoveJoystick();
                UpdateLookJoystick();
            }

            // Shoot when target is found and no input
            if (currentState == ShootState)
            {
                //update to nearest enemy target
                UpdateTarget();

                isRunning = false;

                //set velocity of rigidbody to correct BUG of player sliding after joystick input
                myBody.velocity = new Vector3(0, 0, 0);

                //Set animator values
                playerAnim.isShooting(true);
                playerAnim.Run(false);
                playerAnim.Idle(false);

                // Rotate player towards target locked, POST UpdateTarget()
                Vector3 targetDirection = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
                Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                //we only want to rotate around the Y axis
                partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

                if (target == null)
                {
                    return;
                }
                

                // Used to AUTOFIRE during shootstate.
                PlayerAttack();

            }

            
            //player interact button used to open boxes & solve puzzles FEB 08/22 
            //currently the button fires the gun but will need to update functionality to drop the active gun/item
            if (CrossPlatformInputManager.GetButton("InteractButton"))
            {


            //    if (fireWeapon.Check())
            //    {
            //        fireWeapon.Action();
            //    }
            }

        }
    }

    void FixedUpdate()
    {
        //check layer for collision
        if (Physics.OverlapSphere(groundcheckTransform.position, 0.1f, playerMask).Length == 1)
        {
            return;
        }
    }

    public void TransitionToState(PlayerBaseState state)
    {
        currentState = state; //settin the currentState field to the instance of a concrete state passed in as a parameter.
        currentState.EnterState(this); //calling the EnterState of THIS concrete state.
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this);
    }

    //GAMEPLAY METHODS
    void OnTriggerEnter(Collider other)
    {
        //On Trigger with hand gameObject to trigger Take damage.
        if (other.gameObject.CompareTag("Enemy")) 
        {
            //send damage to the enemy
            TakeDamage(1);
        } 

        //ON Trigger for armor/health potion
        if (other.gameObject.CompareTag("Armor"))
        {
            //Heal the player for 1 shield
            Heal(1);
            
        }
            
        if(other.gameObject.CompareTag("AR")) //(Input.GetKeyDown("1"))     2022-03-17 WEAPON SWAP UPDATED & WORKING*****
        {
            fireWeapon = fireWeapon01;                                       
            weapon01.SetActive(true);
            weapon02.SetActive(false);
        }

            //press "1" to select weapon02
        if(other.gameObject.CompareTag("SMG"))  //(Input.GetKeyDown("2"))   2022-03-17 WEAPON SWAP UPDATED & WORKING*****
        {
            fireWeapon = fireWeapon02;
            weapon01.SetActive(false);
            weapon02.SetActive(true);
        }
       
    }

// need a function to determine which weapon is "Selected", 
    public void PlayerAttack()
    {

        if (fireWeapon.Check())
        {
            fireWeapon.Action();
        }
    }

    public void TakeDamage(int damage)
    {       
        //deduct the damage amount from players current health
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            print("Player has died.");
            Die();
        }
    }

    void Die()
    {
        //playerAnim.SetTrigger("isDead");
        playerAnim.Die();
        SceneManager.LoadSceneAsync("Level 01 - Neighbourhood");
        Debug.Log("player enters death state");

        //QUE GameOver State
        //Destroy(this.gameObject);
    }

    //increase players current health
    //call this if armor is picked up
    public void Heal(int healAmount = 1)
    {
        
        //add the provided heal amount to player current health
        currentHealth += healAmount;

        //prevent health from going over initial health
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    #region player joystick movement and rotation

    //used with joystick method "CrossPlatformInputManager"
    void UpdateMoveJoystick()
    {
        horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 joystickDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        transform.Translate(joystickDirection * _movementSpeed, Space.World);
    }

    void UpdateLookJoystick()
    {
        horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 joystickDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        transform.Translate(joystickDirection * _movementSpeed, Space.World);
        Vector3 lookAtPosition = transform.position + joystickDirection;
        transform.LookAt(lookAtPosition);
    }
    #endregion

    //use gizmo to display range, later may be added to weapons and removed from player
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    
}
