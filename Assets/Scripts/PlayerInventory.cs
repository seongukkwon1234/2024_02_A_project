using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats survivalStats; //Ŭ���� ����

    public int crystalCount = 0;
    public int plantCount = 0;
    public int bushCount = 0;
    public int treeCount = 0;

    //�߰� ������ ����
    public int vegetableStewCount = 0;  //��ä��Ʃ ����
    public int fruitSaladCount = 0;  //��ä��Ʃ ���� 
    public int repairKitCount = 0;  //����ŰƮ ����

    public void Start()
    {
        survivalStats = GetComponent<SurvivalStats>();
    }

    public void UseItem(ItemType itemType)
    {
        if (GetItemCount(itemType) <= 0)
        {
            return;
        }
        switch (itemType)
        {
            case ItemType.VegetableStew:
                RemoveItem(ItemType.VegetableStew, 1);
                survivalStats.EatFood(RecipeList.KitchenRecipes[0].hungerRestoreAmount);
                break;
            case ItemType.FruitSalad:
                RemoveItem(ItemType.FruitSalad, 1);
                survivalStats.EatFood(RecipeList.KitchenRecipes[0].hungerRestoreAmount);
                break;
            case ItemType.RepairKit:
                RemoveItem(ItemType.RepairKit, 1);
                survivalStats.EatFood(RecipeList.WorkbenchRecipes[0].repairAmount);
                break;

        }
    }

    public void AddItem(ItemType itemType, int amount)
    {
        for (int i = 0; 1 < amount; i++)
        {
            AddItem(itemType);
        }
    }

    public void AddItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                crystalCount++;
                Debug.Log($"ũ����Ż ȹ�� ! ���� ���� :  {crystalCount}");
                break;
            case ItemType.Plant:
                plantCount++;
                Debug.Log($"�Ĺ� ȹ�� ! ���� ���� :  {plantCount}");
                break;
            case ItemType.Bush:
                bushCount++;
                Debug.Log($"��Ǯ ȹ�� ! ���� ���� :  {bushCount}");
                break;
            case ItemType.Tree:
                treeCount++;
                Debug.Log($"���� ȹ�� ! ���� ���� :  {treeCount}");
                break;
            case ItemType.VegetableStew:
                vegetableStewCount ++;
                Debug.Log($"��ä��Ʃ ȹ�� ! ���� ���� :  {vegetableStewCount}");
                break;
            case ItemType.FruitSalad:
                fruitSaladCount++;
                Debug.Log($"���ϻ����� ȹ�� ! ���� ���� :  {fruitSaladCount}");
                break;
            case ItemType.RepairKit:
                fruitSaladCount++;
                Debug.Log($"����ŰƮ ȹ�� ! ���� ���� :  {fruitSaladCount}");
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("=======�κ��丮=======");
        Debug.Log($"ũ����Ż:{crystalCount}��");
        Debug.Log($"�Ĺ�:{plantCount}��");
        Debug.Log($"��Ǯ:{bushCount}��");
        Debug.Log($"����:{treeCount}��");
        Debug.Log("=====================");


    }

    public bool RemoveItem(ItemType itemType, int amount = 1)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                if (crystalCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($"ũ����Ż {amount}��� ! ���� ���� :  {crystalCount}");
                    return true;
                }
                break;

            case ItemType.Plant:
                if (plantCount >= amount)
                {
                    plantCount -= amount;
                    Debug.Log($"�Ĺ� {amount}��� ! ���� ���� :  {plantCount}");
                    return true;
                }
                break;

            case ItemType.Bush:
                if (bushCount >= amount)
                {
                    bushCount -= amount;
                    Debug.Log($"��Ǯ {amount}��� ! ���� ���� :  {bushCount}");
                    return true;
                }
                break;
            case ItemType.Tree:
                if (treeCount >= amount)
                {
                    treeCount -= amount;
                    Debug.Log($"���� {amount}��� ! ���� ���� :  {treeCount}");
                    return true;
                }
                break;
            case ItemType.VegetableStew:
                if (vegetableStewCount >= amount)
                {
                    vegetableStewCount -= amount;
                    Debug.Log($"��ä��Ʃ {amount}��� ! ���� ���� :  {vegetableStewCount}");
                    return true;
                }
                break;
            case ItemType.FruitSalad:
                if (fruitSaladCount >= amount)
                {
                    fruitSaladCount -= amount;
                    Debug.Log($"���ϻ����� {amount}��� ! ���� ���� :  {fruitSaladCount}");
                    return true;
                }
                break;
            case ItemType.RepairKit:
                if (repairKitCount >= amount)
                {
                    repairKitCount -= amount;
                    Debug.Log($"����ŰƮ {amount}��� ! ���� ���� :  {repairKitCount}");
                    return true;
                }
                break;
        }
        return false;
    }
    public int GetItemCount(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                return crystalCount;
            case ItemType.Plant:
                return plantCount;
            case ItemType.Bush:
                return bushCount;
            case ItemType.Tree:
                return treeCount;
            case ItemType.VegetableStew:
                return vegetableStewCount;
            case ItemType.FruitSalad:
                return fruitSaladCount;
            case ItemType.RepairKit:
                return repairKitCount;
            default:
                return 0;
        }
    }
}
   
