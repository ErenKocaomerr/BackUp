using UnityEngine;

public class ObjectGrapable : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;
    [SerializeField] float lerpSpeed = 15f;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        objectRigidBody.interpolation = RigidbodyInterpolation.Interpolate;
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidBody.useGravity = false;
        objectRigidBody.isKinematic = false;
        objectRigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity = true;
        objectRigidBody.isKinematic = false;
        objectRigidBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 targetPosition = objectGrabPointTransform.position;
            Vector3 currentPosition = transform.position;

            //Calculate the velocity to move the object towards the target
            Vector3 velocity = (targetPosition - currentPosition) * lerpSpeed;

            // Apply gravity manually to preserve its effect
            Vector3 gravityVelocity = Physics.gravity * Time.fixedDeltaTime;


            // Smoothly move the object towards the target position
            objectRigidBody.linearVelocity = velocity + gravityVelocity;

        }
        else 
        {
            objectRigidBody.linearVelocity += Physics.gravity * Time.fixedDeltaTime;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Prevent moving static or immovable objects
        Rigidbody otherRigidbody = collision.rigidbody;
        if (otherRigidbody == null || otherRigidbody.isKinematic)
        {
        }
    }

    private bool IsNearNPC()
    {
        // Check proximity to an NPC (adjust radius as needed)
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (var hit in hitColliders)
        {
            if (hit.GetComponent<NPCInteraction>())
            {
                return true;
            }
        }
        return false;
    }

}
