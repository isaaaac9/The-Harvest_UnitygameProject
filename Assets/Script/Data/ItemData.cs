using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item")]
public class ItemData : ScriptableObject
{
    public Sprite thumbnail;

    public GameObject gameModel;

    public int PlayerScore;
}
