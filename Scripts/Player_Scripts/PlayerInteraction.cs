using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 5f;  // Maximum interaction range
    public LayerMask interactionLayer;   // Filter for interactable objects (e.g., NPCs)
    private NPCInteraction currentNPC;   // Current NPC being looked at

    private void Update()
    {
        if (DialogueSystem.Instance.IsInteracting)
        {
            // Disable interaction while a dialogue is active
            return;
        }

        // Check for NPC under the crosshair
        CheckForNPC();

        // Handle interaction if mouse button pressed
        HandleInteraction();
    }

    private void CheckForNPC()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // Ray from center of screen (crosshair)
        RaycastHit hit;

        // Cast ray and check if it hits an NPC in interaction range
        if (Physics.Raycast(ray, out hit, interactionRange, interactionLayer))
        {
            currentNPC = hit.collider.GetComponent<NPCInteraction>(); // Get the NPCInteraction script attached to the NPC
        }
        else
        {
            currentNPC = null; // No NPC under the crosshair
        }
    }

    // Handle interaction when the player presses the interaction button (right-click)
    private void HandleInteraction()
    {
        if (currentNPC != null && Input.GetMouseButtonDown(0)) // Right-click to interact
        {
            currentNPC.Interact(); // Trigger the interaction
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Optional: Visualize the interaction range in the Scene view
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactionRange);
    }

}
