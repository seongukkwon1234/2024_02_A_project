using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Crystal,
    Plant,
    Bush,
    Tree,
    VegetableStew, //야채스튜
    FruitSalad, //과일 셀러드 
    RepairKit  //수리키트
}

public class ItemDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;
    public Vector3 lastPosition;
    public float moveThreshold = 0.1f;
    public CollectibleItem currentNearbyItem;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        CheckForItems();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForItems();
            lastPosition = transform.position;
        }

        if(currentNearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectItem(GetComponent<PlayerInventory>());
        }
        
    }

    private void CheckForItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;
        CollectibleItem closestItem = null;

        foreach(Collider collider in hitColliders)
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem>();
            if(item != null && item.canCollect)
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }
        if(closestItem != currentNearbyItem)
        {
            currentNearbyItem = closestItem;
            if(currentNearbyItem != null)
            {
                Debug.Log($"[E] 키를 눌러 {currentNearbyItem.itemName} 수집");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, checkRadius);
    }
}
