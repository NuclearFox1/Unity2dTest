using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerSelect : MonoBehaviour
{
    public GameObject selectedUnit;
    public GameObject previousSelection;
    public bool canSelectUnits;
    public Color selectedColor = Color.yellow; //Selection Color
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    private bool hitSomethingUI;
    public GameObject squareOutline;



    // Start
    void Start()
    {

    }

    // Update
    void Update()

    {
        hitSomethingUI = false;

        if (canSelectUnits && Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);


            if (EventSystem.current.IsPointerOverGameObject())
            {
                hitSomethingUI = true;
                Debug.Log("Clicked UI element");
            }

            else if (hit.collider != null && !hitSomethingUI)
            {
                Debug.Log("Hit an object called: " + hit.collider.gameObject.name);
                GameObject newSelection = hit.collider.gameObject;
                if (newSelection.CompareTag("Unit"))
                {
                    SelectUnit(newSelection);
                }
                else if (newSelection.CompareTag("Building"))
                {
                    SelectUnit(newSelection);
                }
            }
            else
            {
                Debug.Log("Hit nothing.");

                DeselectUnit();
            }
        }

        //Middle mouse test destroy unit.
        if (Input.GetMouseButtonUp(2))
        {
            if (selectedUnit != null)
            {
                Destroy(selectedUnit);
            }
        }
    }

    void SelectUnit(GameObject unit)
    {
        if (unit == selectedUnit)
            return;

        DeselectUnit();

        selectedUnit = unit;
        squareOutline.transform.position = selectedUnit.transform.position;

        if (selectedUnit != null)
        {
            spriteRenderer = selectedUnit.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                originalColor = spriteRenderer.color;
                spriteRenderer.color = selectedColor;
            }
        }
    }

    void DeselectUnit()
    {
        if (selectedUnit != null)
        {
            if (selectedUnit != null)
            {
                // Visual feedback: Restore the SpriteRenderer's original color
                spriteRenderer = selectedUnit.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = originalColor;
                }
            }
            squareOutline.transform.position = new Vector3(2000f, squareOutline.transform.position.y, squareOutline.transform.position.z);

            previousSelection = selectedUnit;
            // Deselecting units
        }


        selectedUnit = null;

    }
}