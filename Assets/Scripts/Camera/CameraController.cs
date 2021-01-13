using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public struct edgeLimit {
        public float LeftEdge;
        public float RightEdge;
        public float TopEdge;
        public float BottomEdge;
    
    }

    public static edgeLimit cameraLimit = new edgeLimit();
    public static edgeLimit scrollLimit = new edgeLimit();

    private float cameraSpeed = 60f;
    public float shiftBoost = 45f;
    public float mouseEdge = 50f;

    private void Awake() {


       
    }


    void Start(){

        cameraLimit.LeftEdge = -1000.0f;
        cameraLimit.RightEdge = 1000.0f;
        cameraLimit.TopEdge = 1000.0f;
        cameraLimit.BottomEdge = -1000.0f;

        scrollLimit.LeftEdge = mouseEdge;
        scrollLimit.RightEdge = mouseEdge;
        scrollLimit.TopEdge = mouseEdge;
        scrollLimit.BottomEdge = mouseEdge;

        
    }

    // Update is called once per frame
    void Update(){
        if (CheckIfUserCameraInput()) {

            Vector3 cameraDesiredMove = getDesiredTranslation();

            if (!isDesiredPositionOverBoundaries(cameraDesiredMove)) {     
                this.transform.Translate(cameraDesiredMove);
            
            }
        
        }
        
    }

    public bool CheckIfUserCameraInput() {

        bool keyboardMove;
        bool mouseMove;
        bool canMove;

        //Keyboard
        if (CameraController.AreCameraKeyboardButtonsPressed()) {
            keyboardMove = true;
        }else { 
            keyboardMove = false; 
        }

        if (CameraController.IsMousePositionWithinBoundaries()) {
            mouseMove = true;
        }else {
            mouseMove = false;
        }

        if (keyboardMove || mouseMove) {
            canMove = true;
        }else {
            canMove = false;
        }

        return canMove;
    
    
    }

    public Vector3 getDesiredTranslation() {

        float moveSpeed;
        float desiredX = 0f;
        float desiredZ = 0f;

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            moveSpeed = (cameraSpeed + shiftBoost) * Time.deltaTime;
        } else {
            moveSpeed = cameraSpeed * Time.deltaTime;
        }

        //Keyboard
        if (Input.GetKey(KeyCode.W)) {
            desiredZ = moveSpeed;
        }

        if (Input.GetKey(KeyCode.S)) {
            desiredZ = moveSpeed * -1;
        }

        if (Input.GetKey(KeyCode.A)) {
            desiredX = moveSpeed * -1;
        }

        if (Input.GetKey(KeyCode.D)) {
            desiredX = moveSpeed;
        }

        //Mouse

        if (Input.mousePosition.x < scrollLimit.LeftEdge) {
            desiredX = moveSpeed * -1;
        }

        if (Input.mousePosition.x > (Screen.width - scrollLimit.RightEdge)) {
            desiredX = moveSpeed;
        }

        if (Input.mousePosition.y < scrollLimit.BottomEdge) {
            desiredZ = moveSpeed * -1;
        }

        if (Input.mousePosition.y > (Screen.height - scrollLimit.TopEdge)) {
            desiredZ = moveSpeed;
        }

        return new Vector3(desiredX, 0, desiredZ);


    }


    public bool isDesiredPositionOverBoundaries(Vector3 desiredPosition) {

        bool overBoundaries = false;

        if ((this.transform.position.x + desiredPosition.x) < cameraLimit.LeftEdge) { overBoundaries = true; }
        if ((this.transform.position.x + desiredPosition.x) > cameraLimit.RightEdge) { overBoundaries = true; }
        if ((this.transform.position.z + desiredPosition.z) > cameraLimit.TopEdge) { overBoundaries = true; }
        if ((this.transform.position.z + desiredPosition.z) < cameraLimit.BottomEdge){ overBoundaries = true; }


        return overBoundaries;
    
    }





    public static bool AreCameraKeyboardButtonsPressed() {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) { return true; } else { return false; }

    }

    public static bool IsMousePositionWithinBoundaries() {

        if (

            (Input.mousePosition.x < scrollLimit.LeftEdge && Input.mousePosition.x > -5) ||
            (Input.mousePosition.x > (Screen.width - scrollLimit.RightEdge) && Input.mousePosition.x < (Screen.width + 5)) ||
            (Input.mousePosition.y < scrollLimit.BottomEdge && Input.mousePosition.y > -5) ||
            (Input.mousePosition.y > (Screen.width - scrollLimit.TopEdge) && Input.mousePosition.y < (Screen.width + 5))

                ) { return true; } else { return false; }
        
    
    
    
    }



}
