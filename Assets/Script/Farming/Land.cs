using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, TimeTracker
{
    public static Land Instance { get; private set; }


    [SerializeField] public AudioSource hoeSound;
    [SerializeField] private AudioSource waterSound;

    public int id;
    public enum LandStatus
    {
        Soil, Farmland, Waterod
    }

    public LandStatus landStatus;

    public Material soilMat, farmLandMat, waterMat;
    //Selection GameObject to enabled when player is selecting Land
    public GameObject select;
 


    new Renderer renderer;

    GameTimeStamp timeWaterod;


    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();  //Get renderer component
        SwitchLandStatus(LandStatus.Soil);      //Set the land to soil by default
        Select(false);
        TimeManager.Instance.RegisterTracker(this);
    }

    

    public void LoadLandData(LandStatus statusToSwitch, GameTimeStamp lasWaterod)
    {
        landStatus = statusToSwitch;
        timeWaterod = lasWaterod;

        Material materialToSwitch = soilMat;
        switch (statusToSwitch)
        {
            case LandStatus.Soil:  //Switch to Soil Material
                materialToSwitch = soilMat;
                break;
            case LandStatus.Farmland:  //Switch to Land Material
                materialToSwitch = farmLandMat;
                break;
            case LandStatus.Waterod:  //Switch to water Material
                materialToSwitch = waterMat;
                break;
        }

        //get renderer to apply the change 
        renderer.material = materialToSwitch;

    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        landStatus = statusToSwitch;
        Material materialToSwitch = soilMat;
        switch(statusToSwitch)
        {
            case LandStatus.Soil:  //Switch to Soil Material
                materialToSwitch = soilMat;
                break;
            case LandStatus.Farmland:  //Switch to Land Material
                materialToSwitch = farmLandMat;
                break;
            case LandStatus.Waterod:  //Switch to water Material
                materialToSwitch = waterMat;
                timeWaterod = TimeManager.Instance.GetGameTimeStamp();
                break;
        }

        //get renderer to apply the change 
        renderer.material = materialToSwitch;

        LandManager.Instance.OnlandStateChange(id, landStatus, timeWaterod);
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

 


    [Header("Crops")]
    public GameObject cropPrefeb;

    GameTimeStamp timewaterod;
    CropBehavior cropPlanted = null;

    public void Interact()
    {
        ItemData toolSlot = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Tool);       //Change the player tool slot
       
        if (!InventoryManager.Instance.SlotEquipped(InventorySlot.InventoryType.Tool))
        {
            return;
        }
        
        EquipmentData equipmentTool = toolSlot as EquipmentData;        //try casting the itemData in the toolslot as EquipmentData

        

        //check if is of Type  EquipementData
        if(equipmentTool != null)
        {
            EquipmentData.ToolType toolType = equipmentTool.toolType;   //Get the tool type
            switch(toolType)
            {
                case EquipmentData.ToolType.Hoe:
                    hoeSound.Play();
                    SwitchLandStatus(LandStatus.Farmland);
                    break;
                case EquipmentData.ToolType.Watering:
                    waterSound.Play();
                    SwitchLandStatus(LandStatus.Waterod);
                    break;
                    
            }
            return;
        }

        SeedData seedTool = toolSlot as SeedData;

        //Player plant the seed 
        //1 Player holding Tool of SeedData 
        //2 The Land status must be either waterd or farmland
        //3 There isn't already a crop that has been planted
        if(seedTool != null && landStatus != LandStatus.Soil && cropPlanted == null )
        {
            SpawnCrop();
            cropPlanted.Plant(id,seedTool);
            //Debug.Log("Plant seed");
            InventoryManager.Instance.ConsumeItem(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool));
        }
    }

    public CropBehavior SpawnCrop()
    {
        GameObject cropObject = Instantiate(cropPrefeb, transform);     //Instantiate the crop Object parented to the Land 
        cropObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        cropPlanted = cropObject.GetComponent<CropBehavior>();      //Acces the CropBehavior of the crop we're going to plant
   
        
        return cropPlanted;
      
    }

    public void ClockUpdate(GameTimeStamp timestamp)
    {
        //Check if min has pass
        if(landStatus == LandStatus.Waterod)
        {
           int minsElapsed = GameTimeStamp.CompareTimestamps(timeWaterod, timestamp);
           //Debug.Log(minsElapsed + "  min - sinces this was watered");
            if(cropPlanted != null)
            {
                cropPlanted.Grow(); 
            }

            if(minsElapsed > 0 )
            {
                SwitchLandStatus(LandStatus.Farmland);
            }
        }
        //handle the wilting of the plant
        //if(landStatus != LandStatus.Waterod && cropPlanted != null)
        //{
        //    if(cropPlanted.cropState != CropBehavior.CropState.Seed)
        //    {
        //        cropPlanted.Wither();
        //    }
        //}
    }
}
