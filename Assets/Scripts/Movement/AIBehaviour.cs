using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{

    public float weight = 1.0f;

    public GameObject target;
    //protected StateAI agent;
    protected MovementControl agent;
    public Vector3 dest;

    public float maxSpeed = 50.0f;
    public float maxAccel = 50.0f;
    public float maxRotation = 5.0f;
    public float maxAngularAccel = 5.0f;

    public virtual void Start() {


        agent = gameObject.GetComponent<MovementControl>();
        //agent = gameObject.GetComponent<StateAI>();


    }

    public virtual void Update() {

        //print(GetSteering().linear);
        agent.setSteering(GetSteering(), weight);

    
    }

    public float MapToRange(float rotation) {

        rotation %= 360.0f;
        if (Mathf.Abs(rotation) > 180.0f) {

            if (rotation < 0.0f) {

                rotation += 360.0f;

            }else {

                rotation -= 360.0f;


            }
        
        
        }

        return rotation;
    }


    public virtual Steering GetSteering() {


        return new Steering();
    
    }

    public void stop() {
        agent.stop();
    }

}
