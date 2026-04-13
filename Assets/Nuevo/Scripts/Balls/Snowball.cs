using UnityEngine;

public class Snowball : Ball
{
    [SerializeField] private int damage = 50;

    protected override void ApplyEffect(IHittable target)
    {
        target.TakeDamage(damage);
    }

}
