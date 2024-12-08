using UnityEngine;

public class TargetPlace : MonoBehaviour
{

    public Transform targetPoint;

    private void OnTriggerEnter(Collider other)
    {
        // Yerleþtirme yapýlacak objeyi kontrol et
        if (other.TryGetComponent(out ItemSOHolder item))
        {
            // Objenin hedef noktaya taþýnmasý
            other.transform.position = targetPoint.position;
            other.transform.rotation = targetPoint.rotation;

            // Ýsteðe baðlý: Yerleþme sýrasýnda ek iþlemler yap
            Debug.Log($"{other.name} placed at target area!");

            // Örneðin, bir ses veya efekt tetikleyebilirsiniz
        }
    }
}
