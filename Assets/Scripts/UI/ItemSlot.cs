using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;  //아이템 이름
    public TextMeshProUGUI countText; //아이템 개수  
    public Button useButton; //사용 버튼

    private ItemType itemType;
    private int itemCount;

    public void Setup(ItemType type, int Count)  //아이템이 생성 될 때 값들을 가져와서 활용한다
    {
        itemType = type;
        itemCount = Count;

        itemNameText.text = GetItemDisplatName(type);
        countText.text = Count.ToString();

        useButton.onClick.AddListener(UseItem);
    }
    
    private string GetItemDisplatName (ItemType type)  //아이템 슬롯에 피되는 이름 설정
    {
        switch(type)
        {
            case ItemType.VegetableStew: return "야채스튜";
            case ItemType.FruitSalad: return "과일 샐러드";
            case ItemType.RepairKit: return "수리키트";
            default: return type.ToString();
        }
    }

    private void UseItem()  //아이템 사용 함수
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();  //유저 인벤토리 참조 
        SurvivalStats stats = FindObjectOfType<SurvivalStats>();  //유저 스탯 참조

        switch(itemType)
        {
            case ItemType.VegetableStew:  //야채스튜 인 경우 
                if(inventory.RemoveItem(itemType, 1))  //인벤토리에서 아이템 1개 삭제 
                {
                    stats.EatFood(40f);  //허기 40 삭제
                }
                break;

            case ItemType.FruitSalad:  //과일 샐러드 인 경우 
                if (inventory.RemoveItem(itemType, 1))  //인벤토리에서 아이템 1개 삭제 
                {
                    stats.EatFood(50f);  //허기 40 삭제
                }
                break;

            case ItemType.RepairKit:  //수리키트 인 경우 
                if (inventory.RemoveItem(itemType, 1))  //인벤토리에서 아이템 1개 삭제 
                {
                    stats.RepairSuit(40f);  //내구도 + 25
                }
                break;
        }    
    }
 
}
