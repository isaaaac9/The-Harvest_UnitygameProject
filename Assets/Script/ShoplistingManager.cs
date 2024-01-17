using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShoplistingManager : MonoBehaviour
{
    public GameObject shopListing;
    public Transform listingGrid;

    ItemData itemToBuy;
    int quantity;



    [Header("Confirmation Screen")]
    public GameObject confirmationScreen;
    public Image itemthumbnail;
    public TMP_Text confirmationPrompt;
    public TMP_Text quantitytext;
    public Button confirmButton;


    public void RenderShop(List<ItemData> shopItems)
    {
        //Reset the listing if there was a previous one
        if(listingGrid.childCount > 0)
        {
            foreach(Transform child in listingGrid)
            {
                Destroy(child.gameObject);
            }
        }
        //Create a new listing for every item
        foreach(ItemData shopItem in shopItems)
        {
            //Instantiate a shop listing prefab for the item
            GameObject listingGameObject = Instantiate(shopListing, listingGrid);

            //Assign it the shop item and display the liting
            listingGameObject.GetComponent<ShopListing>().Display(shopItem);

        }
    }

    public void OpenConfirmationScreen(ItemData item )
    {
        itemToBuy = item;
        quantity = 1;
        RenderConfirmationScreen();
        itemthumbnail.sprite = item.thumbnail;

    }

    public void RenderConfirmationScreen()
    {
        confirmationScreen.SetActive(true);

        confirmationPrompt.text = $"Select {itemToBuy.name}?";

        quantitytext.text = "x" + quantity;

        confirmButton.interactable = true;

        


    }

    public void AddQuantity()
    {
        quantity++;
        if(quantity > 10)
        {
            return;
        }
        RenderConfirmationScreen();
    }

    public void SubstractQuantity()
    {
        if (quantity > 1)
        {
            quantity--;
        }
        RenderConfirmationScreen();
    }

    public void ConfirmSelect()
    {
        Shop.Purchase(itemToBuy, quantity);
        confirmationScreen.SetActive(false);
    }

    public void CancelPurchase()
    {
        confirmationScreen.SetActive(false);

    }
}

    
