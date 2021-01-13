using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public FlowField curFlowField;
    public GridDebug gridDebug;

    public Cell[,] grid { get; private set; }
    private float cellDiameter;

    public void Awake() {
        cellDiameter = cellRadius * 2f;
    }


    public FlowField InitializeFlowField(Vector3 destination) {

        curFlowField = new FlowField(cellRadius, gridSize);
        CreateGrid();
        curFlowField.grid = grid;

        curFlowField.CreateCostField();

        Cell desti = curFlowField.GetCellFromWorldPos(destination);

        curFlowField.CreateIntegrationField(desti);
        curFlowField.CreateFlowField();

        return curFlowField;
        //gridDebug.SetFlowField(curFlowField);


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



}
