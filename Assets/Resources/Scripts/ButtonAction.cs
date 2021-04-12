using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    public void StartLvl()
    {
        SceneManager.LoadScene("Level");
    } 
    
}
