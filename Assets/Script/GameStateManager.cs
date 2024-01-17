using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {

            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    private void Update()
    {
        UpdateShippingState();

    }

    public void UpdateShippingState()
    {
        
       Shipping.Shipitems();
 
    }

}
