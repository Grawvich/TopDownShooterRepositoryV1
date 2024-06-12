using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootboxAnimatorScript : MonoBehaviour
{
    private Animator anim;


    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void BoxIdle(bool isIdle)
    {
        anim.SetBool("isIdle", isIdle);
    }

    public void BoxClose(bool isClosed)
    {
        anim.SetBool("isClosed", isClosed);
    }

        public void BoxOpen(bool isOpen)
    {
        anim.SetBool("isOpen", isOpen);
    }
}
