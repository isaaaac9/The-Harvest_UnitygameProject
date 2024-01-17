using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebManager : MonoBehaviour
{    
    IEnumerator Start()
    {
        WWW request = new WWW("https://theharvestweb.000webhostapp.com/Web.php");
        yield return request;
        string[] webResults = request.text.Split('\t');
        //Debug.Log(webResults[0]);
        int WebNumber = int.Parse(webResults[1]);
        WebNumber *= 2;
        //Debug.Log(WebNumber);
    }
}
