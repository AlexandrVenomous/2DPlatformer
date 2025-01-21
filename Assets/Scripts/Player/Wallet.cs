using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _money = 0;

    public event Action<int> MoneyChanged;

    private void Start()
    {
        MoneyChanged?.Invoke(_money);
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        MoneyChanged?.Invoke(_money);
    }
}
