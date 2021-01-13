using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    #region variables

    public static SelectionManager instance; 
    //float
    public float boxWidth;
    public float boxHeight;
    public float boxTop;
    public float boxLeft;
    //vector2
    public Vector2 boxStart;
    public Vector2 boxFinish;
    public Vector2 mouseDragStartPosition;
    //vector3
    public Vector3 currentMousePoint;
    public Vector3 mouseDownPoint;
    //gui
    public GUIStyle mouseDragSkin;
    //list and arrays
    //public List<GameObject> currentlySelectedUnits = new List<GameObject>();
    //bool
    public bool mouseDragging;
    //gameobjects
    public GameObject selectedUnit;

    SelectedDictionary selectedTable;


    //FSM 
    public enum SelectFSM
    {
        clickOrDrag, 
        clickSelect,
        clickDeselect     
    }
    public SelectFSM selectFSM; 

    #endregion 
    private void Awake()
    {
        instance = this;
        selectedTable = GetComponent<SelectedDictionary>();
    }

    private void Update()
    {
        SelectUnitsFSM();
    }

    private void OnGUI()
    {
        if (mouseDragging)
            GUI.Box(new Rect(boxLeft, boxTop, boxWidth, boxHeight), "", mouseDragSkin);
    }

    private void SelectUnitsFSM()
    {
        switch(selectFSM)
        {
            case SelectFSM.clickOrDrag:
                ClickOrDrag(); 
                break;
            case SelectFSM.clickSelect:
                SelectSingleUnit(); 
                break;
            
        }
    }

    #region helper functions

    private void ClickOrDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) &&/** !EventSystem.current.IsPointerOverGameObject()  && **/ !UnitManager.instance.buildMode) {
            currentMousePoint = hit.point;
            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
            {
                mouseDownPoint = hit.point;
 
                mouseDragStartPosition = Input.mousePosition;

                //click to select unit, or click the ground to deselect all
                if (hit.collider.gameObject.tag == "Unit")
                {
                    selectedUnit = hit.collider.gameObject; 
                    selectFSM = SelectFSM.clickSelect; 
                }
                else if (hit.collider.gameObject.tag == "Ground")
                    selectedTable.deselectAll();
                //selectFSM = SelectFSM.clickDeselect;
            }
            else if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
            {
                //holding shift, click to select units or click selected units to deselect
                if (hit.collider.gameObject.tag == "Unit" && !selectedTable.getDictionary().ContainsKey(hit.collider.gameObject.GetInstanceID())){

                    //AddToCurrentlySelectedUnits(hit.collider.gameObject);
                    selectedTable.addSelected(hit.collider.gameObject);


                }else if (hit.collider.gameObject.tag == "Unit" && selectedTable.getDictionary().ContainsKey(hit.collider.gameObject.GetInstanceID()))

                    //RemoveFromCurrentlySelectedUnits(hit.collider.gameObject);
                    selectedTable.deselect(hit.collider.gameObject.GetInstanceID());
            }
            else if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift))
            {
                if (UserDraggingByPosition(mouseDragStartPosition, Input.mousePosition))
                {         
                    mouseDragging = true;
                    DrawDragBox();
                    SelectUnitsInDrag();
                }
            }
            else if (Input.GetMouseButtonUp(0) && !Input.GetKey(KeyCode.LeftShift))
            {
                mouseDragging = false;
            }
        }
    }

    private void SelectSingleUnit()
    {
        if(selectedUnit != null)
        {
            if (selectedTable.getDictionary().Keys.Count > 0)
            {
                // foreach (KeyValuePair<int, GameObject> entry in selectedTable.getDictionary() )
                //{
                //currentlySelectedUnits[i].transform.Find("SelectionCircle").gameObject.SetActive(false);


                //RemoveFromCurrentlySelectedUnits(currentlySelectedUnits[i]);
                // selectedTable.deselect(entry.Key);
                //}
                selectedTable.deselectAll();

            }
            else if (selectedTable.getDictionary().Keys.Count == 0)
            {

                //AddToCurrentlySelectedUnits(selectedUnit);
                selectedTable.addSelected(selectedUnit);
                selectFSM = SelectFSM.clickOrDrag; 
            }
        }
        else
        {
            Debug.Log("Whaaat?"); 
        }
    }

    private void DrawDragBox()
    {
        boxWidth = Camera.main.WorldToScreenPoint(mouseDownPoint).x - Camera.main.WorldToScreenPoint(currentMousePoint).x;
        boxHeight = Camera.main.WorldToScreenPoint(mouseDownPoint).y - Camera.main.WorldToScreenPoint(currentMousePoint).y;
        boxLeft = Input.mousePosition.x; 
        boxTop = (Screen.height - Input.mousePosition.y) - boxHeight; //need to invert y as GUI space has 0,0 at top left, but Screen space has 0,0 at bottom left. x is the same. 

        if (boxWidth > 0 && boxHeight < 0f)
            boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        else if (boxWidth > 0 && boxHeight > 0f)
            boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + boxHeight);
        else if (boxWidth < 0 && boxHeight < 0f)
            boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y);
        else if (boxWidth < 0 && boxHeight > 0f)
            boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y + boxHeight);

        boxFinish = new Vector2(boxStart.x + Mathf.Abs(boxWidth), boxStart.y - Mathf.Abs(boxHeight));
    }

    private bool UserDraggingByPosition(Vector2 dragStartPoint, Vector2 newPoint)
    {
        if ((newPoint.x > dragStartPoint.x || newPoint.x < dragStartPoint.x) || (newPoint.y > dragStartPoint.y || newPoint.y < dragStartPoint.y))
            return true;
        else
            return false;
    }

    private void SelectUnitsInDrag()
    {
        for (int i = 0; i < UnitManager.instance.units.Count; i++)
        {
            
                Vector2 unitScreenPosition = Camera.main.WorldToScreenPoint(UnitManager.instance.units[i].transform.position);

                if (unitScreenPosition.x < boxFinish.x && unitScreenPosition.y > boxFinish.y && unitScreenPosition.x > boxStart.x && unitScreenPosition.y < boxStart.y) {
                    //AddToCurrentlySelectedUnits(UnitManager.instance.units[i]);
                    selectedTable.addSelected(UnitManager.instance.units[i]);
                }
                else {
                    //RemoveFromCurrentlySelectedUnits(UnitManager.instance.units[i]);
                    selectedTable.deselect(UnitManager.instance.units[i].GetInstanceID());
                }
            
        }
    }

    /*  private void AddToCurrentlySelectedUnits(GameObject unitToAdd)
      {
          if(!currentlySelectedUnits.Contains(unitToAdd))
          {
              selectedTable.addSelected(unitToAdd);
              currentlySelectedUnits.Add(unitToAdd);

              //unitToAdd.transform.Find("SelectionCircle").gameObject.SetActive(true);
         }
      }

      private void RemoveFromCurrentlySelectedUnits(GameObject unitToRemove)
      {
          if(currentlySelectedUnits.Count > 0)
          {
              //unitToRemove.transform.Find("SelectionCircle").gameObject.SetActive(false);
              selectedTable.deselect(unitToRemove.GetInstanceID());
              currentlySelectedUnits.Remove(unitToRemove);

          }
      } 

      private void DeselectAll()
      {
          if(currentlySelectedUnits.Count > 0)
          {
              for(int i = 0; i < currentlySelectedUnits.Count; i++)
              {
                  //currentlySelectedUnits[i].transform.Find("SelectionCircle").gameObject.SetActive(false);
                  currentlySelectedUnits.Remove(currentlySelectedUnits[i]);
              }
          }
          else if(currentlySelectedUnits.Count == 0)
          {
              selectFSM = SelectFSM.clickOrDrag; 
          }
      }
      */
    #endregion

}
