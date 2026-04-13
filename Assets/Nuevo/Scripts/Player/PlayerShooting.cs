using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform _tipTransform;
    private Transform _transform;
    [SerializeField] private JSONLogger shotLogger;

    private void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PoolManager.instance.Load(bulletPrefab, 5);
    }

    // Update is called once per frame
    void Update()
    {
        //Disparo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instanciamos una bala
            var bullet = PoolManager.instance.Spawn(bulletPrefab);
            bullet.GetComponent<TypeOfCharacter>().targetType = TypeOfCharacter.TargetType.Enemy;
            var bulletTransform = bullet.transform;
            bulletTransform.position = _tipTransform.position;
            bulletTransform.rotation = _transform.rotation;

            if (shotLogger != null)
                shotLogger.RegisterShot();
        }
    }
}
