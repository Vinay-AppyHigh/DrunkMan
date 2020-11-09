using System.Collections;
using System.Collections.Generic;
using BzKovSoft.RagdollTemplate.Scripts.Charachter;
using UnityEngine;

public class RoadTriggers : MonoBehaviour
{
    public BzThirdPersonControl BzThirdPersonControl;

    void OnTriggerEnter(Collider collider)
    {
        if (gameObject.tag == "Trigger (1)")
        {
            BzThirdPersonControl.Trigger1 = true;
            BzThirdPersonControl.Trigger1 = false;
            BzThirdPersonControl.Trigger1 = false;
            BzThirdPersonControl.Trigger1 = false;
        }

        if (gameObject.tag == "Trigger (2)")
        {
            BzThirdPersonControl.Trigger1 = false;
            BzThirdPersonControl.Trigger1 = true;
            BzThirdPersonControl.Trigger1 = false;
            BzThirdPersonControl.Trigger1 = false;
        }

        if (gameObject.tag == "Trigger (3)")
        {
            BzThirdPersonControl.Trigger1 = false;
            BzThirdPersonControl.Trigger1 = false;
            BzThirdPersonControl.Trigger1 = true;
            BzThirdPersonControl.Trigger1 = false;
        }

        if (gameObject.tag == "Trigger (4)")
        {
            BzThirdPersonControl.Trigger1 = false;
            BzThirdPersonControl.Trigger1 = false;
            BzThirdPersonControl.Trigger1 = false;
            BzThirdPersonControl.Trigger1 = true;
        }
    }
}