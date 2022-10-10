using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wallet")]
public class WalletScriptableObject : ScriptableObject
{
    [SerializeField] private float _spendingMoney;
    [SerializeField] private float _earningMoney;
    
    public float SpendingMoney { get => _spendingMoney; private set => _spendingMoney = value; }
    public float EarningMoney { get => _earningMoney; private set => _earningMoney = value; }
}
