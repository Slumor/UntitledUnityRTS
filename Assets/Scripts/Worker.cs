using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Char {

    protected override int defaultHealth { get { return 100; } }

    public State state;
    public enum State {
        idle,
        moving,
        gathering,
        depositing       
    }



    // Start is called before the first frame update
    void Start()
    {
        state = State.gathering;
    }

    // Update is called once per frame
    void Update(){
        switch (state) {
            default:
            case State.idle:
                break;
            case State.moving:
                break;
            case State.gathering:
                break;
            case State.depositing:
                break;


        
        
        
        
        }


        
    }
}
