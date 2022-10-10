using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using UnityEngine.UI;

public abstract class Converter : MonoBehaviour, IConvertable, IDropable
{
    #region Serialized Fields
    [SerializeField] protected float convertTime;
    [SerializeField] protected float processTime;
    [SerializeField] protected Transform convertedItemsPlaceHolder;
    [SerializeField] protected ItemType _itemType;
    #endregion

    #region Protected Fields
    protected Stack<GameObject> convertableItemsStack;
    protected float _itemYScale;
    #endregion

    #region Private Fields
    private PoolContainer itempool;
    private MoneyPlace _moneyPlace;
    private WaitForSeconds _waitProcessTimeSeconds, _waitConvertTimeSeconds;
    #endregion

    #region Props
    public ItemType ItemType { get => _itemType; }
    #endregion


    #region Unity Methods
    private void Start()
    {
        itempool = PoolContainer.Instance;
        _itemYScale = itempool.ItemScale(_itemType).y;
        _moneyPlace = transform.parent.GetComponentInChildren<MoneyPlace>();
        convertableItemsStack = new Stack<GameObject>();
        _waitProcessTimeSeconds = new WaitForSeconds(processTime);
        _waitConvertTimeSeconds = new WaitForSeconds(convertTime);
        StartCoroutine(ProcessPhase());
    }
    #endregion

    #region Coroutine Methods
    public virtual IEnumerator ProcessPhase()
    {
        while (true)
        {
            yield return _waitProcessTimeSeconds;
            StartCoroutine(Convert());
        }
    }
    public virtual IEnumerator Convert()
    {
        yield return _waitConvertTimeSeconds;
        if (convertableItemsStack.Count > 0)
        {
            convertedItemsPlaceHolder.position -= new Vector3(0, _itemYScale, 0);
            itempool.AddItemToPool(ItemType, convertableItemsStack.Pop());
            _moneyPlace.MakeMoney();
        }
    }
    #endregion

    #region Drop Mechanic
    public virtual void Drop(GameObject item)
    {
        if (item.name.Equals(_itemType.ToString()))
        {
            convertedItemsPlaceHolder.position += new Vector3(0, _itemYScale, 0);
            item.transform.SetParent(null);
            item.transform.DOJump(convertedItemsPlaceHolder.position, 1, 1, 0.7f)
                .OnStart(() => item.transform.DORotateQuaternion(Quaternion.identity, 0.6f)
                .OnComplete(() => convertableItemsStack.Push(item)));
        }
    } 
    #endregion

}
