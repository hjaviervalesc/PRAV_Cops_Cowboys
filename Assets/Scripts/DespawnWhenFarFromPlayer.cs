using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnWhenFarFromPlayer : MonoBehaviour
{
    public float despawnDistance = 12;
    protected Transform _targetTransform;
    protected Transform _transform;

    private void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
        _targetTransform = Player.instance.GetComponent<Transform>();
    }

    private void Update()
    {
        if(_targetTransform != null) 
        {
            var directionToPlayer = _targetTransform.position - _transform.position;
            var distanceToPlayer = directionToPlayer.magnitude;

            if(distanceToPlayer >= despawnDistance)
            {
                PoolManager.instance.Despawn(gameObject);
            }
        }
    }
}
