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
        if (TriggerState == TriggerType.DoorTrigger)
        {
            GameManager.Instance.TriggerEventForDoor();
        }
        else if (TriggerState == TriggerType.GameOverTrigger)
        {
            GameManager.Instance.TriggerEventForEndLine();
        }
    }
}