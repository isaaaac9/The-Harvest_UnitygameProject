using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator SaveScore(string username, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("saveScore", score);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/SaveScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

}
