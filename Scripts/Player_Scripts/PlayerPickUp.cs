using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] float PickUpDistance = 3f;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private LayerMask collisionLayerMask;


    private ObjectGrapable objectGrapable;

    private void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
          
            if (objectGrapable == null)
            {
                Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, PickUpDistance, pickupLayerMask);

                if (raycastHit.transform.TryGetComponent(out objectGrapable))
                {
                    objectGrapable.Grab(objectGrabPointTransform);

                  //  Physics.IgnoreLayerCollision(gameObject.layer, collisionLayerMask, true);
                }
            }
            else
            {
                objectGrapable.Drop();
                objectGrapable = null;

              //  Physics.IgnoreLayerCollision(gameObject.layer, collisionLayerMask, false);
            }

        }

    } 

     private void TryPlaceObject() 
     {
        float distanceToTarget = Vector3.Distance(objectGrapable.transform.position, targetPoint.position);

        if (distanceToTarget < 1f) // Hedefe 1 birimden daha yakýnsa
        {
            // Objeyi hedef noktanýn tam pozisyonuna yerleþtir
            objectGrapable.transform.position = targetPoint.position;
            objectGrapable.transform.rotation = targetPoint.rotation;

            Debug.Log("Object placed at target!");
        }
        else
        {
            Debug.Log("Object is too far from the target to place.");
        }

        // Obje býrakýldýktan sonra referansý sýfýrla
        objectGrapable = null;

    }


}
