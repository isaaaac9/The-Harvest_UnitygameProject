using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LandManager : MonoBehaviour
{
    public static LandManager Instance { get; private set; }

    public static Tuple<List<LandSaveState>, List<CropSaveState>> farmData = null;

    List<Land> landPlots = new List<Land>();

    List<LandSaveState> landData = new List<LandSaveState>();
    List<CropSaveState> cropData = new List<CropSaveState>();



    private void Awake()
    {
        //If there is more than one instance, destroy the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Set the static instance to this instance
            Instance = this;
        }
    }

    void OnEnable()
    {
        RegisterLandPlots();
        StartCoroutine(LoadFarmData());
        
    }

    IEnumerator LoadFarmData()
    {
        yield return new WaitForEndOfFrame();

    }


    //Get all land object in the scene
    void RegisterLandPlots()
    {
        foreach(Transform landTransform in transform)
        {
            Land land = landTransform.GetComponent<Land>();

            landPlots.Add(land);

            //Create a corresonding LandSaveSate
            landData.Add(new LandSaveState());
            land.id = landPlots.Count - 1;
        }
    }

    #region register and Deregister
    public void RegisterCrop(int landID, SeedData seedToGrow, CropBehavior.CropState cropState, int growth, int health)
    {
        cropData.Add(new CropSaveState(landID, seedToGrow.name, cropState, growth, health));
    }

    public void DeregisterCrop(int landID)
    {
        cropData.RemoveAll(x => x.landID == landID);
    }
    #endregion

    #region State changes
    //Update the corresponding Land Data on ever change to the Land's state
    public void OnlandStateChange(int id, Land.LandStatus landStatus, GameTimeStamp lastWaterod)
    {
        landData[id] = new LandSaveState(landStatus, lastWaterod);
    }

    //Update the corresponding Crop Data on ever change to the Land's state
    public void OnCropStateChange(int landID, CropBehavior.CropState cropState, int growth, int health)
    {
        int cropIndex =  cropData.FindIndex(x => x.landID == landID);

        string seedToGrow = cropData[cropIndex].seedToGrow;
        cropData[cropIndex] = new CropSaveState(landID, seedToGrow, cropState, growth, health);

    }
    #endregion

    #region Loading Data
    public void ImportLandData(List<LandSaveState> landDatasetToLoad)
    {
        for(int i = 0; i< landDatasetToLoad.Count; i++)
        {
            LandSaveState landDataToLoad = landDatasetToLoad[i];
            landPlots[i].LoadLandData(landDataToLoad.landStatus, landDataToLoad.lastWaterod);
        }

        landData = landDatasetToLoad;
    }

    public void ImportCropData(List<CropSaveState> cropDatasetToLoad)
    {
        cropData = cropDatasetToLoad;
        foreach (CropSaveState cropSave in cropDatasetToLoad)
        {
            Land landToPlant = landPlots[cropSave.landID];  

            CropBehavior cropToPlant = landToPlant.SpawnCrop();

            SeedData seedToGrow = (SeedData) InventoryManager.Instance.itemIndex.GetItemFromstring(cropSave.seedToGrow);
            cropToPlant.LoadCrop(cropSave.landID, seedToGrow, cropSave.cropState, cropSave.growth, cropSave.health);
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
}
