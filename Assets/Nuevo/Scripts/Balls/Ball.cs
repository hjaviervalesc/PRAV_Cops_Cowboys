using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 50f;

    void Update()
    {
        // Caída 
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        //Devuelve true y false si existe o no y con out te devuelve el componente.
        // Si golpea algo que puede recibir efectos
        if (other.TryGetComponent<IHittable>(out var hittable))
        {
            ApplyEffect(hittable);
            PoolManager.instance.Despawn(gameObject);
            return;
        }

        // Si toca el cualquier cosa del entorno
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            PoolManager.instance.Despawn(gameObject);
        }


    }

    // Cada tipo de bola implementa su propio efecto
    protected abstract void ApplyEffect(IHittable target);
}