﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour{

    

    public float maxSpeed = 10.0f;
    public float trueMaxSpeed;
    public float maxAccel = 30.0f;

    public float orientation;
    public float rotation;
    public Vector3 velocity;
    protected Steering steering;

    public float maxRotation = 45.0f;
    public float maxAngularAccel = 45.0f;

    private void Start() {

        velocity = Vector3.zero;
        steering = new Steering();
        trueMaxSpeed = maxSpeed;
    }

    public void setSteering(Steering steering, float weight) {

        this.steering.linear += (weight * steering.linear);
        this.steering.angular += (weight * steering.angular);

        
    }

    public void Update() {
        
        Vector3 displacement = velocity * Time.deltaTime;
        displacement.y = 0;

        orientation += rotation * Time.deltaTime;


        if (orientation < 0.0f) {

            orientation += 360.0f;


        }
        else if (orientation > 360.0f) {

            orientation -= 360.0f;

        }

        transform.Translate(displacement, Space.World);
        transform.rotation = new Quaternion();
        transform.Rotate(Vector3.up, orientation);
    }

    public virtual void LateUpdate() {

        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;

        if (velocity.magnitude > maxSpeed) {

            velocity.Normalize();
            velocity = velocity * maxSpeed;

        }

        if (steering.linear.magnitude == 0.0f) {

            velocity = Vector3.zero;

        }
        steering = new Steering();
    }

    public void speedReset() {

        maxSpeed = trueMaxSpeed;

    }

    public void stop() {
        velocity = Vector3.zero;
    }
}
