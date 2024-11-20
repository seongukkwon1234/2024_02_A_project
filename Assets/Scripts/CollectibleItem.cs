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
            Vector3 textPosition = transform.position + Vector3.up * 0.5f;         //������ ��ġ���� �ణ ���� �ؽ�Ʈ ����
            FloatingTextManager.instance.Show($" + {itemName}", textPosition);
        }    

        Debug.Log($"{itemName} ���� �Ϸ�");
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
