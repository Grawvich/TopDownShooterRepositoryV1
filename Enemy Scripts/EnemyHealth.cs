using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static int maxHealth;
    public int currentHealth;
    public EnemyHealthBarScript healthBar;
    private Animator enemyAnim;
    public int scoreValue = 1;
    public Text hitPointsText;

    void Awake()
    {
        EnemyListScript.enemyList.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        //deduct the damage amount from zombies current health
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        hitPointsText.text = currentHealth.ToString() + "/" + maxHealth.ToString();

        if (currentHealth <= 0)
        {
            //print("zombie has entered death state.");
            Die();
        }
    }

    void Die()
    {
        enemyAnim.SetTrigger("Death");
        print("zombie is dead");
        KillCountManagerScript.killCount += scoreValue;
        EnemyListScript.enemyList.Remove(this);
 
        //destroy zombie with 1.5 second delay
        Destroy(gameObject, 1.5f); 
    }

}
