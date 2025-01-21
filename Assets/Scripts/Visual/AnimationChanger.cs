using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationChanger : MonoBehaviour
{
    private const string IsMoving = "IsMoving";
    private const string Jumped = "Jumped";
    private const string Landed = "Landed";

    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;

    private Animator _animator;
    private List<string> _parameters;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _parameters = InitiateParameters();
    }

    private void OnEnable()
    {
        _jumper.Jumped += SetJumpedTrigger;
        _jumper.Landed += SetLanded;
    }

    private void OnDisable()
    {
        _jumper.Jumped -= SetJumpedTrigger;
        _jumper.Landed -= SetLanded;
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

    private void SetIsMoving(bool value)
    {
        if (IsParametersContains(IsMoving))
            _animator.SetBool(IsMoving, value);
    }

    private void SetJumpedTrigger()
    {
        if (IsParametersContains(Jumped))
            _animator.SetTrigger(Jumped);
    }

    private void SetLanded(bool value)
    {
        if (IsParametersContains(Landed))
            _animator.SetBool(Landed, value);
    }

    private bool IsParametersContains(string name)
    {
        return _parameters.Contains(name);
    }
}
