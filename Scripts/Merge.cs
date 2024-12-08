using Unity.VisualScripting;
using UnityEngine;

public class Merge : MonoBehaviour
{

    public GameObject MergeObject;
    public GameObject MergeObject2;

    [SerializeField] ParticleSystem ParticleSystem = null;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            PlayParticle();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {   // iki gameobject taglara baðlý birleþiyor 
        if (collision.gameObject.CompareTag("Knife") && gameObject.CompareTag("Yellow"))
        {       
                Destroy(gameObject); //Birleþtiðinde ikinci gameobjecti yok ediyor 
                GameObject obj = Instantiate(MergeObject, transform.position = gameObject.transform.position , Quaternion.identity);
            
        }

        if (collision.gameObject.CompareTag("Orange") && gameObject.CompareTag("Yellow"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameObject obj = Instantiate(MergeObject2, transform.position = gameObject.transform.position, Quaternion.identity);
        }

        if (collision.gameObject.CompareTag("Red") && gameObject.CompareTag("Gold"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameObject obj = Instantiate(MergeObject, transform.position = gameObject.transform.position, Quaternion.identity);
        }

    }

    public void PlayParticle() 
    {
        ParticleSystem.Play();
    }
}
