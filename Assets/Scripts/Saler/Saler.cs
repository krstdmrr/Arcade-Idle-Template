using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class Saler : MonoBehaviour, IBuyable
{
    #region Serialized Fields
    [SerializeField] private GameObject _lockedMachine;
    [SerializeField] private WalletScriptableObject _walletSO;
    [SerializeField] private TextMeshProUGUI _priceGUI;
    [SerializeField] private Image _filler;
    [SerializeField] private float _priceValue;
    [SerializeField] private Transform _teleportArea;
    #endregion

    #region Private Fields
    private GameObject _player;
    private float _price;
    #endregion

    #region Unity Methods
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _price = _priceValue;
        _priceGUI.text = _price.ToString("00");
    }
    #endregion

    #region Saling Mechanics
    public void SpendMoney()
    {
        if (_price > 0)
        {
            UpdatePrice();
            FillProcess();
        }
        else
        {
            gameObject.tag = "Untagged";
            UnlockedMachine();
            Destroy(gameObject);
        }
    }

    private void UpdatePrice()
    {
        _price -= _walletSO.SpendingMoney * Time.deltaTime;
        _price = Mathf.Clamp(_price, 0, _priceValue);
        _priceGUI.text = _price.ToString("00");
    }

    private void FillProcess()
    {
        _filler.fillAmount += _walletSO.SpendingMoney / _priceValue * Time.deltaTime;
    }

    private void UnlockedMachine()
    {
        _player.transform.position = _teleportArea.position;
        _lockedMachine.SetActive(true);
        _lockedMachine.transform.SetParent(null);

    } 
    #endregion
}
