using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public OrderDataSO dialogueData; // NPC's dialogue data
    private bool isOrderCompleted = false;
    private Dictionary<string, int> remainingItems;

    public Transform startLocation;
    public Transform orderLocation;
    public Transform exitLocation;
    private NpcManager npcManager;



    private void Start()
    {
        InitializeRemainingItems();
        npcManager = FindAnyObjectByType<NpcManager>();

    }


    private void InitializeRemainingItems()
    {
        remainingItems = new Dictionary<string, int>();

        foreach (var tag in dialogueData.requiredTags)
        {
            if (remainingItems.ContainsKey(tag))
            {
                remainingItems[tag]++;
            }
            else
            {
                remainingItems[tag] = 1;
            }
        }

        foreach (var pair in remainingItems)
        {
            Debug.Log($"Initialized Item: {pair.Key}, Count: {pair.Value}");
        }
    }

    //Trigger when Npc on OrderPoint
    public void StartOrder()
    {
        
        Interact();
    }


    // Method to interact with the NPC
    public void Interact()
    {
        if (dialogueData != null)
        {
            DialogueSystem.Instance.StartDialogue(dialogueData.dialogLines, transform);

        }
        else
        {
            Debug.LogWarning($"{gameObject.name} has no dialogue data assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOrderCompleted) return;

        string itemTag = other.gameObject.tag;

        if (dialogueData != null && TryDeliverItem(itemTag))
        {
            Debug.Log($"Item {itemTag} delivered successfully.");
            Destroy(other.gameObject); // Remove the delivered item

            if (IsOrderCompletedClass())
            {
                CompleteOrder();
            }
        }
    }

    private bool TryDeliverItem(string itemTag)
    {
        if (remainingItems.ContainsKey(itemTag) && remainingItems[itemTag] > 0) 
        {
            remainingItems[itemTag]--;
            return true;
        }
       
        return false;
    }


    private bool IsOrderCompletedClass()
    {
        foreach (var item in remainingItems)
        {
            // If any item's count is greater than 0, the order is incomplete
            if (item.Value > 0)
            {
                return false;
            }
        }
        //Order Complete
        return true;
    }



    private void CompleteOrder()
    {
        isOrderCompleted = true;
        DialogueSystem.Instance.StartDialogue(new string[] { dialogueData.completedDialog }, transform);
        npcManager.NpcOrderComplete(this);
    }

    private IEnumerator DisappearAfterInteraction()
    {
        yield return new WaitForSeconds(2f); // Wait for dialogue to finish
        gameObject.SetActive(false); // NPC disappears
    }
}
