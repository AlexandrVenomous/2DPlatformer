using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(Jumper))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private Mover _mover;
    private Jumper _jumper;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
    }

    private void OnEnable()
    {
        _inputReader.AxisSet += Move;
        _inputReader.JumpButtonPressed += Jump;
    }

    private void OnDisable()
    {
        _inputReader.AxisSet -= Move;
        _inputReader.JumpButtonPressed -= Jump;
    }

    private void Move(float horizontalAxis)
    {
        _mover.Move(horizontalAxis);
    }

    private void Jump()
    {
        _jumper.Jump();
    }
}
