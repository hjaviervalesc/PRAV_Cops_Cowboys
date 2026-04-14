using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4f;
    private Rigidbody rb;
    private Vector3 inputVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputVector = new Vector3(horizontal, 0, vertical).normalized;

        if (inputVector.magnitude > 0)
            transform.forward = inputVector;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputVector * movementSpeed * Time.fixedDeltaTime);
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