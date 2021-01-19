// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics;
// using UnityEngine;
// using Debug = UnityEngine.Debug;
//
// public class InputHandler : MonoBehaviour
// {
//     private float OldPos, NewPos;
//     public Rigidbody Player_RB;
//
//
//     public float tiltAroundY = 0;
//     public float smooth = 100f;
//
//     void Update()
//     {
//         Swipe();
//     }
//
//     public void Swipe()
//     {
//         if (Input.touchCount > 0)
//         {
//             Touch touch = Input.GetTouch(0);
//
//             Vector3 touchpos = Input.GetTouch(0).position;
//             // Debug.Log("touchpos = " + touchpos);
//
//
//             switch (touch.phase)
//             {
//                 case TouchPhase.Began:
//                     OldPos = Input.GetTouch(0).position.x;
//                     NewPos = Input.GetTouch(0).position.x;
//                     break;
//                 case TouchPhase.Moved:
//                     Debug.Log("OldPos = " + OldPos);
//                     Debug.Log("NewPos = " + NewPos);
//                     NewPos = Input.GetTouch(0).position.x;
//                     tiltAroundY = NewPos / 100;
//                     tiltAroundY = tiltAroundY / 2;
//                     tiltAroundY = tiltAroundY * 10;
//                     Quaternion target = Quaternion.Euler(0, tiltAroundY, 0);
//                     //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
//                     //transform.Rotate(transform.rotation.x, tiltAroundY * smooth * Time.deltaTime, transform.rotation.z);
//                     
//                     // Debug.Log("touchpos.x = " + touchpos.x);
//                     // Debug.Log("TouchPhase.Moved");
//                     break;
//                 case TouchPhase.Ended:
//                     // tiltAroundY = 0f;
//                     // PlayerMove.m_EulerAngleVelocity = new Vector3(0, 0, 0);
//                     // Debug.Log("TouchPhase.Ended");
//                     break;
//             }
//         }
//     }
//
//
//     //---------------------------------------------------------------------------------------------------------
// }


using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // private CharacterController CharCtrl;
    //
    // void Start()
    // {
    //     CharCtrl = this.GetComponent<CharacterController>();
    // }
    //
    // void Update()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         // The screen has been touched so store the touch
    //         Touch touch = Input.GetTouch(0);
    //         Vector3 touchPosition = Vector3.zero;
    //         if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
    //         {
    //             // If the finger is on the screen, move the object smoothly to the touch position
    //             touchPosition =
    //                 Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, transform.position.y,
    //                     transform.position.z));
    //
    //             // touchPosition.x = transform.position.y;
    //             // transform.position = Vector3.Lerp(transform.position, touchPosition, 20 * Time.deltaTime);
    //         }
    //
    //
    //         //---------------------------------------------------------------------------------------------------------
    //         Debug.Log("touchPosition.x " + touchPosition.x);
    //         Vector3 NextDir = new Vector3(touchPosition.x, 0, 0.05f);
    //         if (NextDir != Vector3.zero)
    //             transform.rotation = Quaternion.LookRotation(NextDir);
    //         CharCtrl.Move(NextDir / 8);
    //     }
    // }

    public static InputHandler Instance;

    public FloatingJoystick Joystick;
    public float movement_Speed;
    public float rotations_Speed;
    public RagdollManager RagdollManager;

    CharacterController charCtrl;

    private Rigidbody rbody;
    private float joyAngle;
    private Animator Animator;


    public static float xMove, zMove;

    //private Vector3 facDir;
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        charCtrl = GetComponent<CharacterController>();
        SetAnimator();
        SetRagdollManager();
    }

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                            RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
    }

    void SetAnimator()
    {
        Animator = GetComponent<Animator>();
    }

    void SetRagdollManager()
    {
        RagdollManager = GetComponent<RagdollManager>();
    }

    void Update()
    {
        if (RagdollManager.GiveControls == true)
        {
            playerMovmentControler();
            Rotate();
            // playerRotationControler();
        }

        // else
        // {
        //     RagdollManager.GetUpFunc();
        // }
    }


    void playerMovmentControler()
    {
        xMove = Joystick.Horizontal; // + joystek.Horizontal;
        // zMove = Joystick.Vertical; // + joystek.Vertical;
        zMove = 1f;
        float gravity = -9.8f;

        Vector3 moveAxis = new Vector3(xMove, gravity, zMove);

        Vector3 movementX = Camera.main.transform.right * moveAxis.x;
        Vector3 movementZ = Camera.main.transform.forward * moveAxis.z;
        Vector3 Direction = movementX + movementZ;
        moveAxis = new Vector3(Direction.x, gravity, Direction.z);

        charCtrl.Move(((moveAxis) * movement_Speed * Time.deltaTime));

        // Animator.SetFloat("Walk", moveAxis.z);
        // Animator.SetFloat("Rotate", moveAxis.x * 2f);
    }

    void playerMovmentControlerbyRB()
    {
    }

    private Vector3 LastRotationDirection;

    void Rotate()
    {
        float inputX = Joystick.Horizontal;
        float inputZ = Joystick.Vertical;

        inputZ = Mathf.Clamp(inputZ, 0, 1);

        Vector3 lookDirection = new Vector3(inputX, 0, inputZ);

        // if (inputX != 0 || inputZ != 0)
        // {
        //     LastRotationDirection = lookDirection;
        // }

        // LastRotationDirection = new Vector3(Mathf.Clamp(LastRotationDirection.x, -0.5f, 0.5f), LastRotationDirection.y,
        //     LastRotationDirection.z);

        Debug.Log("LastRotationDirection = " + LastRotationDirection);

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);


        float step = rotations_Speed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(lookRotation, transform.rotation, step);
    }


    //
    // void playerRotationControler()
    // {
    //     if (xMove > 0)
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right),
    //             rotations_Speed * Time.deltaTime);
    //     }
    //     else if (xMove < 0)
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left),
    //             rotations_Speed * Time.deltaTime);
    //     }
    //
    //     if (zMove > 0)
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward),
    //             rotations_Speed * Time.deltaTime);
    //     }
    //     else if (zMove < 0)
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back),
    //             rotations_Speed * Time.deltaTime);
    //     }
    // }
}