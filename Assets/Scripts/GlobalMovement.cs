using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMovement : MonoBehaviour {
    private SelectedDictionary selectedTable;
    private Dictionary<int, GameObject> selected;

    


    // Start is called before the first frame update
    void Start() {
        selectedTable = GetComponent<SelectedDictionary>();
        selected = selectedTable.getDictionary();
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButton(1)) {
            foreach (KeyValuePair<int, GameObject> pair in selected) {


                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                //If it hits something
                if (Physics.Raycast(ray, out hit, 50000.0f, 1<<8)) {
                    

                    GameObject unit = pair.Value;
                    AIBehaviour behaviour = unit.GetComponent<AIBehaviour>();
                    StateAI ai = unit.GetComponent<StateAI>();
                    ai.movement = unit.GetComponent<MovementControl>();

                    //behaviour.dest = Vector3.zero;
                    //behaviour.target = null;

                    //If it hits a node
                    if (hit.collider.gameObject.tag == "Node") {

                        //Debug.Log("Node");
                        ai.state = StateAI.State.Moving;
                        ai.order = StateAI.Order.Gather;
                        behaviour.target = hit.collider.gameObject;
                        


                    }else {

                        
                        ai.order = StateAI.Order.Move;

                        behaviour.dest = hit.point;
                       
                        //ai.dest = hit.point;

                        //

                        // movement.velocity = Vector3.zero;
                        ai.state = StateAI.State.Moving;

                    }

                   



                }

                

            }


        }
    }
}
