using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class InputHandler : MonoBehaviour
{
    private float DeltaX, DeltaY;
    public Rigidbody Player_RB;
    public PlayerMove PlayerMove;
    public Vector3 From, To;
    public float TimeCount = 1.0f;


    void Start()
    {
        From = Vector3.zero;
    }

    // void Update()
    // {
    //     // Swipe();
    // }

    // public void Swipe()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         Touch touch = Input.GetTouch(0);
    //
    //         Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
    //
    //
    //         switch (touch.phase)
    //         {
    //             case TouchPhase.Began:
    //                 DeltaX = touchpos.x - transform.position.x;
    //                 DeltaY = touchpos.y - transform.position.y;
    //                 // Debug.Log("DeltaX = " + DeltaX);
    //                 // Debug.Log("DeltaY = " + DeltaY);
    //                 break;
    //             case TouchPhase.Moved:
    //                 // To = new Vector3(0, (touchpos.x - DeltaX) * 100, 0);
    //                 // Player_RB.gameObject.transform.rotation = Quaternion.Slerp(From, To, TimeCount);
    //                 // TimeCount = TimeCount + Time.deltaTime;
    //
    //                 Debug.Log("DeltaX = " + DeltaX);
    //                 Debug.Log("touchpos.x = " + touchpos.x);
    //                 PlayerMove.tiltAroundY = (touchpos.x - DeltaX) * 100;
    //                 // Debug.Log("TouchPhase.Moved");
    //                 break;
    //             case TouchPhase.Ended:
    //                 // PlayerMove.m_EulerAngleVelocity = new Vector3(0, 0, 0);
    //                 // Debug.Log("TouchPhase.Ended");
    //                 break;
    //         }
    //     }
    // }


    //---------------------------------------------------------------------------------------------------------


    private float rotationSpeed = 0.02F;
    private float lerpSpeed = 0.02F;
    private Vector3 theSpeed = Vector3.zero;
    private Vector3 avgSpeed = Vector3.zero;
    private bool isDragging = true;
    private Vector3 targetSpeedX;

    private float movePos;

/*voidStart() {
isDragging = true;
}*/

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                isDragging = true;
            }

            if (t.phase == TouchPhase.Moved)
            {
                isDragging = true;
                movePos = t.deltaPosition.x;
                theSpeed = new Vector3(0, movePos, 0.0F);
                avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime);
                transform.Rotate(Camera.main.transform.up * theSpeed.y * rotationSpeed, Space.Self);
                Debug.Log(" $$$$$$$$$$    ");
            }

//             if (t.phase == TouchPhase.Stationary)
//             {
//                 isDragging = false;
// //theSpeed = avgSpeed;
//                 float i = Time.deltaTime * lerpSpeed;
//                 theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, 0.02f);
//             }

            if (t.phase == TouchPhase.Ended && t.phase == TouchPhase.Canceled)
            {
                isDragging = false;
//theSpeed = avgSpeed;
                float i = Time.deltaTime * lerpSpeed;
                theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, 0.02f);
            }
        }

        if (Input.touchCount <= 0)
        {
            isDragging = false;
            float i = Time.deltaTime * lerpSpeed;
            theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, 0.02f);
        }


        // if (Input.GetMouseButton(0))
        // {
        //     theSpeed = new Vector3(0, Input.GetAxis("Horizontal"), 0.0F);
        //     avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime * 5);
        // }
        // else
        // {
        //     if (isDragging)
        //     {
        //         theSpeed = avgSpeed;
        //         isDragging = false;
        //     }
        //
        //     float i = Time.deltaTime * lerpSpeed;
        //     theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, i);
        // }
        //
        //
        // // transform.Rotate(Camera.main.transform.right * theSpeed.y * rotationSpeed, Space.World);
        // transform.Rotate(Camera.main.transform.up * theSpeed.y * rotationSpeed, Space.Self);
        // print(theSpeed);
    }

/*
voidOnGUI()
{
GUI.Label(newRect(10, 10, 500, 20), "isDragging:"+isDragging.ToString());
GUI.Label(newRect(10, 25, 500, 20), "theSpeed:"+theSpeed.ToString());
GUI.Label(newRect(10, 50, 500, 20), "AvgSpeed:"+avgSpeed.ToString());
//GUI.Label(newRect(10, 100, 500, 20), "SwipeSpeed:"+newAngles.ToString());
}*/
}