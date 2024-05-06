using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _groundLayer;
    
    private Vector2 _direction;
    // private bool _isGrounded = false;
    private float _speed = 150.0f;
    private float _jumpPower = 7.0f;

    private bool _isGrounded;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _direction = Vector2.zero;
    }

    void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(x: _direction.x * _speed * Time.deltaTime, _rigidBody.velocity.y);

        _animator.SetFloat(name: "xVelocity", value: Math.Abs(_rigidBody.velocity.x));
        _animator.SetFloat(name: "yVelocity", value: _rigidBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        ContactPoint2D contact = collisionInfo.GetContact(0);
        
        if (contact.normal == Vector2.up)
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if ((1 << collisionInfo.collider.gameObject.layer) == _groundLayer)
        {
            _isGrounded = false;

            _animator.SetBool("isInAir", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();

        FlipSprite();

        ApplyJumping();
    }

    void FlipSprite()
    {
        if (_direction.x < 0)
        {
            _sprite.flipX = true;
        }
        else if (_direction.x > 0)
        {
            _sprite.flipX = false;
        }
    }

    void ApplyJumping()
    {
        if (Input.GetButtonDown(buttonName: "Jump") && _isGrounded)
        {
            _rigidBody.velocity = new Vector2(x: _rigidBody.velocity.x, _jumpPower);

            _animator.SetBool(name: "isInAir", true);

            _isGrounded = false;
        }
        else if (!_isGrounded)
        {
            _animator.SetBool(name: "isInAir", true);
        }
        else
        {
            _animator.SetBool(name: "isInAir", false);
        }
    }

    void GetDirection()
    {
        _direction = new Vector2(Input.GetAxisRaw(axisName: "Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
