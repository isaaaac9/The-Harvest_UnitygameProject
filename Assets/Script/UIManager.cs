using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    public void Startgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void ExitGame()
    {
        Application.ExternalEval("document.location.href='https://theharvestweb.000webhostapp.com/LeaderBoard.php'");
        //Debug.Log("Quit Game");
    }

   
}
