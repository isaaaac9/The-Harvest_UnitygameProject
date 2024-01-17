using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/item Index")]
public class ItemIndex : ScriptableObject
{
    public List<ItemData> items;

    public ItemData GetItemFromstring(string name)
    {
        return items.Find(i => i.name == name);
    }
}
