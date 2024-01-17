using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryUIManager : MonoBehaviour, TimeTracker
{
    public static InventoryUIManager Instance { get; private set; }


    Land selectedLand = null;


    [Header("Status Bar")]
    [Header("Tool")]
    public Image toolEquipSlot;             //Tool equip slot on the status bar
    public TMP_Text toolQuantityText;       //Tool Quantity text on the status bar 

    [Header("Item")]
    //Time UI
    public TMP_Text timeText;

    [Header("Yes No Prompt")]
    public YesNoPrompt yesNoPrompt;


    [Header("Inventory System")]
    public GameObject inventoryPanel;       //The inventory panel
    public HandInventorySlot toolHandSlot;  //The tool equip slot UI on the Inventory panel
    public InventorySlot[] toolSlots;       //The tool slot UIs
    public HandInventorySlot itemHandSlot;  //The item equip slot UI on the Inventory panel
    public InventorySlot[] itemSlots;       //The item slot UIs

   

    [Header("Player Stats")]
    public TMP_Text ScoreText;
    public TMP_Text PlayerDisplay;


    [Header("Shop")]
    public ShoplistingManager shopListingManager;




    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }    
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        RenderInventory();
        AssignSlotToIndexes();
        RenderPlayerStats();
 
        TimeManager.Instance.RegisterTracker(this);

        if (DBManager.LoggedIn)
        {
            PlayerDisplay.text = "Player:" + DBManager.username;
        }

    }

    public void TriggerYesNoPrompt(string message, System.Action onYesCallback)
    {
        yesNoPrompt.gameObject.SetActive(true);

        yesNoPrompt.CreatePrompt(message, onYesCallback);
    }

    public void AssignSlotToIndexes()
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            toolSlots[i].AssignIndex(i);
            itemSlots[i].AssignIndex(i);
        }
    }



    //render Inventory Screen to reflect to Plater Inventory
    public void RenderInventory()
    {
        //Get the respective slots to process
        ItemSlotData[] inventoryToolslots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);
        ItemSlotData[] inventoryItemslots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);

        //Render tool selection
        RenderInventoryPanel(inventoryToolslots, toolSlots);
        
        //Render items selection
        RenderInventoryPanel(inventoryItemslots, itemSlots);

       

        toolHandSlot.Display(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool));
        itemHandSlot.Display(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Item));

        ItemData equippedTool = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Tool);

        toolQuantityText.text = "";

        //check item is on display
        if (equippedTool != null  )
        {

            //Switch to thumbnail 
            toolEquipSlot.sprite = equippedTool.thumbnail;
            toolEquipSlot.gameObject.SetActive(true);
            int quantity = InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool).quantity;
            if ( quantity > 1)
            {
                toolQuantityText.text = quantity.ToString();
            }
            return;
        }
       
        
        toolEquipSlot.gameObject.SetActive(false);


    }

    void RenderInventoryPanel(ItemSlotData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {

            uiSlots[i].Display(slots[i]);  //Display them

        }
        
    }

    public void ToggleInventoryPanel()
    {

        inventoryPanel.SetActive(inventoryPanel.activeSelf);  // if panel hidden show it
        RenderInventory();
    }

    public void ClockUpdate(GameTimeStamp timestamp)
    {
        int minutes = timestamp.minute;
        int seconds = timestamp.second;
        timeText.text = minutes + ":" + seconds;
    }

    public void RenderPlayerStats()
    {
        ScoreText.text = PlayerStats.Score + PlayerStats.CURRENCY;
       
    }

    

    public void OpenShop(List<ItemData> shopItems)
    {
        shopListingManager.gameObject.SetActive(true);
        shopListingManager.RenderShop(shopItems);
    }

  

}
