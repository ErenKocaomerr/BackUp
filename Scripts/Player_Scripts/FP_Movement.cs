using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FP_Movement : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;


    private CharacterController charController;


    private bool isJumping;
    private bool canMove = true;

    private Camera mainCamera;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        charController = GetComponent<CharacterController>();

    }


    private void Update()
    {
        if (!canMove) return;

        PlayerMovement();
    }


    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName);
        float vertInput = Input.GetAxis(verticalInputName);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

    }

    public void DisableMovement(Transform npcTransform)
    {
        canMove = false;

    }

    public void EnableMovement()
    {
        canMove = true;

    }
} 