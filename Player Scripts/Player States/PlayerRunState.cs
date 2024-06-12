using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerRunState : PlayerBaseState
{
    public override void Awake(PlayerController player)
    {

    }

    public override void Start(PlayerController player)
    {

    }

    public override void EnterState(PlayerController player)
    {
        //Debug.Log("Player has entered RunState.");
    }

    public override void Update(PlayerController player)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        //REMMED OUT FOR JOYSTICK UPDATE
        if (CrossPlatformInputManager.GetAxis("Horizontal") == 0 && CrossPlatformInputManager.GetAxis("Vertical") == 0 && enemies.Length > 0 )
        {
            player.TransitionToState(player.ShootState);
        }

        if (CrossPlatformInputManager.GetAxis("Horizontal") == 0 && CrossPlatformInputManager.GetAxis("Vertical") == 0 && enemies.Length <= 0)
        {
            player.TransitionToState(player.IdleState);
        }
    }

    public override void OnCollisionEnter(PlayerController player)
    {
        
    }
}
