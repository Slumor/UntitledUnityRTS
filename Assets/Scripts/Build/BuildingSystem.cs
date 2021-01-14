using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{

    private GridController grid;
    public GameObject moving;

    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponent<GridController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            //If it hits something
            if (Physics.Raycast(ray, out hit, 50000.0f, 1 << 8)) {



                Cell cell = grid.GetCellFromWorldPos(hit.point);

                moving.transform.position = cell.worldPos;


            }







            }


    }
}
