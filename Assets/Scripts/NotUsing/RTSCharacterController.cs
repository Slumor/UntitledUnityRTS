using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RTSCharacterController : MonoBehaviour{

    Camera cam;
    public LayerMask ground;
    public NavMeshAgent playerAgent;


    // Start is called before the first frame update
    void Awake(){

        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update(){

        if (Input.GetMouseButtonDown(0)) {

            playerAgent.SetDestination(getPointUnderCursor());
        
        }


    }

    private Vector3 getPointUnderCursor() { 
    
        Vector2 screenpos = Input.mousePosition;
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(screenpos);

        RaycastHit hitPos;

        Physics.Raycast(mouseWorldPos, cam.transform.forward, out hitPos, 100, ground);

        return hitPos.point;
        
    
    }



}
