using AlignedGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructibleBuilding : MonoBehaviour
{
    [Header("Building Settings")]
    public BuildingType buildingType;
    public string buildingName;
    public int requiredTree = 5;
    public float constructionTime = 2.0f;

    public bool canBuild = true;
    public bool isConstructed = false;

    private Material buildingMaterial;
    
    void Start()
    {
        buildingMaterial = GetComponent<MeshRenderer>().material;
        Color color = buildingMaterial.color;
        color.a = 0.5f;
        buildingMaterial.color = color;
    }

    private IEnumerator ConstructionRoutine()
    {
        canBuild = false;
        float timer = 0;
        Color color = buildingMaterial.color;

        while(timer < constructionTime)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0.5f, 1f, timer / constructionTime);
            buildingMaterial.color = color;
            yield return null;
        }
        isConstructed = true;

        if(FloatingTextManager.instance != null)
        {
            FloatingTextManager.instance.Show($"{buildingName} �Ǽ� �Ϸ� !", transform.position + Vector3.up);
        }
    }

    public void StartConstruction(PlayerInventory inventory)
    {
        if (!canBuild || isConstructed)
            return;
        
        if(inventory.treeCount >= requiredTree)
        {
            inventory.RemoveItem(ItemType.Tree, requiredTree);
            if(FloatingTextManager.instance != null)
            {
                FloatingTextManager.instance.Show($"{buildingName} �Ǽ� ����!", transform.position = Vector3.up);
            }
            StartCoroutine(ConstructionRoutine());
        }
        else
        {
            if(FloatingTextManager.instance != null)
            {
                FloatingTextManager.instance.Show($"������ �����մϴ� ! ({inventory.treeCount} / {requiredTree})", transform.position + Vector3.up);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
