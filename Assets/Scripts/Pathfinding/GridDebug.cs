using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridDebug : MonoBehaviour {
    private Vector2Int gridSize;
    private float cellRadius;
    private FlowField curFlowField;
    public bool displayGrid;
    public GridController gridController;

    public void SetFlowField(FlowField newFlowField) {

        curFlowField = newFlowField;
        cellRadius = newFlowField.cellRadius;
        gridSize = newFlowField.gridSize;

    }

    private void OnDrawGizmos() {

        if (displayGrid) {
            if (curFlowField == null) {
                DrawGrid(gridController.gridSize, Color.yellow, gridController.cellRadius);
            }
            else {
                DrawGrid(gridSize, Color.green, cellRadius);
            }
        }

        if (curFlowField == null) { return; }

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;

        foreach (Cell curCell in curFlowField.grid) {

            Handles.Label(curCell.worldPos, curCell.cost.ToString(), style);
        
        }



    }
    private void DrawGrid(Vector2Int drawGridSize, Color drawColor, float drawCellRadius) {
        Gizmos.color = drawColor;
        for (int x = 0; x < drawGridSize.x; x++) {
            for (int y = 0; y < drawGridSize.y; y++) {
                Vector3 center = new Vector3(drawCellRadius * 2 * x + drawCellRadius, 0, drawCellRadius * 2 * y + drawCellRadius);
                Vector3 size = Vector3.one * drawCellRadius * 2;
                Gizmos.DrawWireCube(center, size);
            }
        }
    }


}