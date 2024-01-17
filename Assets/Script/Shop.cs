using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : InteractableObject
{
    public List<ItemData> shopItems;

    public static void Purchase(ItemData item, int quantity)
    {
        int totalScore = item.PlayerScore * quantity;

        if(PlayerStats.Score >= totalScore)
        {
            PlayerStats.Spend(totalScore);
            ItemSlotData purchasedItem = new ItemSlotData(item,quantity);

            InventoryManager.Instance.ShopToInventory(purchasedItem);
        }
    }

    public override void Pickup()
    {
        InventoryUIManager.Instance.OpenShop(shopItems);
    }
}
 