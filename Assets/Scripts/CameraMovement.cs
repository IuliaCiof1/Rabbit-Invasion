using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerRB;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 position = new Vector3(playerRB.position.x, transform.position.y, playerRB.position.z) + offset;
        Vector3 smothedPosition = Vector3.Lerp(transform.position, position, smoothSpeed);

        transform.position = smothedPosition;
    }
}
