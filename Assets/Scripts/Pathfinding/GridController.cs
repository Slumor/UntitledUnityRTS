using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public FlowField curFlowField;
    public GridDebug gridDebug;



    private void InitializeFlowField() {

        curFlowField = new FlowField(cellRadius, gridSize);
        curFlowField.CreateGrid();
        gridDebug.SetFlowField(curFlowField);


    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {


            InitializeFlowField();
            curFlowField.CreateCostField();

        }
    
    
    
    }

    
}
