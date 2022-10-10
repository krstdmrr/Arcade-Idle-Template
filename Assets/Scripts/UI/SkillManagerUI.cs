using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;
public class SkillManagerUI : ComponentUIAbstract
{
    #region Delegate Events
    public delegate void SkillManagement();
    public SkillManagement IncLimitEvent;

    public delegate void ShoppingEvent();
    public ShoppingEvent ShoppingIncLimit;
    public ShoppingEvent ShoppingDecDuration;
    #endregion


    #region Serialized Fields
    [SerializeField] private ItemHolderScriptableObject _itemHolderSO;
    [SerializeField] private TMP_Text _limitIncText;
    [SerializeField] private TMP_Text _DurationDecText;
    #endregion

    #region Private Fields
    private GameObject _skillManagerPanel;
    private RectTransform _skillManagerPanelRect;
    private float _limitIncPrice, _durationDývPrice;
    #endregion

    #region Init Methods
    public override void Init(InGameUIComponents inGameUIComponents)
    {
        inGameUIComponents.AddUIComponent(InGameUITypes.BuySkill, this); ;
    } 
    #endregion
    #region Unity Methods
    private void Start()
    {
        _skillManagerPanel = transform.GetChild(0).gameObject;
        _skillManagerPanelRect = _skillManagerPanel.GetComponent<RectTransform>();
        SetPrizes();
        SetText();
    }
    #endregion

    #region UI Setter Methods
    private void SetText()
    {
        _limitIncText.text = IncLimitToString();
        _DurationDecText.text = DecDurationToString();
    }

    private void SetPrizes()
    {
        _limitIncPrice = _itemHolderSO.ItemHolderIncrementPrice;
        _durationDývPrice = _itemHolderSO.JumpDurationDivPrice;
    }
    #endregion
    #region Toggle Panel Methods
    public void EnableSkillPanel()
    {
        _skillManagerPanel.SetActive(true);
    }

    public void DisableSkillPanel()
    {
        _skillManagerPanel.SetActive(false);
    }
    #endregion

    #region Buying Methods
    public void BuyIncLimit()
    {
        if (Wallet.SpendMoneyAction.Invoke(_limitIncPrice))
        {
            ShoppingIncLimit?.Invoke();
        }
        else
            ShakePanel();
    }
    public void BuyDecDuration()
    {
        if (Wallet.SpendMoneyAction.Invoke(_durationDývPrice))
        {
            ShoppingDecDuration?.Invoke();
        }
        else
            ShakePanel();
    }

    private void ShakePanel()
    {
        _skillManagerPanelRect.DOPunchAnchorPos(new Vector2(2, 2), 0.5f);
    } 
    #endregion

    #region ToString Methods
    private string IncLimitToString()
    {
        return "Holder Limit +" + _itemHolderSO.ItemHolderIncrement + " = " + _limitIncPrice + " $";
    }
    private string DecDurationToString()
    {
        return "Jump Duration Time / " + _itemHolderSO.JumpDurationDiv + " = " + _durationDývPrice + " $";
    } 
    #endregion



}
