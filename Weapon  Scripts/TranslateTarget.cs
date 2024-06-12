using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 direction;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        target.Translate(direction * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            print("bullet has collided with an enemy.");
            Destroy(this.gameObject);
        }

        if(other.gameObject.CompareTag("Environment"))
        {
            print("bullet has collided with the environment");
        }

        else{
            print("bullet has collided with an obstacle.");
            Destroy(this.gameObject);
        }
    }
}
