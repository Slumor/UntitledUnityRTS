using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depot : MonoBehaviour
{

    public int capacity;
    public int stored;

    public Vector3 gatherPoint;
    // Start is called before the first frame update
    void Start(){

        Vector3 pos = gameObject.transform.position;
        
        gatherPoint = new Vector3(20, 0, 7);

        capacity = 40;
        stored = 0;

}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(gatherPoint, 1);
    }

    public int deposit(int toDeposit) {

        if (stored + toDeposit <= capacity) {

            stored += toDeposit;

            return 0;


        }
        else {

            Debug.Log("Depot full!");
            return toDeposit;
        
        
        }
    
    
    
    
    
    }
}
