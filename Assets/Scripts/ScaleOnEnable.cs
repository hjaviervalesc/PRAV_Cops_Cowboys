using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleOnEnable : MonoBehaviour, ISpawnable
{
    public float timeToScale = .5f;
    public Vector3 scaleVelocity = Vector3.one;

    protected Transform _transform;
    protected Vector3 _initialScale;

    public void OnSpawn()
    {
        _transform.DOScale(_initialScale, timeToScale);         //Hace uso de DOTween para escalar el objeto
    }

    public void OnDespawn()
    {
        _transform.localScale = _initialScale;      //Tamaño inicial al desactivar el objeto
    }

    void Awake()
    {
        _transform = GetComponent<Transform>();
        _initialScale = _transform.localScale;
    }
}
