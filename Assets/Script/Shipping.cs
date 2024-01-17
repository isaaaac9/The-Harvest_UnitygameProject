using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shipping : InteractableObject
{

    public static int secondToShip = 0;
    public static List<ItemSlotData> itemsToShip = new List<ItemSlotData>();

    [SerializeField] private AudioSource scoreSound;
    public override void Pickup()
    {
        ItemData handSlotItem = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Item);

        //check if the player is not holding anything
        if (handSlotItem == null) return;

        //open yes no prompt to confirmed
        InventoryUIManager.Instance.TriggerYesNoPrompt($"Store {handSlotItem.name}?", PlaceItemInShippingBin);
    }
    private void PlaceItemInShippingBin()
    {
        //Get the itemSlotData of the what the player is holding
        ItemSlotData handSlot = InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Item);
        //Debug.Log(handSlot.itemData);

        itemsToShip.Add(new ItemSlotData(handSlot));

        handSlot.Empty();

        InventoryUIManager.Instance.RenderInventory();

        InventoryManager.Instance.RenderHand();

        scoreSound.Play();

        foreach (ItemSlotData item in itemsToShip)
        {

           // Debug.Log($"In the shipping : {item.itemData.name} x {item.quantity}");
        }
    }

    public static void Shipitems()
    {
        int scoreToRecieve = TallyItems(itemsToShip);     //Calculate how nuch the player should recieve score 
        PlayerStats.Earn(scoreToRecieve);           //convert the items to score
        itemsToShip.Clear();        //Empty shipping bin
    }

    static int TallyItems(List<ItemSlotData> items)
    {
        int total = 0;
        foreach (ItemSlotData item in items)
        {
            total += item.quantity * item.itemData.PlayerScore;
        }
        return total;
    }
}
