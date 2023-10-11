using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSquareOutline : MonoBehaviour
{

    public LineRenderer square;
    public bool targetSelected = false;
    private GameObject targetedObject;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        square = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetSelected && targetedObject != null)
        {
            transform.position = targetedObject.transform.position;
        }
        else if (targetedObject == null && targetSelected)
        {
            ForceDeselection();
        }
    }

    //Change the shape of the square outline to match the box collider of the selected object.
    public void AdjustShape(GameObject targetObject)
    {
        

        if (targetObject != null)
        {
            targetedObject = targetObject;
            BoxCollider2D targetCollider = targetObject.GetComponent<BoxCollider2D>();

            if (targetCollider != null)
            {
                Vector3[] points = new Vector3[5];

                Vector3 selectedUnitPosition = targetObject.transform.position;

                // Calculate the points of the adjusted square based on the BoxCollider2D bounds
                Bounds bounds = targetCollider.bounds;

                points[0] = new Vector3(bounds.min.x, bounds.max.y, 0) - selectedUnitPosition; // Top-left
                points[1] = new Vector3(bounds.max.x, bounds.max.y, 0) - selectedUnitPosition; // Top-right
                points[2] = new Vector3(bounds.max.x, bounds.min.y, 0) - selectedUnitPosition; // Bottom-right
                points[3] = new Vector3(bounds.min.x, bounds.min.y, 0) - selectedUnitPosition; // Bottom-left
                points[4] = points[0];

                square.SetPositions(points);
            }
        } 
    }

    public void ForceDeselection()
    {
        playerSelect playerSelectScript = player.GetComponent<playerSelect>();
        playerSelectScript.DeselectUnit();
        Debug.Log("Destroyed object and called deselect.");
        targetSelected = false;
    }
}
