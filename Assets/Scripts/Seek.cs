using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : AIBehaviour
{

    public override Steering GetSteering() {

        Steering steer = new Steering();

        //if (dest != Vector3.zero) {
            //Debug.Log(dest);
            steer.linear = dest - transform.position;
        //}else if (target != null) {
            //Debug.Log("Have a Target");
            //steer.linear = target.transform.position - transform.position;
        //}


        steer.linear.Normalize();
        steer.linear = steer.linear * agent.maxAccel;

        return steer;

    }





}
