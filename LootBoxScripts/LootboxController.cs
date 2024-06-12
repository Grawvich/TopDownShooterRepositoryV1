using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootboxController : MonoBehaviour
{
    private LootboxAnimatorScript boxAnim;
    private BoxBaseState currentState; //hold reference to an instance of the BoxBaseState, a concrete state, as the CONTEXT's current state.

    public BoxBaseState CurrentState
    {
        get { return currentState; }
    }
    public readonly BoxIdleState IdleState = new BoxIdleState();
    public readonly BoxCloseState CloseState = new BoxCloseState();
    public readonly BoxOpenState OpenState = new BoxOpenState();

    void Awake()
    {
        boxAnim = GetComponent<LootboxAnimatorScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        
        //if box is closed && "interact button" is pressed then open box
        if(currentState == IdleState)
        {
            //if interact button is pressed while in IdleState. open Box //currentState==OpenState
            //research how to enable Trigger through script. playerTarget Gameobject may be what we use
            boxAnim.BoxIdle(true);
            boxAnim.BoxOpen(false);
            boxAnim.BoxClose(false);
        }

        if(currentState == OpenState)
        {
            //StartCoroutine to WaitForSeconds(15) before transitioning to Close State
            boxAnim.BoxIdle(false);
            boxAnim.BoxOpen(true);
            boxAnim.BoxClose(false);
        }

        if(currentState == CloseState)
        {
            //If wave.count % 5, transition to IdleState
            boxAnim.BoxIdle(false);
            boxAnim.BoxOpen(false);
            boxAnim.BoxClose(true);
        }
    }

    public void TransitionToState(BoxBaseState state)
    {
        currentState = state; //settin the currentState field to the instance of a concrete state passed in as a parameter.
        currentState.EnterState(this); //calling the EnterState of THIS concrete state.
    }

    public void OnTriggerEnter() 
    {

    }
}
