using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Char{

    //A combat unit

   protected override int defaultHealth { get { return 150; } }
   public State state;

    public Vector3 dest;
    public GameObject target;

    public enum State { 
        idle,
        moving,
        attacking
    
    }



    private void Update() {
        switch (state) {
            default:
            case State.idle:
                dest = Vector3.zero;
                target = null;


                break;
            case State.moving:

                if (ai.state != moveAI.State.Moving) {
                    ai.state = moveAI.State.Moving;
                    
                }

                break;
            case State.attacking:
                break;



            }
        
        }

    }



