using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Notes: We could also do Orthographic camera view instead of perspective maybe.
//TODO: Fix when zooming in the camera is becomming 'janky' or getting 'spasms' xD

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    // Start is called before the first frame update.
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        //we're using localPosition in order for it to stay relative to our camera rig.
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame.
    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput () 
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        // This is the movement supporting both "WASD" and "Arrow" keys cuz' why not.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        // Rotation Input.
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        //Camera Zoom Input.
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        // Making the movement more smooth rather then just setting the position of the transform.
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        //And the same for rotation.
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        //And for Zoom.
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
