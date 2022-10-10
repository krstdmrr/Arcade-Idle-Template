using UnityEngine;

public class BlueCreator : Creator
{
    private float _itemXScale;

    protected override void Start()
    {
        base.Start();
        _itemXScale = _itempool.ItemScale(itemType).x * 2;
    }
    public override void SortCreatedItems(GameObject item)
    {
        _spawnPos = new Vector3(_startPos.position.x + (_count % _maxColumnCount) * (_xInvertal * _itemXScale),
                _startPos.position.y + _itemYScale * ((int)_count / _maxColumnCount)*_yInvertal,
                _startPos.position.z);
        item.transform.position = _spawnPos;
    }
}
