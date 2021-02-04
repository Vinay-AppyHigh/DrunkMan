using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class AIList : MonoBehaviour
{
    public static AIList Instance;
    public List<AICharacterControl> AI_Characters;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        AICharacterControl[] ai_characters = this.gameObject.GetComponentsInChildren<AICharacterControl>();
        foreach (AICharacterControl ai in ai_characters)
        {
            if (ai.gameObject != this.gameObject)
            {
                ai.GiveControls = false;
                AI_Characters.Add(ai);
            }
        }
    }

    public void GiveControlsToAll()
    {
        foreach (AICharacterControl ai in AI_Characters)
        {
            ai.GiveControls = true;
        }
    }
}