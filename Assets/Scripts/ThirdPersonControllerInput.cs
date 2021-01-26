using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ThirdPersonControllerInput : MonoBehaviour
{
    public FloatingJoystick Joystick;

    public ThirdPersonUserControl Control;

    // Start is called before the first frame update
    void Start()
    {
        Control = GetComponent<ThirdPersonUserControl>();
    }

    // Update is called once per frame
    void Update()
    {
        Control.Hinput = Joystick.Horizontal;
        Control.Vinput = 1; //Joystick.Vertical;
    }
}