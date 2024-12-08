using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public Transform startLocation;
    public Transform orderLocation;
    public Transform exitLocation;

    private Queue<NPCInteraction> npcQueue = new Queue<NPCInteraction>();
    private NPCInteraction currentNpcAtOrder;

    // Add an NPC to the queue
    public void EnqueueNPC(NPCInteraction npc)
    {
        npcQueue.Enqueue(npc);
        if (currentNpcAtOrder == null) // Start processing the queue if no NPC is at the order location
        {
            ProcessNextNPC();
        }
    }

    private void ProcessNextNPC()
    {
        if (npcQueue.Count > 0)
        {
            currentNpcAtOrder = npcQueue.Dequeue();

            currentNpcAtOrder.transform.position = orderLocation.position;
            currentNpcAtOrder.StartOrder();
        }
    }

    public void NpcOrderComplete(NPCInteraction npc)
    {
        if (npc == currentNpcAtOrder)
        {
            // Move the NPC to the exit location
            StartCoroutine(MoveNpcToExit(npc));
        }
    }

    private IEnumerator MoveNpcToExit(NPCInteraction npc)
    {
        yield return new WaitForSeconds(1f); // Simulate delay after order completion
        npc.transform.position = exitLocation.position;

        // Allow the next NPC to enter the order location
        currentNpcAtOrder = null;
        ProcessNextNPC();
    }
}
