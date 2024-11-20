using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    CraftingTable,
    Funace,
    Kitchen,
    Storage,
}

[System.Serializable]

public class CraftingRecipe
{
    public string itemName;
    public ItemType resultItem;
    public int resultAmount = 1;

    public float hungerRestoreAmount; //허기 회복량
    public float repairAmount;    //수리량 
    
    public ItemType[] requiredItems;
    public int[] requiredAmounts;
}
