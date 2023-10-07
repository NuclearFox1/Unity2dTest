using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    //Global camera scroll speed for both arrow keys and edge of screen movement.
    public float scrollSpeed = 5.0f;

    //Edge of screen variables
    public float edgeThreshold = 20.0f;
    private bool isEdgeOfScreenMovement = false;

    //Dragging Variables
    private bool isDragging = false;
    public float dragSensitivity = 0.01f;
    private Vector3 dragOrigin;

    //Arrow key variables.
    private bool isArrowKeyMoving = false;

    //Different types of camera movement enabled?
    public bool screenScrollEnabled = true;
    public bool screenDragEnabled = true;
    public bool arrowKeyMovementEnabled = true;

    void Update()
    {
        Vector3 cameraPosition = transform.position;
        Vector3 mousePosition = Input.mousePosition;

        float horizontalMovement = 0f;
        float verticalMovement = 0f;

        //Reset for this frame check.
        isEdgeOfScreenMovement = false;
        isArrowKeyMoving = false;

        //Moving by edge of screen mouse.
        if (screenScrollEnabled && !isDragging && !isArrowKeyMoving && Application.isFocused)
        {
            

            if (mousePosition.x <= edgeThreshold)
            {
                isEdgeOfScreenMovement = true;
                horizontalMovement = -1f;
            }
            else if (mousePosition.x >= Screen.width - edgeThreshold)
            {
                isEdgeOfScreenMovement = true;
                horizontalMovement = 1f;
            }

            if (mousePosition.y >= Screen.height - edgeThreshold)
            {
                isEdgeOfScreenMovement = true;
                verticalMovement = 1f;
            }
            else if (mousePosition.y <= edgeThreshold)
            {
                isEdgeOfScreenMovement = true;
                verticalMovement = -1f;
            }
        }

        //Moving by arrow keys
        if (arrowKeyMovementEnabled && !isDragging && !isEdgeOfScreenMovement)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                isArrowKeyMoving = true;
                horizontalMovement = -1f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                isArrowKeyMoving = true;
                horizontalMovement = 1f;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                isArrowKeyMoving = true;
                verticalMovement = 1f;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                isArrowKeyMoving = true;
                verticalMovement = -1f;
            }
        }

        // Check for right mouse button press to initiate dragging.
        if (Input.GetMouseButtonDown(1) && screenDragEnabled && !isArrowKeyMoving && !isEdgeOfScreenMovement)
        {
            isDragging = true;
            dragOrigin = mousePosition;
        }

        // Check for right mouse button release to stop dragging.
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        // During dragging, calculate the movement based on mouse drag delta.
        if (isDragging)
        {
            Vector3 dragDelta = (mousePosition - dragOrigin) * dragSensitivity; // Adjust sensitivity
            dragOrigin = mousePosition;
            cameraPosition += new Vector3(-dragDelta.x, -dragDelta.y, 0f);
        }
        else
        {
            // Calculate the movement vector and apply speed.
            Vector3 movement = new Vector3(horizontalMovement, verticalMovement, 0f).normalized;
            cameraPosition += movement * scrollSpeed * Time.deltaTime;
        }

        // Apply the new camera position.
        transform.position = cameraPosition;
    }
}