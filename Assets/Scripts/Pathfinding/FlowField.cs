using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField {

    public Cell[,] grid { get; private set; }
    public Vector2Int gridSize { get; private set; }
    public float cellRadius { get; private set; }

    private float cellDiameter;

    public FlowField(float _cellRadius, Vector2Int _gridSize) {

        cellRadius = _cellRadius;
        cellDiameter = cellRadius * 2f;
        gridSize = _gridSize;
    
    
    }

    public void CreateGrid() {

        grid = new Cell[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++) {

            for (int y = 0; y < gridSize.y; y++) {


                Vector3 WorldPos = new Vector3(cellDiameter * x + cellRadius, 0, cellDiameter * y + cellRadius);
                grid[x, y] = new Cell(WorldPos, new Vector2Int(x, y));

            }
        
        
        }
    
    
    
    }

    //If adding new objects - add them here
    public void CreateCostField() {

        Vector3 cellHalfExtents = Vector3.one * cellRadius;
        int terrainMask = LayerMask.GetMask("Impassable");

        foreach (Cell curCell in grid) {

            Collider[] obstacles = Physics.OverlapBox(curCell.worldPos, cellHalfExtents, Quaternion.identity, terrainMask);

            bool costIncreased = false;

            foreach (Collider col in obstacles) {

                if (col.gameObject.layer == 10) {

                    curCell.increaseCost(255);
                    continue;

                    //Example of adding another object
                } else if (!costIncreased && col.gameObject.layer == 11) {

                    curCell.increaseCost(3);
                    costIncreased = true;
                
                }
            
            
            }
        
        
        
        }
    
    
    
    
    
    
    }

}
