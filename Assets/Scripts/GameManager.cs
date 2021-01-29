using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject LevelClearMssg;
    public BoxCollider EndLine;
    public BoxCollider DoorTrigger;
    public Animator EndLineAnimator;


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
    }

    IEnumerator GameFinished()
    {
        LevelClearMssg.SetActive(true);
        yield return new WaitForSeconds(2);
        RagdollManager.Instance.GiveControls = false;
    }

    public void TriggerEventForDoor()
    {
        Debug.Log("open");
        EndLineAnimator.SetTrigger("OpenDoors");
    }

    public void TriggerEventForEndLine()
    {
        StartCoroutine(GameFinished());
    }
}