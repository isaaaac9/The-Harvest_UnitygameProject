using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CropSaveState
{
    public int landID;

    public string seedToGrow;
    public CropBehavior.CropState cropState;
    public int growth;
    public int health;

    public CropSaveState(int landID, string seedToGrow, CropBehavior.CropState cropState, int growth, int health)
    {
        this.landID = landID;
        this.seedToGrow = seedToGrow;
        this.cropState = cropState;
        this.growth = growth;
        this.health = health;
    }
}
