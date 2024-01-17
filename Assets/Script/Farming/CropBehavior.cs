using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    int landID;

    SeedData seedToGrow;        //Information on what the crop will grow into



    [Header("Stages of Life")]
    //public GameObject wilted;
    public GameObject seed;
    private GameObject seeding;
    private GameObject harvestable;

    int growth;
    int maxGrowth;

    int maxHealth = GameTimeStamp.MinutetoSecond(75);
    int health;


  
    public enum CropState
    {
        Seed, Seeding, Harvestable

    }
    public CropState cropState;

    

    public void Plant(int landID, SeedData seedToGrow)
    {

        LoadCrop(landID, seedToGrow, CropState.Seed, 0, 0);
        LandManager.Instance.RegisterCrop(landID, seedToGrow, cropState, growth, health);
        

    }

    public void LoadCrop(int landID, SeedData seedToGrow, CropBehavior.CropState cropState, int growth, int health)
    {
        this.landID = landID;
        this.seedToGrow = seedToGrow;                                       //Save the seed information
        seeding = Instantiate(seedToGrow.Seeding, transform);
        ItemData cropToYield = seedToGrow.cropToYield;                      //Acces the crop item data
        harvestable = Instantiate(cropToYield.gameModel, transform);        //Instancetiate the harvestable crop 

        int minToGrow = GameTimeStamp.MinutetoSecond(seedToGrow.secToGrow);

        maxGrowth = GameTimeStamp.MinutetoSecond(minToGrow);

        this.growth = growth;
        this.health = health;
        if (seedToGrow.regrowable)
        {
            ReGrowharvest regrowableHarvest = harvestable.GetComponent<ReGrowharvest>();

            regrowableHarvest.SetParent(this);
        }
        SwitchState(cropState);
    }

    public void Grow()
   {

        growth++;
        if (health < maxHealth)
        {
            health++;
        }

        if (growth >= maxGrowth / 2 && cropState == CropState.Seed)          //Growth 50%
        {
            SwitchState(CropState.Seeding);
        }

        if( growth >= maxGrowth && cropState == CropState.Seeding)        //Growth199%
        {
            SwitchState(CropState.Harvestable);
        }

        //inform Loadmanager on the  change
        LandManager.Instance.OnCropStateChange(landID, cropState, growth, health);
    }



    //public void Wither()
    //{
    //    health--;
    //    if(health <= 0 && cropState != CropState.Seed)
    //    {
    //        SwitchState(CropState.Wilted);
    //    }
    //    //inform Loadmanager on the  change
    //    LandManager.Instance.OnCropStateChange(landID, cropState, growth, health);

    //}

    void SwitchState(CropState stateToSwitch)
    {
        //Reset everything
        seed.SetActive(false);
        seeding.SetActive(false);
        harvestable.SetActive(false);

        //wilted.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Seed:
                seed.SetActive(true);
                break;
            case CropState.Seeding:
                seeding.SetActive(true);
                health = maxHealth;
                break;
            case CropState.Harvestable:
                harvestable.SetActive(true);
                if (!seedToGrow.regrowable)
                {
                    harvestable.transform.parent = null;
                    harvestable.GetComponent<InteractableObject>().onInteract.AddListener(RemoveCrop);
                }
                break;
            //case CropState.Wilted:
            //    wilted.SetActive(true);
            //    break; 
        }
            
        cropState = stateToSwitch;
    }

    public void RemoveCrop()
    { 
        LandManager.Instance.DeregisterCrop(landID);
        Destroy(gameObject);
    }

    public void Regrow()
    {
        int secToRegrow = GameTimeStamp.MinutetoSecond(seedToGrow.SecToRegrow);                 //Reset the growth
        growth = maxGrowth - GameTimeStamp.MinutetoSecond(secToRegrow);                         //Get the regrowth time in hour

        SwitchState(CropState.Seeding);                                                         //Switch the state back to seeding
    }

    
}
