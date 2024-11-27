using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CraftingUIManager : MonoBehaviour
{
    public static CraftingUIManager Instance { get; private set; } //싱글톤 인스턴스 
    [Header("UI Reference")]
    public GameObject craftingPanel;  //조합 UI 패널
    public TextMeshProUGUI buildingNameText; //건물 이름 텍스트 
    public Transform recipeContainer;  //레시피 버튼이 들어갈 컨테이너 
    public Button closeButton;  //닫기 버튼 
    public GameObject recipeButtonPrefabs; //레시피 버튼 프리팹

    private BuildingCrafter currentCrafter;  //현재 선택된 건물의 제작 시스템

    private void Awake()
    {
        if (Instance == null) Instance = this;  //싱글톤 설정
        else Destroy(gameObject);

        craftingPanel.SetActive(false);
    }

    private void RefreshRecipeList()
    {
        foreach(Transform child in recipeContainer)
        {
            Destroy(child.gameObject);
        }

        if(currentCrafter != null && currentCrafter.recipes != null)
        {
            foreach(CraftingRecipe recipe in currentCrafter.recipes)
            {
                GameObject buttonObj = Instantiate(recipeButtonPrefabs, recipeContainer);
                RecipeButton recipeButton = buttonObj.GetComponent<RecipeButton>();
                recipeButton.Setup(recipe, currentCrafter);
            }
        }
    }

    public void ShowUI(BuildingCrafter crafter)
    {
        currentCrafter = crafter;
        craftingPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if(crafter != null)
        {
            buildingNameText.text = crafter.GetComponent<ConstructibleBuilding>().buildingName;
        }
    }

    public void HideUI()
    {
        craftingPanel.SetActive(false);
        currentCrafter = null;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        closeButton.onClick.AddListener(() => HideUI());
    }
}
