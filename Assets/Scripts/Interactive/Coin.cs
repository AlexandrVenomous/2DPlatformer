using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _denomination;

    public event Action<Coin> Collected;

    public int GetDenomination()
    {
        Collected?.Invoke(this);
        return _denomination;
    }
}
