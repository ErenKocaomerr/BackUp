using UnityEngine;

public class TargetPlace : MonoBehaviour
{

    public Transform targetPoint;

    private void OnTriggerEnter(Collider other)
    {
        // Yerle�tirme yap�lacak objeyi kontrol et
        if (other.TryGetComponent(out ItemSOHolder item))
        {
            // Objenin hedef noktaya ta��nmas�
            other.transform.position = targetPoint.position;
            other.transform.rotation = targetPoint.rotation;

            // �ste�e ba�l�: Yerle�me s�ras�nda ek i�lemler yap
            Debug.Log($"{other.name} placed at target area!");

            // �rne�in, bir ses veya efekt tetikleyebilirsiniz
        }
    }
}
