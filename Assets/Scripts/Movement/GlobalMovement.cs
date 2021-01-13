using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMovement : MonoBehaviour {

    private GridController gridController;
    private SelectedDictionary selectedTable;
    private Dictionary<int, GameObject> selected;
    private FlowField flowfield;

    


    // Start is called before the first frame update
    void Start() {

        selectedTable = GetComponent<SelectedDictionary>();
        selected = selectedTable.getDictionary();

        
        
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButton(1)) {
            foreach (KeyValuePair<int, GameObject> pair in selected) {

                //Will need to change this - each unit will need to store their own flow field
                //Make a new GridController?

                //If we make a new controller gotta make sure we set the x and the y
                
                
                //gridController.gridSize.x = 50;
                //gridController.gridSize.y = 50;
                
                


                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                //If it hits something
                if (Physics.Raycast(ray, out hit, 50000.0f, 1<<8)) {

                    gridController = GetComponent<GridController>();

                    GameObject gameobj = pair.Value;
                    AIBehaviour behaviour = gameobj.GetComponent<AIBehaviour>();



                    //Flow field handling
                    

                    flowfield = gridController.InitializeFlowField(hit.point);

 
                    Unit unit = gameobj.GetComponent<Unit>();

                    ///Don't like this
                    unit.ai.finalDest = hit.point;
                    //unit.ai.gridController = gridController;
                    unit.ai.curFlowField = flowfield;

                    //behaviour.dest = Vector3.zero;
                    //behaviour.target = null;


                        
                    unit.state = Unit.State.moving;
                        //behaviour.stop();

                        //behaviour.dest = hit.point;
                       
                      
                }

                

            }


        }
    }
}
