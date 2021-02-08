using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class AIHelper : MonoBehaviour
{
    public AICharacterControl AiCharacterControl;

    // Start is called before the first frame update
    void Awake()
    {
        AiCharacterControl = GetComponent<AICharacterControl>();
    }

    public void AIMove(bool enable)
    {
        AiCharacterControl.GiveControls = enable;
    }
}