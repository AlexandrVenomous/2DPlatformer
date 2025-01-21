using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Legs : MonoBehaviour
{
    private bool _isLanded = false;

    public event Action Landed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isLanded == false)
        {
            _isLanded = true;
            Landed?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isLanded = false;
    }
}
