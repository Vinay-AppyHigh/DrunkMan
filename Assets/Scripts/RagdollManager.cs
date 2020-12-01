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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TurnOnRagdoll();
        }
    }

    public void TurnOnRagdoll()
    {
        // this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Rigidbody.useGravity = false;
        Animator.enabled = false;
        CharController.enabled = false;

        foreach (Collider c in RagdollParts)
        {
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
        }
    }
}