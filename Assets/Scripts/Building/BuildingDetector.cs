using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;
    public Vector3 lastPosition;
    public float moveThreshold = 0.1f;
    public ConstructibleBuilding currentNearbyBuilding;
    public BuildingCrafter currentBuildingCrafter;  //추가

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
            Debug.Log($"{currentNearbyBuilding.buildingName} 의 제작 메뉴 열기");
            CraftingUIManager.Instance?.ShowUI(currentBuildingCrafter);

        }
    }
    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);  //감지 범위 내에 모든 콜라이더를 찾아옴 ,

        float closestDistance = float.MaxValue;  //가장 가까운 거리의 초기값
        ConstructibleBuilding closestBuilding = null;  //가장 가까운 건물 초기값
        BuildingCrafter closesCrafter = null;

        foreach (Collider collider in hitColliders) //각 콜라이더를 검사하여 수집 가능한 건물 찾음 
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();
            if (building != null)
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);  //거리 계산 
                if (distance < closestDistance) //모든 건물 감지 
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closesCrafter = building.GetComponent<BuildingCrafter>();  //여기서 크래프트 가져오기 
                }
            }
        }
        if (closestBuilding != currentNearbyBuilding)  //가장 가까운 건물이 변경되었을 때 메세지 표시 
        {
            currentNearbyBuilding = closestBuilding;   //가장 가까운 건물 업데이트
            currentBuildingCrafter = closesCrafter;
            if (currentNearbyBuilding != null)
            {
                if (FloatingTextManager.instance == null)
                {
                    Vector3 textPosition = transform.position + Vector3.up * 0.5f;  //아이템 위치보다 약간 위에 텍스트 생성 
                    FloatingTextManager.instance.Show(
                        $"[F]키로 {currentNearbyBuilding.buildingName} 건설(나무{currentNearbyBuilding.requiredTree} 개 필요)"
                        , currentNearbyBuilding.transform.position + Vector3.up
                        );
                }
            }
        }
    }
}
