using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    // Start is called before the first frame update
    public int resources;
    public string type;

    public int gather() {

        if (resources != 0) {
            resources = resources - 1;
            return 1;
        }
        else {

            return 0;
        }
        
    }
}
