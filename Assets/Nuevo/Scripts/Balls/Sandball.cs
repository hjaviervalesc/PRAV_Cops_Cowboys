using UnityEngine;

public class Sandball : Ball
{
    [SerializeField] private float stunTime = 3f;
    protected override void ApplyEffect(IHittable target)
    {
        target.Stun(stunTime);
    }

}
