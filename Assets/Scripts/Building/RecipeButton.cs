using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI recipeName;  //레시피 이름 
    public TextMeshProUGUI materialsText;  //필요재료 텍스트 
    public Button craftingButton;  //제작 버튼

    private CraftingRecipe recipe;  //레시피 데이터 
    private BuildingCrafter crafter;  //건물의 제작 시스템
    private PlayerInventory playerInventory;     //플레이어 인벤토리 

    public void Setup(CraftingRecipe recipe , BuildingCrafter crafter)  //버튼 셋업 함수
    {
        this.recipe = recipe;
        this.crafter = crafter;
        playerInventory = FindObjectOfType<PlayerInventory>();

        recipeName.text = recipe.itemName;  //레시피 정보표시
        UpdateMaterialText();

        craftingButton.onClick.AddListener(OnCraftButtonClicked); //제작 버튼에 이벤트 연결
    }
    private void UpdateMaterialText()  //재료 정보 업데이트 함수
    {
        string materials = "필요 재료 : \n";
        for(int i = 0; i < recipe.requiredItems.Length; i++)
        {
            ItemType item = recipe.requiredItems[i];
            int required = recipe.requiredAmounts[i];
            int has = playerInventory.GetItemCount(item);
            materials += $"{item} : {has}/{required}\n";
        }
        materialsText.text = materials;

    }

    private void OnCraftButtonClicked()
    {
        crafter.TryCrafting(recipe, playerInventory);
        UpdateMaterialText();
    }

}
