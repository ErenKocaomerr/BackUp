using UnityEngine;
using TMPro;

public class ObjectHighlightWithRaycast : MonoBehaviour
{
    public Material highlightMaterial; // Material to apply for highlighting
    public TMP_Text objectNameText; // Reference to the TextMeshPro UI element for the object name
    private Material originalMaterial; // To store the object's original material
    private Renderer objectRenderer; // To access the renderer of the hovered object
    private bool isHighlighted = false; // To track if the object is highlighted

    private Camera mainCamera;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;

        // Ensure the object has a renderer component
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material; // Save the original material
        }
        else
        {
            Debug.LogError("Object doesn't have a Renderer component.");
        }

        // Hide the object name by default
        if (objectNameText != null)
        {
            objectNameText.enabled = false;
        }
    }

    void Update()
    {
        // Perform a raycast from the camera to the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits an object
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the hit object is the current one
            if (hitObject == gameObject && !isHighlighted)
            {
                HighlightObject(hitObject);
            }
            else if (hitObject != gameObject && isHighlighted)
            {
                RemoveHighlight();
            }
        }
        else
        {
            // If the ray does not hit any object, remove the highlight
            if (isHighlighted)
            {
                RemoveHighlight();
            }
        }
    }

    // Highlights the object by adding the highlight material
    private void HighlightObject(GameObject hitObject)
    {
        if (objectRenderer != null && highlightMaterial != null && !isHighlighted)
        {
            // Add highlight material to the material array
            Material[] newMaterials = new Material[objectRenderer.materials.Length + 1];
            objectRenderer.materials.CopyTo(newMaterials, 0);
            newMaterials[newMaterials.Length - 1] = highlightMaterial;
            objectRenderer.materials = newMaterials;

            // Show object name
            if (objectNameText != null)
            {
                objectNameText.text = hitObject.name;
                objectNameText.enabled = true;
            }

            isHighlighted = true; // Mark as highlighted
        }
    }

    // Removes the highlight material and resets to the original material
    private void RemoveHighlight()
    {
        if (objectRenderer != null && highlightMaterial != null && isHighlighted)
        {
            // Remove the highlight material from the material array
            Material[] currentMaterials = objectRenderer.materials;
            Material[] newMaterials = new Material[currentMaterials.Length - 1];

            int index = 0;
            for (int i = 0; i < currentMaterials.Length; i++)
            {
                if (currentMaterials[i] != highlightMaterial)
                {
                    newMaterials[index] = currentMaterials[i];
                    index++;
                }
            }

            objectRenderer.materials = newMaterials;

            // Hide the object name
            if (objectNameText != null)
            {
                objectNameText.enabled = false;
            }

            isHighlighted = false; // Mark as not highlighted
        }
    }
}
