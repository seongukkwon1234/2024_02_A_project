using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;
    public Vector3 lastPosition;
    public float moveThreshold = 0.1f;
    public ConstructibleBuilding currentNearbyBuilding;
    public BuildingCrafter currentBuildingCrafter;  //�߰�

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        CheckForBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(lastPosition,transform.position) > moveThreshold)
        {
            CheckForBuilding();
            lastPosition = transform.position;
        }

        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());
        }
        else if (currentBuildingCrafter != null)
        {
            Debug.Log($"{currentNearbyBuilding.buildingName} �� ���� �޴� ����");
            CraftingUIManager.Instance?.ShowUI(currentBuildingCrafter);

        }
    }
    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);  //���� ���� ���� ��� �ݶ��̴��� ã�ƿ� ,

        float closestDistance = float.MaxValue;  //���� ����� �Ÿ��� �ʱⰪ
        ConstructibleBuilding closestBuilding = null;  //���� ����� �ǹ� �ʱⰪ
        BuildingCrafter closesCrafter = null;

        foreach (Collider collider in hitColliders) //�� �ݶ��̴��� �˻��Ͽ� ���� ������ �ǹ� ã�� 
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();
            if (building != null)
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);  //�Ÿ� ��� 
                if (distance < closestDistance) //��� �ǹ� ���� 
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closesCrafter = building.GetComponent<BuildingCrafter>();  //���⼭ ũ����Ʈ �������� 
                }
            }
        }
        if (closestBuilding != currentNearbyBuilding)  //���� ����� �ǹ��� ����Ǿ��� �� �޼��� ǥ�� 
        {
            currentNearbyBuilding = closestBuilding;   //���� ����� �ǹ� ������Ʈ
            currentBuildingCrafter = closesCrafter;
            if (currentNearbyBuilding != null)
            {
                if (FloatingTextManager.instance == null)
                {
                    Vector3 textPosition = transform.position + Vector3.up * 0.5f;  //������ ��ġ���� �ణ ���� �ؽ�Ʈ ���� 
                    FloatingTextManager.instance.Show(
                        $"[F]Ű�� {currentNearbyBuilding.buildingName} �Ǽ�(����{currentNearbyBuilding.requiredTree} �� �ʿ�)"
                        , currentNearbyBuilding.transform.position + Vector3.up
                        );
                }
            }
        }
    }
}
