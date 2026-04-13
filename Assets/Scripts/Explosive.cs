using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Explosive : MonoBehaviour
{
    public float timeToExplode = 5, explosionRadius = 70;
    protected Material _material;
    protected Transform _transform;

    public static bool bunker = false;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;      
        _transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    private void OnDisable()
    {
        _material.color = Color.white;      //Se pone a blanco o color inicial si se desabilita
    }

    private void RemoveInSphere(Collider element)
    {
      var health = element.GetComponent<PlayerHealth>();      //Si tiene componente de Health elimina todo lo que hay dentro. Pulsar tecla como ejercicio y que tenga en cuenta si esta en bunker y el personaje, que sea como parte del ejercicio final
        if (health)
            health.Kill();
    }

    private IEnumerator ExplosionCoroutine()
    {
        yield return null;

        _material.DOColor(Color.red, timeToExplode);        //Cambia el color de la explosion
        yield return new WaitForSeconds(timeToExplode);

        foreach (var col in Physics.OverlapSphere(_transform.position, explosionRadius))
        {
            /*if (col.CompareTag("Player") && bunker == false)
                RemoveInSphere(col);
            if (col.CompareTag("Enemy"))*/
                RemoveInSphere(col);
        }

        PoolManager.instance.Despawn(gameObject);
    }
}
