using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Seed")]

public class SeedData : ItemData
{
    // Time the seed type takes to grow into a crop
    public int daysToGrow;

    // The crop the seed will yield
    public ItemData cropToYield;
}
