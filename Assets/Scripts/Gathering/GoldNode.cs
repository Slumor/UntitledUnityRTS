using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldNode : Node {

    
    public Vector3 gatherPoint;
    public NodeTracker nt;


    public void Start() {

        Vector3 pos = gameObject.transform.position;
   

        gatherPoint = new Vector3(pos.x+0.5f, pos.y, pos.z-2f);

        
    }

    private void Update() {

        if (resources == 0) {


            nt.goldNodes.Remove(gameObject);
            Destroy(gameObject);
        
        }


    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(gatherPoint, 1);
    }


}
