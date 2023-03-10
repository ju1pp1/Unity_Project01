using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    //TESTING
    //public bool isInRange;
    //public KeyCode interactKey;
    //public UnityEvent interactAction;

    public float radius = 3f; //3.2f
    public Transform interactionTransform;

    public bool isFocus = false;
    Transform player;
    public bool hasInteracted = false;
    public virtual void Interact() //virtual
    {
        // This method is meant to be overwritten.
    }
    
    void Update ()
    {
       /* 
       if(isInRange) //If we are in range to interact.
        {
            if (Input.GetKeyDown(interactKey)) //Player presses key
            {
                interactAction.Invoke();
            }
        }
        */
        
        
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
            if (isFocus && !hasInteracted)
            {
                
                float distance = Vector3.Distance(player.position, interactionTransform.position);
                if (distance <= radius) // 
                {
                    //Debug.Log("Interact!!");
                    
                    hasInteracted = true;
                    
                    Interact();
                }
            }
        //}
        

    }
    /*
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player now in range");
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player not in range");
        }
    }
    */
    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
        
        
    }
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    
    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
