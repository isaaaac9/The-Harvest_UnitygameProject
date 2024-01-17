using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats 
{

    public static int Score { get; private set; }
    

    public const string CURRENCY = " Points";


    public static void Spend(int playerScore)
    {
        if(playerScore > Score)
        {
            return;
        }
        Score -= playerScore;
        InventoryUIManager.Instance.RenderPlayerStats();
    }

    public static void Earn(int income)
    {
        Score += income;
        InventoryUIManager.Instance.RenderPlayerStats();

    }

    

    


}
