using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LandSaveState 
{
    public Land.LandStatus landStatus;
    public GameTimeStamp lastWaterod;

    public LandSaveState(Land.LandStatus landStatus, GameTimeStamp lasWaterod)
    {
        this.landStatus = landStatus;
        this.lastWaterod = lasWaterod;
    }
}
