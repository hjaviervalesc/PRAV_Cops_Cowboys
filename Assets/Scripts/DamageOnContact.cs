using UnityEngine;

public class DamageOnContact : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null || other.gameObject.GetComponent<Enemy>() != null)
        {
            if (this.GetComponent<PlayerHealth>() != null)
                this.GetComponent<PlayerHealth>().TakeDamage(10);
            if(other.GetComponent<PlayerHealth>() != null)
            {
                var health = other.GetComponent<PlayerHealth>();
                health.TakeDamage(10);
            }
        }
    }
}
