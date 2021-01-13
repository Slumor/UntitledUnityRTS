using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAI : MonoBehaviour {

    public enum State { 
        Idle,
        Moving,
    }

    public enum Order { 
        Move,
        Gather,
        Attack,
        Build
    
    }

    public State state;
    public Order order;

    public Seek seek;

    public bool arrived;

    public AIBehaviour ai;

    public FlowField curFlowField;

    public MovementControl movement;

    public Vector3 finalDest;

    public float maxSpeed = 10.0f;

    //public int gatherRate = 2;
    //public int carryCapacity = 8;
    //Node miningTarget;

    private void Awake() {

        state = State.Idle;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        arrived = false;
       // movement = gameObject.GetComponent<MovementControl>();
        ai = gameObject.GetComponent<AIBehaviour>();
        movement = gameObject.GetComponent<MovementControl>();
   
    }

    

    // Update is called once per frame
    public virtual void Update(){
        switch (state) {
            default:
            case State.Idle:
                break;
            case State.Moving:

              

                if (gameObject.GetComponent<Seek>() == null) { 
                    seek = gameObject.AddComponent<Seek>();
                }

                if (curFlowField == null) { return; }

                Cell cellBelow = curFlowField.GetCellFromWorldPos(gameObject.transform.position);
                Vector3 moveDirection = new Vector3(cellBelow.bestDirection.vector.x, 0, cellBelow.bestDirection.vector.y);

                ai.dest = curFlowField.getNeighbour(cellBelow, cellBelow.bestDirection);
               
                 


                seek.dest = ai.dest;
                seek.target = ai.target;



               if ((gameObject.transform.position - finalDest).magnitude < 1) {
                    //Stop 
                   movement.stop();
                   Destroy(seek);
                   state = State.Idle;

                   arrived = true;



                }
                break;
        
        }
        
    }

    


    private void OnDrawGizmos() {
        UnityEditor.Handles.Label(transform.position + Vector3.up * 3, state.ToString());
    }


    //private void OnTriggerEnter(Collider other) {
      //  if (other.tag == "Node" && order == Order.Gather) {

      //      state = State.Gathering;
      //      movement.velocity = Vector3.zero;
      //      miningTarget = other.gameObject.GetComponent<Node>();

      //  }
   // }

    //IEnumerator mine() {

      //  yield return new WaitForSeconds(2);
      //  miningTarget.gather();
      //  Debug.Log(miningTarget.resources);

   // }

}
