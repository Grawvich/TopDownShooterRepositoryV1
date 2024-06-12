using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        //On Trigger with hand gameObject to trigger Take damage.
        if (other.gameObject.CompareTag("Player")) 
        {
            Destroy(gameObject);
        } 


    }
}
