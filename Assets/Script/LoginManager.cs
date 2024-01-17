using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoginManager : MonoBehaviour
{

    public static LoginManager Instance { get; private set; }


    [SerializeField] private bool upperMode = false;

    public TMP_InputField usernamefield;
    public TMP_InputField passwordfield;
    public Button submitbutton;

    public GameObject LoginSuccess;
    public TMP_Text LoginDeniedText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        LoginSuccess.gameObject.SetActive(false);
    }

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", usernamefield.text);
        form.AddField("Password", passwordfield.text);
        WWW www = new WWW("https://theharvestweb.000webhostapp.com/Login.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            DBManager.username = usernamefield.text;
            DBManager.score = int.Parse(www.text.Split('\t')[1]);
            LoginSuccess.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            //Debug.Log(DBManager.username);
            //Debug.Log("Login score:" +DBManager.score);
            SceneManager.LoadScene(1);

        }
        else
        {
            //Debug.Log("User login failed " + www.text);
            LoginDeniedText.text = www.text;
        }

    }

    public IEnumerator SaveScore(int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", DBManager.username);
        form.AddField("saveScore", score);

        //Debug.Log(DBManager.username);

        WWW www = new WWW("https://theharvestweb.000webhostapp.com/SaveScore.php", form);
        yield return www;

        string x = www.text;

            //Debug.Log(x);
            DBManager.score = score;
            //Debug.Log(DBManager.username);
            //Debug.Log("backend Score: " + DBManager.score);

    }

  



    public void VerifyInputs()
    {
        submitbutton.interactable = (usernamefield.text.Length >= 8 && passwordfield.text.Length >= 8);
    }
}
