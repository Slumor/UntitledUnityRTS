﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField {

    public Cell[,] grid;
    public Vector2Int gridSize { get; private set; }
    public float cellRadius { get; private set; }

    private float cellDiameter;

    //Set this in moveclass
    public Cell destinationCell;

    public FlowField(float _cellRadius, Vector2Int _gridSize) {

        cellRadius = _cellRadius;
        cellDiameter = cellRadius * 2f;
        gridSize = _gridSize;

        //grid = _grid;
        //CreateGrid();


    }

   /* public void CreateGrid() {

        grid = new Cell[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++) {

            for (int y = 0; y < gridSize.y; y++) {


                Vector3 WorldPos = new Vector3(cellDiameter * x + cellRadius, 0, cellDiameter * y + cellRadius);
                grid[x, y] = new Cell(WorldPos, new Vector2Int(x, y));

            }
        
        
        }
    
    
    
    }*/

    //If adding new objects - add them here


    public FlowField generateField(Cell _dest){
        CreateCostField();
        CreateIntegrationField(_dest);
        CreateFlowField();

        return this;

    }





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

    public void CreateIntegrationField(Cell _destinationCell) {

        destinationCell = _destinationCell;

        destinationCell.cost = 0;
        destinationCell.bestCost = 0;

        Queue<Cell> cellsToCheck = new Queue<Cell>();

        cellsToCheck.Enqueue(destinationCell);

        while (cellsToCheck.Count > 0) {

            Cell curcell = cellsToCheck.Dequeue();
            List<Cell> curNeighbours = GetNeighbourCells(curcell.gridIndex, GridDirection.CardinalDirections);

            foreach (Cell curNeighbour in curNeighbours) {

                if (curNeighbour.cost == byte.MaxValue) { continue; }

                if (curNeighbour.cost + curcell.bestCost < curNeighbour.bestCost) {

                    curNeighbour.bestCost = (ushort)(curNeighbour.cost + curcell.bestCost);
                    cellsToCheck.Enqueue(curNeighbour);
                
                }
            
            }
        }
    
    }


    public void CreateFlowField() {

        foreach (Cell curCell in grid) {

            List<Cell> curNeighbours = GetNeighbourCells(curCell.gridIndex, GridDirection.AllDirections);

            int bestCost = curCell.bestCost;

            foreach (Cell curNeighbour in curNeighbours) {


                if (curNeighbour.bestCost < bestCost) {

                    bestCost = curNeighbour.bestCost;
                    curCell.bestDirection = GridDirection.GetDirectionFromV2I(curNeighbour.gridIndex - curCell.gridIndex);
                
                }
            
            }
        }
    
    
    }




    private List<Cell> GetNeighbourCells(Vector2Int nodeIndex, List<GridDirection> directions) {


        List<Cell> neighbourCells = new List<Cell>();

        foreach (Vector2Int curDirection in directions) {

            Cell newNeighbour = GetCellAtRelativePos(nodeIndex, curDirection);

            if (newNeighbour != null) {

                neighbourCells.Add(newNeighbour);

            }
        
        
        }

        return neighbourCells;
    
    }

    public Vector3 getNeighbour(Cell cellBelow, GridDirection dir) {

       Vector2Int index = cellBelow.gridIndex;

        Cell bestNeighbour = GetCellAtRelativePos(index, dir);

        return bestNeighbour.worldPos;
    
    
    
    }

    private Cell GetCellAtRelativePos(Vector2Int originPos, Vector2Int relativePos) {


        Vector2Int finalPos = originPos + relativePos;

        if (finalPos.x < 0 || finalPos.x >= gridSize.x || finalPos.y < 0 || finalPos.y >= gridSize.y) {

            return null;

        }


        else { return grid[finalPos.x, finalPos.y]; }
    
    
    
    }

    public Cell GetCellFromWorldPos(Vector3 worldPos) {
        float percentX = worldPos.x / (gridSize.x * cellDiameter);
        float percentY = worldPos.z / (gridSize.y * cellDiameter);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.Clamp(Mathf.FloorToInt((gridSize.x) * percentX), 0, gridSize.x - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt((gridSize.y) * percentY), 0, gridSize.y - 1);
        return grid[x, y];
    }

}
