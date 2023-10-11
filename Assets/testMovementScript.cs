using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMovementScript : MonoBehaviour
{
    public float movementSpeedY = 0.0005f;
    public float movementSpeedX = 0.0005f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y += movementSpeedY;
        currentPosition.x += movementSpeedX;
        transform.position = currentPosition;
    }
}
