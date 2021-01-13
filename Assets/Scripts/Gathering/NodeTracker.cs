using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTracker : MonoBehaviour{

    //Tracks marked nodes

    public List<GameObject> goldNodes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Gold").Length; i++) {
            GameObject Gnode = GameObject.FindGameObjectsWithTag("Gold")[i];
            goldNodes.Add(Gnode);
            Gnode.GetComponent<GoldNode>().nt = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
