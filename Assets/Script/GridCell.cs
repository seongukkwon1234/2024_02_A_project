using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell 
{
    public Vector3Int Position;
    public bool IsOccupied;
    public GameObject Building;

    public GridCell(Vector3Int position)
    {
        Position = position;
        IsOccupied = false;
        Building = null;
    }
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
