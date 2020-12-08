using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLivingsForward : MonoBehaviour
{
    public float Movespeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.forward * Movespeed);
    }
}