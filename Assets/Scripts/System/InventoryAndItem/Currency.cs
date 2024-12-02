using UnityEngine;

public enum CurrencyType
{
    Soul,
    Silver
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Currency")]
public class Currency : ItemData
{
    public CurrencyType currencyType;
}
