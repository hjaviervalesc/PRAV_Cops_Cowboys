using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UIElements;

public class Speedable : MonoBehaviour, IPickable
{
    public float speedMultiplier = 1.5f;
    public float duration = 3f;

    // Delegado opcional para añadir efectos extra
    public Action<Player> OnPickedExtra;

    [SerializeField] private JSONLoggerSpeedables speedableLogger;

    public void OnPick(Player player)
    {
        // Registrar recogida
        if (speedableLogger != null)
            speedableLogger.RegisterPickup();

        // Aplicar efecto al jugador
        player.movement.ModifySpeed(speedMultiplier, duration);

        // Tween visual
        transform.DOScale(0, 0.25f).SetEase(Ease.InBack);

        // Efectos extra del manager
        OnPickedExtra?.Invoke(player);

        // Devolver al pool después del tween
        DOVirtual.DelayedCall(0.3f, () =>
        {
            PoolManager.instance.Despawn(gameObject);
        });
    }
}