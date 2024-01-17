using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InteractableObject : MonoBehaviour
{
    public ItemData item;
    public UnityEvent onInteract = new UnityEvent();



 


    public virtual void Pickup()
    {
        onInteract?.Invoke();
        InventoryManager.Instance.EquipHandSlot(item);        //Set the player's inventory to the item
        InventoryManager.Instance.RenderHand();             //update the change i nthe scene
        Destroy(gameObject);
    }
}
