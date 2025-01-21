using UnityEngine;

[RequireComponent(typeof(Wallet))]
public class CoinCollector : MonoBehaviour
{
    private Wallet _wallet;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Coin coin))
            _wallet.AddMoney(coin.GetDenomination());
    }
}
