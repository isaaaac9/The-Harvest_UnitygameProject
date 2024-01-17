using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class RegisterManager : MonoBehaviour
{
    [SerializeField] private bool upperMode = false;

    public TMP_InputField usernamefield;
    public TMP_InputField passwordfield;
    public Button submitbutton;
    public GameObject RegisterSuccess;
    public TMP_Text RegisterDeniedText;
    private void Start()
    {
        RegisterSuccess.gameObject.SetActive(false);
    }

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", usernamefield.text);
        form.AddField("Password", passwordfield.text);
        WWW www = new WWW("https://theharvestweb.000webhostapp.com/RegisterUser.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            //Debug.Log("User created Succesfully");
            RegisterSuccess.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);

        }
        else
        {
            //Debug.Log("User failed" + www.text);
            RegisterDeniedText.text = www.text;
        }
    }

    public void VerifyInputs()
    {
        submitbutton.interactable = (usernamefield.text.Length >= 8 && passwordfield.text.Length >= 8);
    }

}
