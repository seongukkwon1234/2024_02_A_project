using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour    
{
    public int crystalCount = 0;
    public int plantCount = 0;
    public int bushCount = 0;
    public int treeCount = 0;

    public void AddItem(ItemType itemType, int amount)
    {
        for(int i = 0; 1 < amount; i++ )
        {
            AddItem(itemType);
        }
    }
   
    public void AddItem(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.Crystal:
                crystalCount++;
                Debug.Log($"Å©¸®½ºÅ» È¹µæ ! ÇöÀç °³¼ö :  {crystalCount}");
                break;
            case ItemType.Plant:
                plantCount++;
                Debug.Log($"½Ä¹° È¹µæ ! ÇöÀç °³¼ö :  {plantCount}");
                break;
            case ItemType.Bush:
                bushCount++;
                Debug.Log($"¼öÇ® È¹µæ ! ÇöÀç °³¼ö :  {bushCount}");
                break;
            case ItemType.Tree:
                treeCount++;
                Debug.Log($"³ª¹« È¹µæ ! ÇöÀç °³¼ö :  {treeCount}");
                break;
        }
    }

    public bool RemoveItem(ItemType itemType , int amount = 1)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                if(crystalCount >= amount)
                {
                    crystalCount++;
                    Debug.Log($"Å©¸®½ºÅ» È¹µæ ! ÇöÀç °³¼ö :  {crystalCount}");
                    return true;
                }
                break;
                
            case ItemType.Plant:
                if (plantCount >= amount)
                {
                    plantCount++;
                    Debug.Log($"½Ä¹° È¹µæ ! ÇöÀç °³¼ö :  {plantCount}");
                    return true;
                }          
                break;

            case ItemType.Bush:
                if (bushCount >= amount)
                {
                    bushCount++;
                    Debug.Log($"¼öÇ® È¹µæ ! ÇöÀç °³¼ö :  {bushCount}");
                    return true;
                }                    
                break;
            case ItemType.Tree:
                if (treeCount >= amount) 
                {
                    treeCount++;
                    Debug.Log($"³ª¹« È¹µæ ! ÇöÀç °³¼ö :  {treeCount}");
                    return true;
                }                   
                break;
        }
        return false;
    }
    public int GetItemCount(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.Crystal:
                return crystalCount;
            case ItemType.Plant:
                return plantCount;
            case ItemType.Bush:
                return bushCount;
            case ItemType.Tree:
                return treeCount;
            default:
                return 0;
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("=======ÀÎº¥Åä¸®=======");
        Debug.Log($"Å©¸®½ºÅ»:{crystalCount}°³");
        Debug.Log($"½Ä¹°:{plantCount}°³");
        Debug.Log($"¼öÇ®:{bushCount}°³");
        Debug.Log($"³ª¹«:{treeCount}°³");
        Debug.Log("=====================");

        
    }
}
