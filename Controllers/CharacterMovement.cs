using System.Collections;
using System.Collections.Generic;
using System.Drawing;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class CharacterMovement : MonoBehaviour
{

    public NavMeshAgent playerNavMeshAgent;
    public Camera playerCamera;
    public GameObject targetDest;
    
    public LayerMask whatCanBeClickedOn;
    public Interactable focus;
    public PlayerMotor motor;

    
    // Start is called before the first frame update
    
    void Start()
    {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        motor = GetComponent<PlayerMotor>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        return;

        if (Input.GetMouseButton(0))
        {
            Ray myRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;
            
            if (Physics.Raycast(myRay, out myRaycastHit, 100, whatCanBeClickedOn)) //Camera.main.ScreenPointToRay(Input.mousePosition)
            {
                //playerNavMeshAgent.destination = myRaycastHit.point;
                motor.MoveToPoint(myRaycastHit.point);
                RemoveFocus();
            }
            
            /*if (Physics.Raycast(myRay, out myRaycastHit))
            {
                targetDest.transform.position = myRaycastHit.point;
                playerNavMeshAgent.SetDestination(myRaycastHit.point);
            }*/
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1)) // GetMouseButton(1)
        {
            Ray myRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;
            
            if (Physics.Raycast(myRay, out myRaycastHit, 100)) //Camera.main.ScreenPointToRay(Input.mousePosition)
            {
                //Check if hit an interactable
                Interactable interactable = myRaycastHit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
                if(interactable == null)
                {
                    //-myRaycastHit.point
                    motor.MoveToPoint(transform.position);
                    RemoveFocus();
                    
                }
            }
            
        }
        
        
    }
    
    void SetFocus ( Interactable newFocus)
    {   
        if(newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

                focus = newFocus;
                motor.FollowTarget(newFocus);
            
            
        }
        
        newFocus.OnFocused(transform);
        
        
    }
    public void RemoveFocus()
    {
        if(focus != null)
            focus.OnDefocused();
        
        focus = null;
        motor.StopFollowingTarget();
    }
}
