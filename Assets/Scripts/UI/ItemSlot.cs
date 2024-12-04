using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;  //������ �̸�
    public TextMeshProUGUI countText; //������ ����  
    public Button useButton; //��� ��ư

    private ItemType itemType;
    private int itemCount;

    public void Setup(ItemType type, int Count)  //�������� ���� �� �� ������ �����ͼ� Ȱ���Ѵ�
    {
        itemType = type;
        itemCount = Count;

        itemNameText.text = GetItemDisplatName(type);
        countText.text = Count.ToString();

        useButton.onClick.AddListener(UseItem);
    }
    
    private string GetItemDisplatName (ItemType type)  //������ ���Կ� �ǵǴ� �̸� ����
    {
        switch(type)
        {
            case ItemType.VegetableStew: return "��ä��Ʃ";
            case ItemType.FruitSalad: return "���� ������";
            case ItemType.RepairKit: return "����ŰƮ";
            default: return type.ToString();
        }
    }

    private void UseItem()  //������ ��� �Լ�
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();  //���� �κ��丮 ���� 
        SurvivalStats stats = FindObjectOfType<SurvivalStats>();  //���� ���� ����

        switch(itemType)
        {
            case ItemType.VegetableStew:  //��ä��Ʃ �� ��� 
                if(inventory.RemoveItem(itemType, 1))  //�κ��丮���� ������ 1�� ���� 
                {
                    stats.EatFood(40f);  //��� 40 ����
                }
                break;

            case ItemType.FruitSalad:  //���� ������ �� ��� 
                if (inventory.RemoveItem(itemType, 1))  //�κ��丮���� ������ 1�� ���� 
                {
                    stats.EatFood(50f);  //��� 40 ����
                }
                break;

            case ItemType.RepairKit:  //����ŰƮ �� ��� 
                if (inventory.RemoveItem(itemType, 1))  //�κ��丮���� ������ 1�� ���� 
                {
                    stats.RepairSuit(40f);  //������ + 25
                }
                break;
        }    
    }
 
}
