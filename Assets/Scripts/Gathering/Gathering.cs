using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gathering : MonoBehaviour
{
    [SerializeField] private GoldNode goldnode;
    [SerializeField] private Depot depot;

    private Worker worker;
    private Vector3 dest;
    private moveAI moveai;

    private AIBehaviour aIBehaviour;

    private GridController gridController;
    private FlowField flowfield;



    public State state;
    public enum State {
        idle,
        gathering,
        depositing

    }


    private void Awake() {
        moveai = gameObject.GetComponent<moveAI>();
        worker = gameObject.GetComponent<Worker>();
        aIBehaviour = gameObject.GetComponent<AIBehaviour>();


        dest = goldnode.gameObject.transform.position;

    }

    //Probably want to put this somewhere better
    private void genFlowField(Vector3 desti) {

        GameObject sys = GameObject.Find("EventSystem");

        gridController = sys.GetComponent<GridController>();

        gridController.InitializeFlowField();

        flowfield = gridController.curFlowField;

        flowfield.CreateCostField();

        Cell destinationCell = flowfield.GetCellFromWorldPos(desti);

        flowfield.CreateIntegrationField(destinationCell);
        flowfield.CreateFlowField();



    }



    // Start is called before the first frame update
    void Start(){



        if (worker.carrying == 0) {

           //TODO - Refactor this

            genFlowField(dest);
            
            //Move to a gold node
            moveai.finalDest = dest;
            //worker.ai.finalDest = dest;
            moveai.curFlowField = flowfield;
            //worker.ai.curFlowField = flowfield;
            moveai.state = moveAI.State.Moving;
            //worker.state = Worker.State.moving;

            moveai.ai = aIBehaviour;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
