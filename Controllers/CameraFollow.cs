using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    public int depth = -20;
    public int depth2 = 11;
    float minFov = 24f;
    float maxFov = 145f;
    float sensitivity = 10f;
    private float currentZoom = 10f;
    public float zoomSpeed = 4f;

    
    // Update is called once per frame
    void Update()
    {
        var fov = Camera.main.fieldOfView;
        
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;


        if(playerTransform != null)
        {
            //transform.position = playerTransform.position + new Vector3(0, 10, -6);
            transform.position = playerTransform.position + new Vector3(0, 14, -11); //0, 7, -8
            //transform.rotation = Quaternion.Slerp(transform.rotation, playerTransform.rotation, .1f);
        }
        
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minFov, maxFov);
    }
    public void setTarget(Transform target)
    {
        playerTransform = target;
    }
}
