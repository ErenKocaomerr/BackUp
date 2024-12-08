using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Crafter : MonoBehaviour
{
    [SerializeField] private Image Image;
    [SerializeField] private List<RecipieSO> RecipieSOList;
    [SerializeField] BoxCollider ItemPlaceBoxCollider;
    [SerializeField] private Transform ItemSpawnPoint;
    [SerializeField] private Transform vfxSpawnItem;



    public void Craft() 
    {
        // Debug.Log("Craft");
       Collider[] collderArray = Physics.OverlapBox(
            transform.position + 
            ItemPlaceBoxCollider.center, 
            ItemPlaceBoxCollider.size ,
            ItemPlaceBoxCollider.transform.rotation);

        List<ItemsScriptableObject> ýnputItemList = new List<ItemsScriptableObject>();
        List<GameObject> consubleItemGameObjectsList = new List<GameObject>();


        foreach (Collider collider in collderArray) 
        {
            if (collider.TryGetComponent(out ItemSOHolder ýtemSOHolder))
            {
                ýnputItemList.Add(ýtemSOHolder.ýtemsScriptableObject);
                consubleItemGameObjectsList.Add(collider.gameObject);
            }
        }


        foreach (RecipieSO recipie in RecipieSOList)
        {
            
            List<ItemsScriptableObject> requariedItems = new List<ItemsScriptableObject>(recipie.ýnputItemSOList);
            List<ItemsScriptableObject> matchedItems = new List<ItemsScriptableObject>();


            foreach (ItemsScriptableObject items in ýnputItemList) 
            {
                if (requariedItems.Contains(items))
                {
                    requariedItems.Remove(items);
                    matchedItems.Add(items);
                }
            }

            // Tüm Gerekli eþyalar tariftemi
            bool allRequiredItemsPresent = requariedItems.Count == 0;

            // Tarifte Fazladan Ýtem varmý?
            bool hasExtraItems = matchedItems.Count != ýnputItemList.Count; 


            //  Tarif Doðruysa Girilen eþyalarý yok edip tarifi spawn etme
            if (allRequiredItemsPresent && !hasExtraItems)
            {

                Transform spawnItemTransform = Instantiate(recipie.outputItemSO.prefab, ItemSpawnPoint.position, ItemSpawnPoint.rotation);

                Instantiate(vfxSpawnItem, ItemSpawnPoint.position, ItemSpawnPoint.rotation);

                foreach (GameObject consubleItemGameObject in consubleItemGameObjectsList)
                {
                    Destroy(consubleItemGameObject);
                }

                Debug.Log("Oley");
            }
        }

        Debug.Log("No matching recipe found with available items.");

    }
}
