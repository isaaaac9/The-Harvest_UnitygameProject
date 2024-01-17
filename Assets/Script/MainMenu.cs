using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource themeSound;

    public TMP_Text PlayerDisplay;
    private void Start()
    {
        themeSound.Play();
        if(DBManager.LoggedIn)
        {
            PlayerDisplay.text = "Player:" + DBManager.username;
        }
    }

    public void GotoMainmenu()
    {
        SceneManager.LoadScene(1);

    }
}
