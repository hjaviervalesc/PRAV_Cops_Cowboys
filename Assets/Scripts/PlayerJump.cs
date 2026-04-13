using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpSpeed = 10;
    public GameObject floor;

    protected Transform _playerTransform;

    private bool inFloor = true;
    private Rigidbody rb;
    private Vector3 jump;


    private void Start()
    {
        _playerTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0f, 3f, 0f);     //Force to jump
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == floor)
        {
            inFloor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == floor)
        {
            inFloor = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && inFloor)      //Press J and is in floor
        {
            rb.AddForce(jump * jumpSpeed, ForceMode.Impulse);       //Jump player  --> to do this, iskinecmatic is disabled
        }
    }
}
