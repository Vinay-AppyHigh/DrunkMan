using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RagdollManager : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Animator Animator;
    public CharacterController CharController;
    public List<Collider> RagdollParts = new List<Collider>();


    void Awake()
    {
        SetRigidbody();
        SetAnimator();
        SetCharController();
        SetRagdollParts();
    }

    void SetRigidbody()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void SetAnimator()
    {
        Animator = GetComponent<Animator>();
    }

    void SetCharController()
    {
        CharController = GetComponent<CharacterController>();
    }


    void SetRagdollParts()
    {
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            if (c.gameObject != this.gameObject)
            {
                c.isTrigger = true;
                RagdollParts.Add(c);
            }
        }
    }

    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         TurnOnRagdoll();
    //     }
    // }

    public void TurnOnRagdoll()
    {
        // this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Rigidbody.useGravity = false;
        Animator.enabled = false;
        CharController.enabled = false;
        // Debug.Log("3");
        foreach (Collider c in RagdollParts)
        {
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
        }
    }


    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("1");
    //     if (collision.collider.tag == "Obstacle")
    //     {
    //         Debug.Log("2");
    //         TurnOnRagdoll();
    //         Debug.Log("4");
    //     }
    // }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Debug.Log("1");
        if (hit.transform.tag == "Obstacle")
        {
            // Debug.Log("2");
            // hit.transform.SendMessage("TurnOnRagdoll", SendMessageOptions.DontRequireReceiver);
            TurnOnRagdoll();
            // Debug.Log("4");
        }
    }
    
    
    
}