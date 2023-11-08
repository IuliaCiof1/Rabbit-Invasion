using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Camera cam;
    Camera camCpy;
    // Start is called before the first frame update
    void Start()
    {
        camCpy = cam;
        camCpy.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 cursorPosition = Input.mousePosition; //screen position
        // cursorPosition.z = transform.position.z; //add depth, otherwise it will result in position of camera
        // Vector3 cursorWorldPosition = cam.ScreenToWorldPoint(cursorPosition);
        //  cursorWorldPosition -= transform.position;
        // //direction.z = 0;
        // transform.up = cursorWorldPosition.normalized;
        // //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // //transform.LookAt(direction);
        ////transform.rotation = Quaternion.Euler(0, angle, 0);


        //Debug.Log(direction + "------" + angle);
        ////transform.rotation = Quaternion.LookRotation(new Vector3(transform.position.x, Input.mousePosition.y, transform.position.z));
        //transform.LookAt(new Vector3(cursorWorldPosition.x, transform.position.y, cursorWorldPosition.z));


        //Vector2 lookDir = cursorPosition - transform.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, angle, 0);
       // camCpy.transform.position = cam.transform.position;
        Vector3 mousePosition = Input.mousePosition;
        
        // Set a distance from the camera to create a plane for the mouse position
        float distanceFromCamera = 1f;

        // Adjust the mouse position with the distance
        mousePosition.z = distanceFromCamera;
        // mousePosition.y -= -30;
        // Convert the mouse position to a world point
         Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(mousePosition);
        //Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cam.nearClipPlane));

        // Calculate the direction from the player to the mouse position
        Vector3 direction = mouseWorldPosition - transform.position;
       // direction.x += -32.27f;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //Debug.DrawRay(mouseWorldPosition, Vector3.down*1000,Color.red);
        // Apply the rotation to the player object
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
