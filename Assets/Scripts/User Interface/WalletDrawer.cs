using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WalletDrawer : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _wallet.MoneyChanged += DrawMoney;
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged -= DrawMoney;
    }

    private void DrawMoney(int money)
    {
        _text.text = money.ToString();
    }
}
