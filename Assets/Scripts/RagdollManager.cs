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
    public Transform FrontRaycast, BackRaycast;


    private bool OnGround = false, Standing = true;

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


    void FixedUpdate()
    {
        if (OnGround)
        {
            StartCoroutine(GetUp());
        }

        // RaycastHit hitfront1;
        // if (Physics.Raycast(FrontRaycast.position, transform.TransformDirection(FrontRaycast.forward), out hitfront1,
        //     1f))
        // {
        // }
        //
        // Debug.DrawRay(FrontRaycast.position, transform.TransformDirection(FrontRaycast.forward),
        //     Color.yellow);
        //
        // RaycastHit hitback1;
        // if (Physics.Raycast(BackRaycast.position, transform.TransformDirection(BackRaycast.forward), out hitback1, 1f))
        // {
        // }
        //
        // Debug.DrawRay(BackRaycast.position, transform.TransformDirection(BackRaycast.forward),
        //     Color.yellow);
    }

    public void TurnOnRagdoll()
    {
        // this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Rigidbody.useGravity = false;
        Rigidbody.isKinematic = true;
        Animator.enabled = false;
        CharController.enabled = false;
        OnGround = true;
        Standing = false;
        // Debug.Log("3");
        foreach (Collider c in RagdollParts)
        {
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
        }
    }

    public void TurnOffRagdoll()
    {
        // this.gameObject.GetComponent<BoxCollider>().enabled = false;

        Animator.enabled = true;
        OnGround = false;
        Standing = true;
        Rigidbody.useGravity = true;
        Rigidbody.isKinematic = false;
        CharController.enabled = true;
        foreach (Collider c in RagdollParts)
        {
            c.isTrigger = true;
            c.attachedRigidbody.velocity = Vector3.zero;
        }

        // Debug.Log("3");
    }

    IEnumerator GetUp()
    {
        OnGround = false;
        Debug.Log(" Getting UP ");
        yield return new WaitForSeconds(2f);
        GetUpFromFallenSide();
    }

    IEnumerator GetUpAnimationComplete()
    {
        yield return new WaitForSeconds(2f);
        GotUp();
    }

    void GotUp()
    {
        Standing = true;
    }

    void GetUpFromFallenSide()
    {
        RaycastHit hitfront;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(FrontRaycast.position, transform.TransformDirection(FrontRaycast.forward), out hitfront,
            1f))
        {
            Debug.DrawRay(FrontRaycast.position, transform.TransformDirection(FrontRaycast.forward) * hitfront.distance,
                Color.yellow);
            Debug.Log("Did Hit Front ");
            TurnOffRagdoll();
            Animator.SetTrigger("GetUpFromFront");
        }

        RaycastHit hitback;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(BackRaycast.position, transform.TransformDirection(BackRaycast.forward), out hitback, 1f))
        {
            Debug.DrawRay(BackRaycast.position, transform.TransformDirection(BackRaycast.forward) * hitback.distance,
                Color.yellow);
            Debug.Log("Did Hit Back ");
            TurnOffRagdoll();
            Animator.SetTrigger("GetUpFromBack");
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