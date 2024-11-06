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
