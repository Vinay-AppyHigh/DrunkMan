using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerHandler : MonoBehaviour
{
    public TriggerType TriggerState;

    public enum TriggerType
    {
        DoorTrigger,
        GameOverTrigger
    }

    void OnTriggerEnter(Collider other)
    {
        if (TriggerState == TriggerType.DoorTrigger && other.tag == "Player" || other.tag == "AI")
        {
            GameManager.Instance.TriggerEventForDoor();
        }
        else if (TriggerState == TriggerType.GameOverTrigger && other.tag == "Player")
        {
            GameManager.Instance.TriggerEventForEndLine();
        }
    }
}