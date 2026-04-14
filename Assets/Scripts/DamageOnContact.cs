using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        // 1. ¿El otro objeto puede recibir daño?
        if (other.TryGetComponent<IHittable>(out var hittable))
        {
            hittable.TakeDamage(damage);

        }
    }
}