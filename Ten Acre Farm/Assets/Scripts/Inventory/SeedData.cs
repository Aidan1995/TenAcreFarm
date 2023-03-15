using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Seed")]

public class SeedData : ItemData
{
    public int daysToGrow;

    public ItemData cropToYield;
}
