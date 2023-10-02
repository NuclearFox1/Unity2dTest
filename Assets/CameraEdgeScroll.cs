using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeScroll : MonoBehaviour
{
    public float scrollSpeed = 5.0f;
    public float edgeThreshold = 20.0f;
    public float dragSensitivity = 0.01f;

    private bool isDragging = false;
    private Vector3 dragOrigin;

    void Update()
    {
        Vector3 cameraPosition = transform.position;
        Vector3 mousePosition = Input.mousePosition;

        float horizontalMovement = 0f;
        float verticalMovement = 0f;

        if (!isDragging)
        {
            if (mousePosition.x <= edgeThreshold)
            {
                horizontalMovement = -1f;
            }
            else if (mousePosition.x >= Screen.width - edgeThreshold)
            {
                horizontalMovement = 1f;
            }

            if (mousePosition.y >= Screen.height - edgeThreshold)
            {
                verticalMovement = 1f;
            }
            else if (mousePosition.y <= edgeThreshold)
            {
                verticalMovement = -1f;
            }
        }

        // Check for right mouse button press to initiate dragging.
        if (Input.GetMouseButtonDown(1))
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
            Vector3 dragDelta = (mousePosition - dragOrigin) * dragSensitivity; // Adjust sensitivity if needed.
            dragOrigin = mousePosition;
            cameraPosition += new Vector3(-dragDelta.x, -dragDelta.y, 0f); // Corrected the vertical movement here.
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