using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private LandingDetector _landingDetector;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private bool _isLanded = false;

    public event Action Jumped;
    public event Action<bool> Landed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _landingDetector.Landed += ChangeLanded;
    }

    private void OnDisable()
    {
        _landingDetector.Landed -= ChangeLanded;
    }

    public void Jump()
    {
        if (_isLanded)
        {
            SetVelocity(_jumpForce);
            Jumped?.Invoke();
            SetLanded(false);
        }
    }

    private void ChangeLanded()
    {
        SetLanded(true);
    }

    private void SetLanded(bool value)
    {
        _isLanded = value;
        Landed?.Invoke(value);
    }

    private void SetVelocity(float y)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, y);
    }
}
