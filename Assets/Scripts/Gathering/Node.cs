using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    // Start is called before the first frame update
    public int resources;
    public string type;

    public int gather(int gather) {

        if (resources - gather <= 0) {
            int remainder = resources;
            resources = 0;
            return remainder;
            

        }
        else {

            resources = resources - gather;
            return gather;
            
        }
        
    }
}
