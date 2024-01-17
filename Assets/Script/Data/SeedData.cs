using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/Seed")]
public class SeedData : ItemData
{
    public int secToGrow;

    public ItemData cropToYield;

    public GameObject Seeding;

    [Header("Regrowable")]
    public bool regrowable;

    public int SecToRegrow;


}
