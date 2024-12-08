using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEditor.Rendering;
using System.Collections;
using Unity.Cinemachine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }

    public TMP_Text dialogText;
    public GameObject dialogBox;

    private Queue<string> dialogQueue;
    public bool IsInteracting { get; private set; }
    private bool hasStartedDialogue = false;

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    [SerializeField]  public float typingSpeed = 0.05f;

    private FP_Movement playerMovement;
    public CinemachineCamera dialogueCamera;

    private Transform currentNpcTransform;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dialogQueue = new Queue<string>();
        dialogBox.SetActive(false); //Hide Start

        playerMovement = FindFirstObjectByType<FP_Movement>();
    }


    public void StartDialogue(string[] dialogueLines, Transform npcTransform)
    {
        IsInteracting = true;
        hasStartedDialogue = false;
        dialogQueue.Clear();

        // Enqueue the dialogue lines from the ScriptableObject
        foreach (var line in dialogueLines)
        {
            dialogQueue.Enqueue(line);
        }

        dialogBox.SetActive(true); 
        currentNpcTransform = npcTransform;

        playerMovement?.DisableMovement(npcTransform);
        ActivateDialogueCamera(npcTransform);

        ShowNextLine();
    }

    private void ShowNextLine()
    {
        if (isTyping)
        {
            // If still typing, do nothing
            return;
        }

        if (dialogQueue.Count > 0)
        {
            string line = dialogQueue.Dequeue();
            typingCoroutine = StartCoroutine(TypeText(line));
        }
        else
        {
            EndInteraction();
        }
    }

    public void EndInteraction()
    {
        IsInteracting = false;
        dialogBox.SetActive(false); // Hide the dialogue box

        playerMovement?.EnableMovement();
        DeactivateDialogueCamera();

    }

    private void Update()
    {
        if (IsInteracting)
        {
            // Handle dialogue progression only after the first line is shown
            if (!hasStartedDialogue)
            {
                // Wait for the first frame of interaction
                if (Input.GetMouseButtonDown(0))
                {
                    hasStartedDialogue = true; // Consume the first button press
                }
            }
            else
            {
                // Progress the dialogue
                if (Input.GetMouseButtonDown(0))
                {
                    ShowNextLine();
                }
            }
        }
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogText.text = ""; // Clear current text

        foreach (char c in line)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }


    private void ActivateDialogueCamera(Transform npcTransform)
    {
        if (dialogueCamera != null)
        {
            dialogueCamera.LookAt = npcTransform;
            dialogueCamera.Priority = 10; // Set a higher priority to make this camera active
        }
    }

    private void DeactivateDialogueCamera()
    {
        if (dialogueCamera != null)
        {
            dialogueCamera.Priority = 0; // Lower priority to deactivate this camera
            dialogueCamera.LookAt = null; // Clear the Look At target
        }
    }

}
