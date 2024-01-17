using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopListing : MonoBehaviour, IPointerClickHandler
{


    public Image itemthumbnail;
    public TMP_Text nameText;

    ItemData itemData;

    public void Display(ItemData itemData)
    {
        this.itemData = itemData;
        itemthumbnail.sprite = itemData.thumbnail;
        nameText.text = itemData.name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryUIManager.Instance.shopListingManager.OpenConfirmationScreen(itemData);
    }

   
}
