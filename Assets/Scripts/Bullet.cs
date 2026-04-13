using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 12;
    public float timeToDespawn = 5;

    protected Transform _transform;
    protected TypeOfCharacter _targetType;

    private void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
        _targetType = gameObject.GetComponent<TypeOfCharacter>();
    }

    private void OnEnable()
    {
        StartCoroutine(IDespawn());
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position += _transform.forward * speed * Time.deltaTime;
    }

    private IEnumerator IDespawn()
    {
        yield return new WaitForSeconds(timeToDespawn);
        PoolManager.instance.Despawn(this.gameObject);
    }

    /*private void OnTriggerEnter(Collider other)                   //Con trigger. Tiene que estar el is trigger de las balas activos           Se quita el trigger para gestionar el daño desde DamageOnContact
     {
        if ((other.gameObject.GetComponent<Enemy>() != null) || (other.gameObject.GetComponent<Player>() != null))
        {
            PoolManager.instance.Despawn(gameObject);
            if (other.gameObject.GetComponent<Enemy>())
            {
                PoolManager.instance.Despawn(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
            //_manager._enemies.RemoveAll(s => s == null);
        }
    }*/
}
