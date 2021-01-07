using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depot : MonoBehaviour
{

    public Vector3 gatherPoint;
    // Start is called before the first frame update
    void Start(){

        Vector3 pos = gameObject.transform.position;
        
        gatherPoint = new Vector3(pos.x + 10f, pos.y, pos.z);

}

    // Update is called once per frame
    void Update()
    {
        
    }
}
