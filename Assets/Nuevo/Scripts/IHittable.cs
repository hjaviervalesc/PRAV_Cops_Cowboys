//Lo va a implementar todo a lo que golpeen mis bolas
public interface IHittable
{
    void TakeDamage(int amount);
    void Stun(float duration);
}