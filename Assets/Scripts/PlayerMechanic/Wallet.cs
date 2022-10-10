using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Wallet : MonoBehaviour
{
    public static Func<float, bool> SpendMoneyAction;
    [SerializeField] private WalletScriptableObject _walletSO;

    #region Private Fields
    private float _totalMoney, _earnMoney, _spendMoney;
    private MoneyAreaUI _moneyAreaUI;
    private IBuyable _currentSaler;
    private ITakeableMoney _currentMoneyPlace; 
    #endregion

    public float TotalMoney { get => _totalMoney; private set => _totalMoney = value; }

    #region Unity Methods
    private void Awake()
    {
        _moneyAreaUI = (MoneyAreaUI)UIManager.Instance.GetInGameUIComponent(InGameUITypes.MoneyArea) as MoneyAreaUI;
    }
    private void Start()
    {
        _moneyAreaUI?.UpdateMoneyAction(_totalMoney);
        _earnMoney = _walletSO.EarningMoney;
        _spendMoney = _walletSO.SpendingMoney;
        SpendMoneyAction += SpendMoney;
    }
    #endregion

    #region Money Mechanics
    private bool SpendMoney(float value)
    {
        if (value > TotalMoney)
            return false;
        else
        {
            _totalMoney -= value;
            UpdateMoneyUI();
            return true;
        }
    }

    private void DecreaseMoney()
    {
        _totalMoney -= _walletSO.SpendingMoney * Time.deltaTime;
        UpdateMoneyUI();
    }

    private void IncreaseMoney()
    {
        _totalMoney += _walletSO.EarningMoney;
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        _totalMoney = Mathf.Clamp(_totalMoney, 0, Mathf.Infinity);
        _moneyAreaUI?.UpdateMoneyAction(_totalMoney);
    }
    #endregion
    #region OnTrigger Methods
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CollectMoney") && _currentMoneyPlace.isActive())
        {
            _currentMoneyPlace.TakeMoney(this.transform.position + new Vector3(0, 0.5f, 0));

        }
        if (_totalMoney > 0 && other.CompareTag("Saler"))
        {
            _currentSaler.SpendMoney();
            DecreaseMoney();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ITakeableMoney>(out ITakeableMoney moneyPlace))
        {
            _currentMoneyPlace = moneyPlace;
        }
        if (other.TryGetComponent<IBuyable>(out IBuyable saler))
        {
            _currentSaler = saler;
        }
        if (other.TryGetComponent<ICustomizable>(out ICustomizable custom))
        {
            custom.LoadSkillSaler();
        }
        if (other.gameObject.CompareTag("Money"))
        {
            IncreaseMoney();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ITakeableMoney>() != null)
        {
            _currentMoneyPlace = null;
        }
        if (other.GetComponent<IBuyable>() != null)
        {
            _currentSaler = null;
        }
        if (other.TryGetComponent<ICustomizable>(out ICustomizable custom))
        {
            custom.ResetSkillSaler();
        }

    }
    #endregion
}
