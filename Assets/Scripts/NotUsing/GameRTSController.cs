using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRTSController : MonoBehaviour {



    private Vector3 startPosition;
    private void Update(){
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            startPosition = hit.point;

        
            


        }

        if (Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            Debug.Log(startPosition + " " + hit.point);

            Collider[] colliderArray = Physics.OverlapBox(startPosition, hit.point);

            foreach (Collider col in colliderArray) {
                Debug.Log(col);
            }

        }

    }
}
