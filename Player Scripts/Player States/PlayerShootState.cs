using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShootState : PlayerBaseState 
{
    public MixinBase fireWeapon;

    public override void Awake(PlayerController player)
    {
    
    }

    public override void Start(PlayerController player)
    {
 
    }

    public override void EnterState(PlayerController player)
    {
        //Debug.Log("player has entered ShootState");
    }

    public override void Update(PlayerController player)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (CrossPlatformInputManager.GetAxis("Horizontal") != 0 || CrossPlatformInputManager.GetAxis("Vertical") != 0)
        {
            player.TransitionToState(player.RunState);
        }
        
        if (enemies.Length <= 0)
        {
            player.TransitionToState(player.IdleState);
        }    
    }
 
    public override void OnCollisionEnter(PlayerController player)
    {
       
    }


}
