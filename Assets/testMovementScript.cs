using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y += 0.0005f;
        transform.position = currentPosition;
    }
}
