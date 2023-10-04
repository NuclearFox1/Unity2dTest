using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSelect : MonoBehaviour
{
    public GameObject selectedUnit;
    public GameObject previousSelection;
    public bool canSelectUnits;
    public Color selectedColor = Color.yellow; //Selection Color
    private Color originalColor;
    private SpriteRenderer spriteRenderer;


    // Start
    void Start()
    {

    }

    // Update
    void Update()
    {
        if (canSelectUnits && Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
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

            previousSelection = selectedUnit;
            // Deselecting units
        }


        selectedUnit = null;

    }
}
