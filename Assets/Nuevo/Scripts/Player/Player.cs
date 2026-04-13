using UnityEngine;

public class Player : Singleton<Player>
{
    public PlayerMovement movement { get; private set; }
    public PlayerShooting shooting { get; private set; }
    public PlayerHealth health { get; private set; }

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        shooting = GetComponent<PlayerShooting>();
        health = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IPickable>(out var pickable))
        {
            pickable.OnPick(this);
        }
    }
}