using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class MoneyAreaUI : ComponentUIAbstract
{
    public Action<float> UpdateMoneyAction;

    [SerializeField] private TMP_Text _moneyText;

    private float _money;

    public override void Init(InGameUIComponents inGameUIComponents)
    {
        inGameUIComponents.AddUIComponent(InGameUITypes.MoneyArea, this);
    }

    #region Unity Methods
    private void Awake()
    {
        UpdateMoneyAction += UpdateMoneyText;
    }

    void Start()
    {
        DOTween.SetTweensCapacity(1000, 100);
    } 
    #endregion

    private void UpdateMoneyText(float money)
    {
        DOTween.To(x => _money = x, _money, money, 0.5f).OnUpdate(() => _moneyText.text = _money.ToString("00"));
    }
}
