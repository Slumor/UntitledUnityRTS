using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldNode : Node {

    
    public Vector3 gatherPoint;


    public void Start() {

        Vector3 pos = gameObject.transform.position;

        

        gatherPoint = new Vector3(31, 0, 7);

        GameObject.Find("EventSystem").GetComponent<NodeTracker>().nodes.Add(gameObject);
       
       

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(gatherPoint, 1);
    }


}
