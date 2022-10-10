using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoneyPlace : MonoBehaviour , ITakeableMoney
{
    #region Serialized Fields
    [SerializeField] private GameObject money;
    [SerializeField] private float _moneyValue;
    [SerializeField] private int moneyRow, moneyColumn;
    [SerializeField] private Transform moneyPlaceHolder;
    #endregion

    #region Private Fields
    private GameObject[] moneys;
    private float _moneyYScale;
    private int moneyCount;
    private int _count;
    private bool _collectedMoney = true; 
    #endregion

    public float MoneyValue { get => _moneyValue; }

    #region Unity Methods
    void Start()
    {
        moneyCount = moneyRow * moneyColumn;
        moneys = new GameObject[moneyCount];
        _moneyYScale = money.transform.GetChild(0).transform.lossyScale.y;

        CreateMoney();
    } 
    #endregion

    #region Money Mechanics
    private void CreateMoney()
    {
        for (int i = 0; i < moneyCount; i++)
        {
            GameObject moneyClone = Instantiate(money, this.transform);
            moneyClone.SetActive(false);
            moneyClone.name = "Money" + i;
            moneys[i] = moneyClone;
        }
    }

    public void MakeMoney()
    {
        if (_count < moneyCount)
        {
            moneys[_count].SetActive(true);
            moneys[_count].transform.localPosition = new Vector3(moneyPlaceHolder.localPosition.x + (((int)_count / moneyRow) % moneyRow) * 2, moneyPlaceHolder.localPosition.y + (_moneyYScale * (_count % 3)), moneyPlaceHolder.localPosition.z - ((int)_count / moneyColumn) * 2);
            moneys[_count].transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.35f).SetEase(Ease.InOutCubic);
            _count++;
            _count = Mathf.Clamp(_count, 0, moneyCount - 1);
        }


    }

    public void TakeMoney(Vector3 playerPos)
    {
        if (_collectedMoney)
        {
            _collectedMoney = false;
            moneys[_count].transform.DOJump(playerPos, 1, 1, 0.1f)
                .OnComplete(() =>
                {
                    moneys[_count].SetActive(false);
                    if (_count > 0)
                        _count--;
                    _collectedMoney = true;
                });
        }
    }

    public bool isActive()
    {
        return moneys[0].activeInHierarchy;
    } 
    #endregion

}

