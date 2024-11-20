using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemType itemType;
    public string itemName;
    public float respawnTime = 30.0f;
    public bool canCollect = true;

    public void CollectItem(PlayerInventory inventory)
    {
        if (!canCollect) return;

        inventory.AddItem(itemType);

        if(FloatingTextManager.instance != null)
        {
            Vector3 textPosition = transform.position + Vector3.up * 0.5f;         //아이템 위치보다 약간 위에 텍스트 생성
            FloatingTextManager.instance.Show($" + {itemName}", textPosition);
        }    

        Debug.Log($"{itemName} 수집 완료");
        StartCoroutine(RespawnRoutione());
    }

    private IEnumerator RespawnRoutione()
    {
        canCollect = false;
        GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(respawnTime);

        GetComponent<MeshRenderer>().enabled = true;
        canCollect = true;
    }
}
