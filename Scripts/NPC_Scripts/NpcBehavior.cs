using UnityEngine;

public class NpcBehavior : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Transform targetLocation;
    private System.Action onDestinationReached;

    // Start moving towards a target location
    public void MoveTo(Transform target, System.Action onReach)
    {
        targetLocation = target;
        onDestinationReached = onReach;
    }

    private void Update()
    {
        if (targetLocation == null) return;

        // Move towards the target location
        transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, moveSpeed * Time.deltaTime);

        // Check if the NPC has reached the target
        if (Vector3.Distance(transform.position, targetLocation.position) < 0.1f)
        {
            targetLocation = null; // Stop moving
            onDestinationReached?.Invoke(); // Notify when destination is reached
        }
    }
}
