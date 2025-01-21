using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Visualizer : MonoBehaviour
{
    private const string IS_MOVING = "IsMoving";
    private const string JUMPED = "Jumped";
    private const string GROUND_COLLIDED = "GroundCollided";

    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;

    private readonly float _flip = 0f;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private List<string> _parameters;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _parameters = InitiateParameters();
    }

    private void OnEnable()
    {
        _mover.DirectionChanged += ChangeFlip;
        _jumper.Jumped += SetJumpedTrigger;
        _jumper.GroundCollidedChanged += SetGroundCollided;
    }

    private void OnDisable()
    {
        _mover.DirectionChanged -= ChangeFlip;
        _jumper.Jumped -= SetJumpedTrigger;
        _jumper.GroundCollidedChanged -= SetGroundCollided;
    }

    private void Update()
    {
        SetIsMoving(_mover.IsMoving);
    }

    private List<string> InitiateParameters()
    {
        List<string> parameters = new();

        for (int i = 0; i < _animator.parameterCount; i++)
            parameters.Add(_animator.GetParameter(i).name);

        return parameters;
    }

    private void ChangeFlip(float direction)
    {
        _spriteRenderer.flipX = direction <= _flip;
    }

    private void SetIsMoving(bool value)
    {
        if (ParametersContains(IS_MOVING))
            _animator.SetBool(IS_MOVING, value);
    }

    private void SetJumpedTrigger()
    {
        if (ParametersContains(JUMPED))
            _animator.SetTrigger(JUMPED);
    }

    private void SetGroundCollided(bool value)
    {
        if (ParametersContains(GROUND_COLLIDED))
            _animator.SetBool(GROUND_COLLIDED, value);
    }

    private bool ParametersContains(string name)
    {
        return _parameters.Contains(name);
    }
}
