using UnityEngine;

public class FootStep : MonoBehaviour
{

    public AudioSource AudioSource;

    public AudioClip footStep;

    RaycastHit hits;
    public Transform RayStart;
    public float Range;
    public LayerMask LayerMask; 

    private void FootSteps() 
    {
        if (Physics.Raycast(RayStart.position, RayStart.transform.up * -1 , out hits , Range , LayerMask))
        {
            if (hits.collider.CompareTag("indoor"))
            {
                FootStepSound(footStep);
            }
        }
    }

    void FootStepSound(AudioClip audio) 
    {
        AudioSource.pitch = Random.Range(0.8f, 1f);
        AudioSource.PlayOneShot(audio); 
    }
}
