using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public UnityEvent buttonClick;

    private void Awake()
    {
       if(buttonClick == null) { buttonClick = new UnityEvent(); }
    }

    void onMouseUp()
    {
        print("click!");
        buttonClick.Invoke();
    }
}
