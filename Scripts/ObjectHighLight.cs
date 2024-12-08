using UnityEngine;
using TMPro;

public class ObjectHighLight : MonoBehaviour
{

    public float displayDistance = 4f; // Maximum distance to show the object's name
    public string referenceTag = "Player"; // Tag for the reference point (e.g., Player or MainCamera)
    public string uiTextTag = "InformationText"; // Tag for the UI Text element

    private Transform referencePoint; // Cached reference point (e.g., Player)
    private TMP_Text objectNameText; // Cached UI Text component for the object name

    private bool isHovered = false; // Flag to track hover state


    void Start()
    {
        // Find the reference point dynamically by tag
        GameObject referenceObject = GameObject.FindGameObjectWithTag(referenceTag);
        if (referenceObject != null)
        {
            referencePoint = referenceObject.transform;
        }
        else
        {
            Debug.LogError($"Reference point with tag '{referenceTag}' not found in the scene.");
            enabled = false; // Disable the script to prevent errors
            return;
        }

        // Find the UI Text dynamically by tag
        GameObject textObject = GameObject.FindGameObjectWithTag(uiTextTag);
        if (textObject != null)
        {
            objectNameText = textObject.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogError($"UI Text with tag '{uiTextTag}' not found in the scene.");
            enabled = false; // Disable the script to prevent errors
            return;
        }
    }

    void Update()
    {
        // Skip logic if setup failed
        if (referencePoint == null || objectNameText == null) return;

        // Calculate the distance from the reference point to the object
        float distanceToReference = Vector3.Distance(referencePoint.position, transform.position);

        // Perform raycast to check if the object is under the mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Check if the object is hit and within the specified distance
            if (hit.collider.gameObject == gameObject && distanceToReference <= displayDistance)
            {
                
                if (!isHovered)
                {
                    isHovered = true;
                    ShowObjectName();
                }

            }
            else
            {
                // Reset scaling and hide the name
                if (isHovered)
                {
                    isHovered = false;
                    HideObjectName();
                }

            }
        }
        else
        {
            // Reset scaling and hide the name when not hovering
            if (isHovered)
            {
                isHovered = false;
                HideObjectName();
            }
        }
    }

    
    void ShowObjectName()
    {
        if (objectNameText != null)
        {
            objectNameText.text = gameObject.name;
            objectNameText.enabled = true;
        }
    }

    void HideObjectName()
    {
        if (objectNameText != null)
        {
            objectNameText.enabled = false;
        }
    }
}


