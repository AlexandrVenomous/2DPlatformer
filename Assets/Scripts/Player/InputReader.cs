using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";

    public event Action<float> AxisSet;
    public event Action JumpButtonPressed;

    private void Update()
    {
        ListenAxis();
        ListenJumpButton();
    }

    private void ListenAxis()
    {
        AxisSet?.Invoke(Input.GetAxisRaw(Horizontal));
    }

    private void ListenJumpButton()
    {
        if (Input.GetButton(Jump))
            JumpButtonPressed?.Invoke();
    }
}
