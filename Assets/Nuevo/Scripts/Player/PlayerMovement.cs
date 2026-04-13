using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4f;
    private Transform _transform;

    private void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento del player
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var inputVector = new Vector3(horizontal, 0, vertical).normalized;

        _transform.position += inputVector * movementSpeed * Time.deltaTime;

        if (inputVector.magnitude > 0)
            _transform.forward = inputVector;

    }
    public void ModifySpeed(float multiplier, float duration)
    {
        movementSpeed *= multiplier;

        StartCoroutine(ResetSpeed(multiplier, duration));
    }

    private IEnumerator ResetSpeed(float multiplier, float duration)
    {
        yield return new WaitForSeconds(duration);
        movementSpeed /= multiplier;
    }
}
