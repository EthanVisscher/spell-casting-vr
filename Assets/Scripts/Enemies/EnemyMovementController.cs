using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;

    private Rigidbody rb;
    private bool isGrounded;

    private Animator _animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(-transform.right);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(transform.right);
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(transform.forward);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(-transform.forward);
            }
        }

        _animator.SetFloat("ForwardVelocity", Vector3.Dot(rb.velocity, transform.forward));

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -150f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, 150f * Time.deltaTime);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
