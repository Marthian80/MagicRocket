using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        ExitGame();        
    }

    void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            Debug.Log("Quit the game");
            Application.Quit();
        }
    }
}
