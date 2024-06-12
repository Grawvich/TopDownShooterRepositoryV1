using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxIdleState : BoxBaseState
{
   public override void Awake(LootboxController box)
    {

    }

    public override void Start(LootboxController box)
    {

    }

    public override void EnterState(LootboxController box)
    {
        //Debug.Log("Lootbox has entered IdleState.");
    }

    public override void Update(LootboxController box)
    {

    }

    public override void OnTriggerEnter(Collider col)
    {
        //compare tag to playerTarget, enable interact  button
    }

}
