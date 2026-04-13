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

    public void OnPick(Player player)
    {
        // Aumentar velocidad
        player.movement.ModifySpeed(speedMultiplier, duration);

        // Tween visual básico
        transform.DOScale(0, 0.25f).SetEase(Ease.InBack);

        // Ejecutar efectos extra si los hay
        OnPickedExtra?.Invoke(player);

        transform.DOScale(0, 0.25f)
        .SetEase(Ease.InBack)
        .OnKill(() => { }); // evita errores

        // Destruir power-up
        Destroy(gameObject, 0.3f);
    }
}