using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is for Camera to follow player
//smoothing of the camera transform
//keeping the camera field of view within the Bounds
public class CameraBounds : MonoBehaviour
{
    [SerializeField] public Transform followTransform;
    public BoxCollider mapBounds;
    private float xMinPosition;
    private float xMaxPosition;
    private float zMinPosition;
    private float zMaxPosition;
    private float cameraX;
    private float cameraZ;
    private float cameraOrthosize; 
    private float cameraRatio;
    private Camera mainCam;
    //smooth camera movement
    private Vector3 smoothPos;
    [SerializeField] public float smoothSpeed = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        xMinPosition = mapBounds.bounds.min.x;
        xMaxPosition = mapBounds.bounds.max.x;
        zMinPosition = mapBounds.bounds.min.z;
        zMaxPosition = mapBounds.bounds.max.z;
        mainCam = GetComponent<Camera>();
        cameraOrthosize = mainCam.orthographicSize;
        // 2.0f is needed so we dont lose precision in calculation, DO NOT REMOVE
        cameraRatio = (xMaxPosition + cameraOrthosize) / 2.0f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraX = Mathf.Clamp(followTransform.position.x, xMinPosition + cameraOrthosize, xMaxPosition - cameraOrthosize);
        cameraZ = Mathf.Clamp(followTransform.position.z, zMinPosition + cameraOrthosize, zMaxPosition - cameraOrthosize);
        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(cameraX, this.transform.position.y, cameraZ), smoothSpeed);
        this.transform.position = smoothPos;
    }
}
