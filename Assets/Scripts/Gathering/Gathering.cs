using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gathering : MonoBehaviour
{
    [SerializeField] private GoldNode goldnode;
    [SerializeField] private Depot depot;

    private Worker worker;
    private GameObject goldDest;
    private Vector3 depotDest;

    private moveAI moveai;

    private GameObject sys;


    bool Mrunning;

    bool Drunning;

    bool moving;

    private AIBehaviour aIBehaviour;

    private GridController gridController;
    


    public State state;
    public enum State {
        idle,
        gathering,
        depositing

    }


    private void Awake() {

        Drunning = false;
        Mrunning = false;
        moving = false;

        moveai = gameObject.GetComponent<moveAI>();
        worker = gameObject.GetComponent<Worker>();

        goldDest = null;

        sys = GameObject.Find("EventSystem");

        depotDest = depot.gatherPoint;

    }

    //Probably want to put this somewhere better
    private FlowField genFlowField(Vector3 desti) {

       
        gridController = sys.GetComponent<GridController>();


        FlowField flowfield = gridController.InitializeFlowField(desti);

        
        return flowfield;



    }



    // Start is called before the first frame update
    void Start(){


    }

    //Todo - Probably move this to the flowfield script and pull requests from there
    public void moveTo(Vector3 destination) {



        moving = true;

        FlowField ff = genFlowField(destination);

        //Move to a gold node
        moveai.finalDest = destination;
        //worker.ai.finalDest = dest;
        moveai.curFlowField = ff;
        //worker.ai.curFlowField = flowfield;
        moveai.state = moveAI.State.Moving;
        //worker.state = Worker.State.moving;

   




    }

    // Update is called once per frame
    void Update(){

        switch (state) {

            default:
            case State.idle:
                break;

            case State.gathering:



                if (!moving && !moveai.arrived) {

                    if (goldDest == null) {

                        goldDest = closestGoldNode();
                        goldnode = goldDest.GetComponent<GoldNode>();

                        
                        
                    
                    }
                    Vector3 goldGather = goldnode.gatherPoint;



                    moveTo(goldGather);



                }
                
                
                if (moveai.arrived) {

                    moving = false;
                    
                    if (!Mrunning) {
                        
                        StartCoroutine(mining());
                        
                    }


                    if (worker.carrying >= worker.capacity) {
                        moveai.arrived = false;
                        state = State.depositing;
                    
                    }

                }
                break;

            case State.depositing:

                if (!moving && !moveai.arrived) {
                   
                    

                    moveTo(depotDest);



                }


                if (moveai.arrived) {
                    moving = false;

                    if (!Drunning) { StartCoroutine(depositing());}
                                  
                
                }

                if (worker.carrying == 0) {
                    moveai.arrived = false;
                    state = State.gathering;
                
                }



                break;

        
        
        
        
        
        
        
        }
        
    }

    public GameObject closestGoldNode() {

        NodeTracker t = sys.GetComponent<NodeTracker>();

        List<GameObject> goldNodes = t.goldNodes;

        GameObject bestNode = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (GameObject Node in goldNodes) {

            Vector3 directionToNode = Node.transform.position - transform.position;

            float dSqrToNode = directionToNode.sqrMagnitude;

            if (dSqrToNode < closestDistanceSqr) {

                closestDistanceSqr = dSqrToNode;
                bestNode = Node;

            
            }

        
        }




        return bestNode;

    }

    IEnumerator mining() {
        Mrunning = true;
        //Print the time of when the function is first called.

        
        worker.carrying += goldnode.gather(5);
        
        
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 5 seconds print the time again.


        Mrunning = false;
    }

    IEnumerator depositing() {
        Drunning = true;

        yield return new WaitForSeconds(2);

        worker.carrying += depot.deposit(worker.carrying);
        worker.carrying -= worker.carrying;
        


        Drunning = false;
    }

}
