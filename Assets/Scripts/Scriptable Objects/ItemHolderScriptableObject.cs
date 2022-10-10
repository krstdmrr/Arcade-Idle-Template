using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Management/Item Holder Management",fileName ="ItemHolderSettings")]
public class ItemHolderScriptableObject : ScriptableObject
{
    #region SerializeFields
    [SerializeField] private int _itemHolderIncrement, _itemHolderLimit;
    [SerializeField] private float _jumpDuration, _jumpDurationDiv, _itemHolderIncrementPrice, _JumpDurationDivPrice;
    #endregion


    #region Props
    public int ItemHolderIncrement { get => _itemHolderIncrement; set => _itemHolderIncrement = value; }
    public int ItemHolderLimit { get => _itemHolderLimit; set => _itemHolderLimit = value; /* set from save system*/ }
    public float JumpDuration { get => _jumpDuration;  set => _jumpDuration = value; }
    public float JumpDurationDiv { get => _jumpDurationDiv; set => _jumpDurationDiv = value; }
    public float ItemHolderIncrementPrice { get => _itemHolderIncrementPrice; set => _itemHolderIncrementPrice = value; }
    public float JumpDurationDivPrice { get => _JumpDurationDivPrice; set => _JumpDurationDivPrice = value; }
    #endregion
}
