using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    public NpcManager npcManager;
    public GameObject npcPrefab;

    public void SpawnNPC()
    {
        GameObject npcObject = Instantiate(npcPrefab, npcManager.startLocation.position, Quaternion.identity);
        NPCInteraction npcInteraction = npcObject.GetComponent<NPCInteraction>();

        if (npcInteraction != null)
        {
            npcInteraction.startLocation = npcManager.startLocation;
            npcInteraction.orderLocation = npcManager.orderLocation;
            npcInteraction.exitLocation = npcManager.exitLocation;

            npcManager.EnqueueNPC(npcInteraction);
        }
    }
}
