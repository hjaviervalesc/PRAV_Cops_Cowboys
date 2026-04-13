using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerHealth : MonoBehaviour, IHittable
{
    public int maxHealth = 100;         //Vida maxima
    public float invulnerabilityPeriod = .5f;       //Tiempo sin quitar vida

    public bool isAlive => _currentHealth > 0;      //Comprueba que sigue vivo
    public GameObject camera;
    public TargetType targetType;

    [SerializeField]
    protected int _currentHealth;       
    protected Material _material;       //Para aplicar DOTween en el color
    protected bool isInvulnerable;

    //Nuevo
    private bool stunned = false;
    private PlayerMovement movement;
    private PlayerShooting shooting;

    [SerializeField] private JSONLoggerKills killsLogger;


    public enum TargetType
    {
        Player
    }

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        camera = GameObject.Find("Main Camera");

        movement = Player.instance.GetComponent<PlayerMovement>();
        shooting = Player.instance.GetComponent<PlayerShooting>();

    }


    private void OnEnable()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)      //Sufre daño
    {
        if(!isInvulnerable)     //No vulnerable
        {
            StartCoroutine(SetInvulnerable());      //Espera invulnerabilidad
            _currentHealth -= amount;       //Quita vida
            var healthProportion = _currentHealth / (float)maxHealth;       //Propocion de vida para DOTween
            if(targetType == TargetType.Player )
            {
                Color targetColor = new Color(1, healthProportion, healthProportion, 1);        //Color en proporcion al daño recibido
                _material.DOColor(Color.red, 0.15f).SetLoops(3).onComplete = () => _material.color = targetColor;
            }

            if(_currentHealth <= 0)     //Destruye Player
            {
                _currentHealth = 0;
                if(Player.instance.GetComponent<PlayerHealth>().isAlive == false)
                {
                    camera.transform.parent = null;
                }
                PoolManager.instance.Despawn(this.gameObject);
            }
        }
    }

    public void TakeLife(int amount)        //Pilla vida (esto puede ser como ejercicio opcional para subir nota)
    {
        _currentHealth += amount;

        var healthProportion = _currentHealth / (float)maxHealth;

        if (healthProportion > 1)
        {
            healthProportion = 1;
        }

        Color targetColor = new Color(1, healthProportion, healthProportion, 1);
        _material.DOColor(Color.red, 0.15f).SetLoops(3).onComplete = () => _material.color = targetColor;
    }

    private IEnumerator SetInvulnerable()       //Modifica vulnerabilidad
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityPeriod);
        isInvulnerable = false;
    }

    public void Kill()      //Elimina vida completa del Player y la pasa al json
    {
        TakeDamage(_currentHealth);
        if (killsLogger != null)
            killsLogger.RegisterKill();

    }



    public void Stun(float duration)
    {
        if (!stunned)
            StartCoroutine(StunRoutine(duration));

    }

    private IEnumerator StunRoutine(float duration)
    {
        stunned = true;

        //Desavtiva mov y disp 
        movement.enabled = false;
        shooting.enabled = false;

        yield return new WaitForSeconds(duration);

        //Restaura
        movement.enabled = true;
        shooting.enabled = true;

        stunned = false;
    }
}
