using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSound : MonoBehaviour
{
    public static GameSound Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
