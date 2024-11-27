using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCrafter : MonoBehaviour
{
    public BuildingType buildingType;  //건물 타입
    public CraftingRecipe[] recipes;  //사용 가능 레시피 배열
    private SurvivalStats survivalStats;  //생존 스택 참조
    private ConstructibleBuilding building; //선물 상태 참조
    
    void Start()
    {
        survivalStats = FindObjectOfType<SurvivalStats>();
        building = GetComponent<ConstructibleBuilding>();

        switch (buildingType)  //건물 타비에 따라 레시피 설정
        {
            case BuildingType.Kitchen:
                recipes = RecipeList.KitchenRecipes;
                break;
            case BuildingType.CraftingTable:
                recipes = RecipeList.WorkbenchRecipes;
                break;
        }
    }

    public void TryCrafting(CraftingRecipe recipe, PlayerInventory inventory)  //아이템 제작 지도
    {
        if (!building.isConstructed)  //건물이 건설 완료 되지 않았다면 제작 불가 
        {
            FloatingTextManager.instance?.Show("건설이 완료 되지 않았습니다.", transform.position + Vector3.up);
            return;
        }

        for(int i= 0; i < recipe.requiredItems.Length; i++)  //재료 소비
        {
            if (inventory.GetItemCount(recipe.requiredItems[i]) < recipe.requiredAmounts[i])
            {
                FloatingTextManager.instance?.Show("재료가 부족합니다.", transform.position + Vector3.up);
                return;
            }
        }
        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            inventory.RemoveItem(recipe.requiredItems[i] , recipe.requiredAmounts[i]);
        }

        survivalStats.DamageCrafting();

        inventory.AddItem(recipe.resultItem, recipe.resultAmount);
        FloatingTextManager.instance?.Show($"{recipe.itemName}", transform.position + Vector3.up);
    }

   
    void Update()
    {
        
    }
}
