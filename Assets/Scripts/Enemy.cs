using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speedEnemy = 2, sightRange = 12, sightAngle = 120, timeToFire = 3;
    public GameObject bulletPrefab;
    public Transform _tipTransform;

    private Transform _playerTransform;
    protected Transform _transform;

    void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
        _transform.position = transform.position - new Vector3(0f, _transform.position.y, 0f);
        _playerTransform = Player.instance.transform;
    }


    private void Start()
    {
        PoolManager.instance.Load(bulletPrefab, 5);
    }
    // Update is called once per frame
    void Update()
    {
        _transform.LookAt(_playerTransform);     //Enemy movement
        _transform.position += _transform.forward * speedEnemy * Time.deltaTime;
    }

    private void OnEnable()
    {
        StartCoroutine(CheckShouldFire());
    }

    /*private void OnTriggerEnter(Collider other)               //El trigger se quita para gestionar el daño desde DamageOnContact
    {
        if(other.gameObject.GetComponent<Player>() != null && other.gameObject.GetComponent<TypeOfCharacter>().targetType == TypeOfCharacter.TargetType.Player)
        {
            Destroy(other.gameObject);
            PoolManager.instance.Despawn(this.gameObject);
        }
    }*/

    private IEnumerator CheckShouldFire()
    {
        yield return null;
        while(true)
        {
            var directionToPlayer = _playerTransform.position - _transform.position;
            var distanceToPlayer = directionToPlayer.magnitude;

            if(distanceToPlayer <= sightRange && Vector3.Angle(_transform.forward, directionToPlayer) <= sightAngle / 2 )
            {

                var bullet = PoolManager.instance.Spawn(bulletPrefab);
                bullet.GetComponent<TypeOfCharacter>().targetType = TypeOfCharacter.TargetType.Player;
                var bulletTransform = bullet.transform;
                bulletTransform.position = _tipTransform.position;
                bulletTransform.forward = directionToPlayer;
                yield return new WaitForSeconds(timeToFire);

            }
            else
            {
                yield return null;
            }
        }
    }

}
