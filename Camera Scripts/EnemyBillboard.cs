using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBillboard : MonoBehaviour
{
    public GameObject my_cam;

    private void Start()
    {
        my_cam = GameObject.FindGameObjectWithTag("Camera");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        transform.LookAt(transform.position + my_cam.transform.rotation * Vector3.back, 
                            my_cam.transform.rotation * Vector3.down);
        //this instantiates zombies wierdly but it does fix the error we came across when hitting PLAY
    }
}
