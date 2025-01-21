using UnityEngine;

public class RotationChanger : MonoBehaviour
{
    [SerializeField] private Mover _mover;

    private readonly float _noRotation = 0f;
    private readonly float _halfOfFullTurnRotation = 180f;

    private void OnEnable()
    {
        _mover.DirectionChanged += ChangeRotation;
    }

    private void OnDisable()
    {
        _mover.DirectionChanged -= ChangeRotation;
    }

    private void ChangeRotation(float direction)
    {
        Quaternion rotation = transform.rotation;
        rotation.y = direction > _noRotation ? _noRotation : _halfOfFullTurnRotation;
        transform.rotation = rotation;
    }
}
