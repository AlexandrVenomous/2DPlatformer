using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Jumper), typeof(Wallet))]
public class Player : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string JUMP = "Jump";

    private Mover _mover;
    private Jumper _jumper;
    private Wallet _wallet;

    public event Action<Coin> CoinCollected; 

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _wallet = GetComponent<Wallet>();
    }

    private void Update()
    {
        _mover.Move(Input.GetAxisRaw(HORIZONTAL));

        if (Input.GetButton(JUMP))
            _jumper.Jump();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Coin coin))
        {
            CoinCollected?.Invoke(coin);
            _wallet.AddMoney(coin.GetDenomination());
        }
    }
}
