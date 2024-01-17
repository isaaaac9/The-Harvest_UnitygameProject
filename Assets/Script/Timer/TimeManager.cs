using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public GameObject endPanel;

    [SerializeField] private AudioSource themeSound;
    [SerializeField] private AudioSource scoreSound;


    [Header("Internal Clock")]
    [SerializeField]
    GameTimeStamp timestamp;
    public float TimeScale = 1.0f;

    private int remainingDuration;
    [Header("Game Time")]
    public int Duration;
    [SerializeField] 
    private TextMeshProUGUI uiText;

    [Header("Player Stats")]
    public TMP_Text ScoreText;
    public TMP_Text endScoreText;
    public TMP_Text FinalScoreText;
    public TMP_Text ScoreHeadText;
    public TMP_Text PlayerDisplay;
    public Image PlayerHUD;

    public int highScore = 0;


   [HideInInspector]
    public bool gameIsOver = false;


    List<TimeTracker> listeners = new List<TimeTracker>();        //List of object to inform of changes to the time

    private void Awake()
    {
        if(Instance != null  && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this; 
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        timestamp = new GameTimeStamp(0, 0);
        StartPanelActivation();
        StartCoroutine(TimeUpdate());
        Being(Duration);
        themeSound.Play();
        highScore = 0;

    }

    public IEnumerator TimeUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1 / TimeScale);
            Tick();
        }
    }

    private void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
            remainingDuration--;
            yield return new WaitForSeconds(1f);

        }
        OnEnd();
        SaveHighScore();
    }

    public void Tick()
    {
        timestamp.UpdateTime();

        //inform each of the listener of the new time stats
        foreach(TimeTracker listener in listeners)
        {
            listener.ClockUpdate(timestamp);
        }
    }
     
    //Listener
    public void RegisterTracker(TimeTracker listener)
    {
        listeners.Add(listener);

    }

    public void UnRegistertracker(TimeTracker listener)
    {
        listeners.Add(listener);
    }

    //Get timestamp
    public GameTimeStamp GetGameTimeStamp()
    {
        return new GameTimeStamp(timestamp);
    }

    public void StartPanelActivation()
    {
        endPanel.SetActive(false);

    }

    
    public void SaveHighScore()
    {
        if (PlayerStats.Score > DBManager.score)
        {
            StartCoroutine(LoginManager.Instance.SaveScore(PlayerStats.Score));
            PlayerPrefs.SetInt("HighScore: ", highScore);
            endScoreText.text = highScore.ToString("HighScore: " + PlayerStats.Score);
        }
    }


    private void OnEnd()
    {
        //print("End");
        
        EndPanelActivation();
       
    }

    public void EndPanelActivation()
    {
        

        gameIsOver = true;
        endPanel.SetActive(true);
        ScoreText.enabled = false;
        ScoreHeadText.enabled = false;
        PlayerHUD.enabled = false;
        uiText.enabled = false;
        themeSound.Pause();
        scoreSound.Play();
        PlayerDisplay.text = "";
        uiText.text = "";
        endScoreText.text = highScore.ToString("HighScore: " + DBManager.score);
        FinalScoreText.text = PlayerStats.Score.ToString("Score: " + PlayerStats.Score);
        //Debug.Log("Score: " + FinalScoreText.text);
        //Debug.Log("highScore: " + endScoreText.text);
       



    }


}
