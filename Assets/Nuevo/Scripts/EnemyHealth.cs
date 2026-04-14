using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyHealth : MonoBehaviour, IHittable
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private float invulnerabilityPeriod = 0.2f;

    private int _currentHealth;
    private bool _isInvulnerable;
    private bool stunned = false;

    private Enemy enemyScript;
    private TypeOfCharacter typeOfCharacter;
    protected Material _material;       //Para aplicar DOTween en el color



    private void Awake()
    {
        typeOfCharacter = GetComponent<TypeOfCharacter>();
        enemyScript = GetComponent<Enemy>();
        _material = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        _currentHealth = maxHealth;
        _isInvulnerable = false;
        stunned = false;

        if (enemyScript != null)
            enemyScript.enabled = true;
    }

    public void TakeDamage(int amount)
    {
        if (!_isInvulnerable)     //No vulnerable
        {
            StartCoroutine(InvulnerabilityRoutine());      //Espera invulnerabilidad
            _currentHealth -= amount;       //Quita vida
            var healthProportion = _currentHealth / (float)maxHealth;       //Propocion de vida para DOTween
            if (typeOfCharacter.targetType == TypeOfCharacter.TargetType.Enemy)
            {
                Color targetColor = new Color(1, healthProportion, healthProportion, 1);        //Color en proporcion al daño recibido
                _material.DOColor(Color.red, 0.15f).SetLoops(3).onComplete = () => _material.color = targetColor;
            }

            if (_currentHealth <= 0)     //Destruye Enemy
            {
                Die();
            }
        }
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityPeriod);
        _isInvulnerable = false;
    }

    private void Die()
    {
        PoolManager.instance.Despawn(gameObject);
    }

    // -------------------------
    //          STUN
    // -------------------------
    public void Stun(float duration)
    {
        if (!stunned)
            StartCoroutine(StunRoutine(duration));
    }

    private IEnumerator StunRoutine(float duration)
    {
        stunned = true;

        if (enemyScript != null)
            enemyScript.enabled = false;

        yield return new WaitForSeconds(duration);

        if (enemyScript != null)
            enemyScript.enabled = true;

        stunned = false;
    }
}