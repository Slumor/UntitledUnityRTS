using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldNode : Node {

    
    public Vector3 gatherPoint;


    public void Start() {

        Vector3 pos = gameObject.transform.position;

        gatherPoint = pos;

        GameObject.Find("EventSystem").GetComponent<NodeTracker>().nodes.Add(gameObject);
       
       

    }


}
