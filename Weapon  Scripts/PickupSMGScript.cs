using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSMGScript : MonoBehaviour
{
    public GameObject newWeapon; // weaponFab
    
    void OnTriggerEnter(Collider other)
    {
        //On Trigger with hand gameObject to trigger Take damage.
        if (other.gameObject.CompareTag("Player")) 
        {
            print("SMG Sphere has been collided with.");
            //PlayerController ActiveWeapon = other.gameObject.GetComponent<PlayerController>();        

            //ActiveWeapon.SelectWeapon();
            //Destroy(gameObject);
        } 


    }
}
