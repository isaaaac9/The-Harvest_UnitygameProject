using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTimeStamp
{
    public int minute;
    public int second;

    public GameTimeStamp(int minute, int second)
    {
        this.minute = minute;
        this.second = second;
    }
    
    //new Instance of a GameTimeStamp from another pre-exiting one 
    public GameTimeStamp(GameTimeStamp timestamp)
    {
        this.minute = timestamp.minute;
        this.second = timestamp.second;
    }

    //Time Update 
    public void UpdateTime()        
    {
        second++;
        if(second >= 60)
        {
            second = 0;
            minute++;
        }
    }

    //Convert Minute to seconds 
    public static int MinutetoSecond(int minute)
    {
        return minute * 1;     //60 secs - 1 minute
    }


    // Calculate the differance between 2 timestamps 
    public static int CompareTimestamps(GameTimeStamp timestamp1, GameTimeStamp timestamp2 )
    {
        int timestamp1Mins = MinutetoSecond(timestamp1.minute);
        int timestamp2Mins = MinutetoSecond(timestamp2.minute);
        int differance = timestamp2Mins - timestamp1Mins;
        return Mathf.Abs(differance);


      
    }

}
