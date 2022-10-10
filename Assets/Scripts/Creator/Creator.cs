using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Creator : MonoBehaviour, ICreatable
{
    #region Serialized Fields
    [SerializeField] protected Transform _startPos;
    [SerializeField] protected int _maxRowCount, _maxColumnCount;
    [SerializeField] protected float _createTime, _xInvertal, _yInvertal;
    [SerializeField] protected ItemType itemType;
    #endregion

    #region Protected Fields
    protected PoolContainer _itempool;
    protected int _count = 0;
    protected float _itemYScale;
    protected Vector3 _spawnPos;
    #endregion

    #region Private Fields
    private Stack<GameObject> _createdItemList;
    private WaitForSeconds _waitCreateTimeSeconds; 
    #endregion

    #region Unity Methods
    protected virtual void Start()
    {
        _itempool = PoolContainer.Instance;
        _waitCreateTimeSeconds = new WaitForSeconds(_createTime);
        _createdItemList = new Stack<GameObject>();
        _spawnPos = _startPos.position;
        _itemYScale = _itempool.ItemScale(itemType).y * 2;
        StartCoroutine(OnStart());
    }
    #endregion

    #region Creator Mechanics
    public void CreateItems()
    {
        if (_itempool.CheckITemFromPool(itemType) && _count < _maxRowCount * _maxColumnCount)
        {
            GameObject item = _itempool.GetItemFromPool(itemType);

            SortCreatedItems(item);
            item.transform.SetParent(this.transform);
            item.SetActive(true);
            _createdItemList.Push(item);
            _count++;
        }
    }
    public virtual void SortCreatedItems(GameObject item)
    {
        _spawnPos = new Vector3(_startPos.position.x + ((int)_count / _maxRowCount) * _xInvertal,
                        _startPos.position.y + _itemYScale * (_count % _maxRowCount) * _yInvertal,
                        _startPos.position.z);
        item.transform.position = _spawnPos;
    }
    public GameObject GetLastItem()
    {
        if (_createdItemList.Count > 0)
        {
            _count--;
            return _createdItemList.Pop();
        }
        else
            return null;

    }
    #endregion

    #region Start Coroutine
    private IEnumerator OnStart()
    {
        while (true)
        {
            CreateItems();
            yield return _waitCreateTimeSeconds;
        }
    }
    #endregion


}
