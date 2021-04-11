using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public GameManager gm;
    private void OnTriggerEnter()
    {
        gm.GameWon();
        print("xd");
    }
}
