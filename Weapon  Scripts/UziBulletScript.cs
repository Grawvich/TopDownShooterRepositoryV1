using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziBulletScript : MonoBehaviour
{
     public float damage = 10f; // originally 20
    // Start is called before the first frame update
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
