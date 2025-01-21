using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private Legs _legs;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private bool _isOnGround = false;

    public event Action Jumped;
    public event Action<bool> GroundCollidedChanged;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _legs.Landed += ChangeGroundCollided;
    }

    private void OnDisable()
    {
        _legs.Landed -= ChangeGroundCollided;
    }

    public void Jump()
    {
        if (_isOnGround)
        {
            SetVelocity(_jumpForce);
            Jumped?.Invoke();
            SetGroundCollided(false);
        }
    }

    private void ChangeGroundCollided()
    {
        SetGroundCollided(true);
    }

    private void SetGroundCollided(bool value)
    {
        _isOnGround = value;
        GroundCollidedChanged?.Invoke(value);
    }

    private void SetVelocity(float y)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, y);
    }
}
