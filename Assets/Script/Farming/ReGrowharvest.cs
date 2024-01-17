using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReGrowharvest : InteractableObject
{
    CropBehavior parentCrop;
   
    public void SetParent(CropBehavior parentCrop)
    {
        this.parentCrop = parentCrop;
    }

    public override void Pickup()
    {
        InventoryManager.Instance.EquipHandSlot(item);        //Set the player's inventory to the item
        InventoryManager.Instance.RenderHand();             //update the change i nthe scene

        parentCrop.Regrow();                                //Set the parent crop bak to seeding to regrow it 
    }
}
