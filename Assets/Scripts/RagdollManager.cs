using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RagdollManager : MonoBehaviour
{
    public Rigidbody Rigidbody;

    public static RagdollManager Instance;

    // public Animator Animator;
    public CharacterController CharController;
    public CapsuleCollider CapsuleCollider;
    public List<Collider> RagdollParts = new List<Collider>();


    public bool OnGround = false, Standing = true, GiveControls = true;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        SetRigidbody();
        // SetAnimator();
        SetCharController();
        SetRagdollParts();
    }

    public GameObject BlackScreen;

    void FadeBlackOut()
    {
        BlackScreen.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    IEnumerator Start()
    {
        TurnOnRagdoll();
        yield return new WaitForEndOfFrame();
        TurnOffRagdoll();
        yield return new WaitForSeconds(4f);
        FadeBlackOut();
    }

    void SetRigidbody()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void SetAnimator()
    {
        //  Animator = GetComponent<Animator>();
    }

    void SetCharController()
    {
        CharController = GetComponent<CharacterController>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
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
    }

    public void TurnOnRagdoll()
    {
        // this.gameObject.GetComponent<BoxCollider>().enabled = false;
        RagdollHelper helper = GetComponent<RagdollHelper>();
        helper.ragdolled = true;
        Rigidbody.useGravity = false;
        Rigidbody.isKinematic = true;
        //   Animator.enabled = false;
//        CharController.enabled = false;
        CapsuleCollider.isTrigger = true;
        OnGround = true;
        Standing = false;
        GiveControls = false;


        Debug.Log("3");
        foreach (Collider c in RagdollParts)
        {
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
        }
    }

    public void TurnOffRagdoll()
    {
        // this.gameObject.GetComponent<BoxCollider>().enabled = false;
        RagdollHelper helper = GetComponent<RagdollHelper>();
        helper.ragdolled = false;
        // Animator.enabled = true;
        // Animator.Rebind();
        OnGround = false;
        Standing = true;
        Rigidbody.useGravity = true;
        Rigidbody.isKinematic = false;
        foreach (Collider c in RagdollParts)
        {
            c.isTrigger = true;
            c.attachedRigidbody.velocity = Vector3.zero;
        }

        //  CharController.enabled = true;
        CapsuleCollider.isTrigger = false;
        // Debug.Log("3");
    }

    public void GetUpFunc()
    {
        StartCoroutine(GetUp());
    }


    IEnumerator GetUp()
    {
        OnGround = false;
        yield return new WaitForSeconds(2f);
        GetUpFromFallenSide();
    }

    IEnumerator GetUpAnimationComplete()
    {
        Debug.Log(" Getting UP ");
        yield return new WaitForSeconds(0.5f);
        Debug.Log(" Got UP ");
        GiveControls = true;
    }

    public void GotUp()
    {
        StartCoroutine(GetUpAnimationComplete());
    }

    void GetUpFromFallenSide()
    {
        // RaycastHit hitfront;
        // // Does the ray intersect any objects excluding the player layer
        // if (Physics.Raycast(FrontRaycast.position, transform.TransformDirection(FrontRaycast.forward), out hitfront,
        //     1f))
        // {
        //     Debug.DrawRay(FrontRaycast.position, transform.TransformDirection(FrontRaycast.forward) * hitfront.distance,
        //         Color.yellow);
        //     Debug.Log("Did Hit Front ");
        //    // TurnOffRagdoll();
        //     //Animator.SetTrigger("GetUpFromFront");
        // }
        //
        // RaycastHit hitback;
        // // Does the ray intersect any objects excluding the player layer
        // if (Physics.Raycast(BackRaycast.position, transform.TransformDirection(BackRaycast.forward), out hitback, 1f))
        // {
        //     Debug.DrawRay(BackRaycast.position, transform.TransformDirection(BackRaycast.forward) * hitback.distance,
        //         Color.yellow);
        //     Debug.Log("Did Hit Back ");
        //     
        //     //Animator.SetTrigger("GetUpFromBack");
        // }
        TurnOffRagdoll();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("1");
        if (collision.collider.tag == "Obstacle")
        {
            Debug.Log("2");
            TurnOnRagdoll();
            Debug.Log("4");
        }
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Debug.Log("1");
        if (hit.transform.tag == "Obstacle")
        {
//            Debug.Log("2");
            // hit.transform.SendMessage("TurnOnRagdoll", SendMessageOptions.DontRequireReceiver);
            TurnOnRagdoll();
            // Debug.Log("4");
        }
    }
}