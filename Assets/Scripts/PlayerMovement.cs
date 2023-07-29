using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 20f;

    private Animator _animator;
    private Rigidbody _rb;
    private Vector3 _movement;
    private Quaternion _rot = Quaternion.identity;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    void Start ()
    {
        _animator = GetComponent<Animator> ();
        _rb = GetComponent<Rigidbody> ();
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        _movement.Set(horizontal, 0f, vertical);
        _movement.Normalize ();

        _animator.SetBool (IsWalking, _movement != Vector3.zero);

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, _movement, turnSpeed * Time.deltaTime, 0f);
        _rot = Quaternion.LookRotation (desiredForward);
    }

    void OnAnimatorMove ()
    {
        _rb.MovePosition (_rb.position + _movement * _animator.deltaPosition.magnitude * moveSpeed);
        _rb.MoveRotation (_rot);
    }
}
