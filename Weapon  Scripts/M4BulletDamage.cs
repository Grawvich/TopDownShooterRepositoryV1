using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4BulletDamage : MonoBehaviour
{
    public float damage = 10f; // originally 20
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SendMessage("TakeDamage", damage);
        }
        if (other.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
