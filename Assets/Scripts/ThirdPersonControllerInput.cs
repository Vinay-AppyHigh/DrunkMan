using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Ragdoll;

public class ThirdPersonControllerInput : MonoBehaviour
{
    public FloatingJoystick Joystick;

    public ThirdPersonUserControl Control;

    public RagdollManager RagdollManager;

    // Start is called before the first frame update
    void Start()
    {
        Control = GetComponent<ThirdPersonUserControl>();
        RagdollManager = GetComponent<RagdollManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RagdollManager.GiveControls == true)
        {
            Control.Hinput = Joystick.Horizontal;
            Control.Vinput = Mathf.Clamp(Joystick.Vertical, 0, 1);
        }
        else
        {
            Control.Hinput = 0f;
            Control.Vinput = 0f;
        }
    }
}