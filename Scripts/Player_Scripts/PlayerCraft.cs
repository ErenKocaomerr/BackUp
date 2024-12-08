using Unity.VisualScripting;
using UnityEngine;

public class PlayerCraft : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask interactLayerMask;

   private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) 
        {
            float interactDistance = 3f;

            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, interactDistance, interactLayerMask))
            {

                if (raycastHit.transform.TryGetComponent(out Crafter craftingAnvil))
                {
                    if (Input.GetKeyDown(KeyCode.E)) 
                    {
                        craftingAnvil.Craft();
                    }
                }
            }
            


        }

    }
}
