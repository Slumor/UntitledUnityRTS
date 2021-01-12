using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Char {

    protected override int defaultHealth { get { return 100; } }

    public int carrying;
    [SerializeField] public int capacity;

    public State state;
    bool gatherScript;
    public enum State {
        idle,
        gathering,
        moving,
        deposting
         
    }



    // Start is called before the first frame update
    void Start()
    {
        carrying = 0;
        state = State.gathering;
        gatherScript = false;
       
    }

    // Update is called once per frame
    void Update(){
        switch (state) {
            default:
            case State.idle:
                break;
            case State.gathering:

                if (!gatherScript) {
                    //gameObject.AddComponent<Gathering>();
                    //gatherScript = true;
                }

                break;

            case State.moving:

                if (ai.state != moveAI.State.Moving) {
                    ai.state = moveAI.State.Moving;

                }


                break;


        
        }


        
    }
}
