using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Ragdoll;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject LevelClearMssg;
    public BoxCollider EndLine;
    public BoxCollider DoorTrigger;
    public Animator EndLineAnimator;

    public RagdollManager RagdollManager;

    public GameObject BlackScreen;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        LevelClearMssg.SetActive(false);
        EndLine.isTrigger = true;
        DoorTrigger.isTrigger = true;
        StartCoroutine(FadeBlackOut());
    }

    IEnumerator GameFinished()
    {
        LevelClearMssg.SetActive(true);
        yield return new WaitForSeconds(2);
        RagdollManager.GiveControls = false;
    }

    public void TriggerEventForDoor()
    {
        EndLineAnimator.SetTrigger("OpenDoors");
    }

    public void TriggerEventForEndLine()
    {
        StartCoroutine(GameFinished());
    }


    IEnumerator FadeBlackOut()
    {
        yield return new WaitForSeconds(4f);
        BlackScreen.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        AIList.Instance.GiveControlsToAll();
    }
}