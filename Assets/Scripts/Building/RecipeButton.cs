using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI recipeName;  //������ �̸� 
    public TextMeshProUGUI materialsText;  //�ʿ���� �ؽ�Ʈ 
    public Button craftingButton;  //���� ��ư

    private CraftingRecipe recipe;  //������ ������ 
    private BuildingCrafter crafter;  //�ǹ��� ���� �ý���
    private PlayerInventory playerInventory;     //�÷��̾� �κ��丮 

    public void Setup(CraftingRecipe recipe , BuildingCrafter crafter)  //��ư �¾� �Լ�
    {
        this.recipe = recipe;
        this.crafter = crafter;
        playerInventory = FindObjectOfType<PlayerInventory>();

        recipeName.text = recipe.itemName;  //������ ����ǥ��
        UpdateMaterialText();

        craftingButton.onClick.AddListener(OnCraftButtonClicked); //���� ��ư�� �̺�Ʈ ����
    }
    private void UpdateMaterialText()  //��� ���� ������Ʈ �Լ�
    {
        string materials = "�ʿ� ��� : \n";
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
