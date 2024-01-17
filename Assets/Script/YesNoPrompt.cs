using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class YesNoPrompt : MonoBehaviour
{
    [SerializeField]
    TMP_Text promptText;
    Action onYesSelected = null;


    public void CreatePrompt(string message, Action onYesSelected)
    {
        this.onYesSelected = onYesSelected;     //Set the action
        promptText.text = message;
    }

    public void Answer(bool yes)
    {
        if(yes && onYesSelected != null)
        {
            onYesSelected();
        }
        onYesSelected = null;       //reset Action
        gameObject.SetActive(false);
    }
}
