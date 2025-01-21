using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private readonly float _minMovingSpeed = 0.1f;

    private Rigidbody2D _rigidbody;
    private bool _isMoving = false;
    private float _lastDirection = 0f;

    public event Action<float> DirectionChanged;

    public bool IsMoving => _isMoving;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontalAxis)
    {
        SetVelocity(horizontalAxis * _moveSpeed);

        if (Mathf.Abs(horizontalAxis) > _minMovingSpeed)
        {
            _isMoving = true;

            if (_lastDirection != horizontalAxis)
            {
                _lastDirection = horizontalAxis;
                DirectionChanged?.Invoke(horizontalAxis);
            }
        }
        else
        {
            _isMoving = false;
        }
    }

    private void SetVelocity(float x)
    {
        _rigidbody.velocity = new Vector2(x, _rigidbody.velocity.y);
    }
}
