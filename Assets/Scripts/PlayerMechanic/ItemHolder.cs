using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemHolder : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private Transform _holderPosition;
    [SerializeField] private ItemHolderScriptableObject _itemHolderSO;
    #endregion

    #region Private Fields
    private List<GameObject> _itemHolder;
    private Transform _lastItemTransform;
    private Vector3 _startPos;
    private float _itemScaleY, _tempTime, _jumpDuration;
    private int _holderCount, _holderLimit = 20;
    private bool _isSort = true, _activeDrop = true;
    private GameObject _currentItem;
    private string _currentItemType;
    private ICreatable _creator;
    private IDropable _converter;
    private SkillManagerUI _skillManagerUI;
    private WaitForSeconds _waitTakeItemTimeSeconds;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        _skillManagerUI = (SkillManagerUI)UIManager.Instance.GetInGameUIComponent(InGameUITypes.BuySkill);

    }
    private void Start()
    {
        _itemHolder = new List<GameObject>();
        _waitTakeItemTimeSeconds = new WaitForSeconds(1);
        _startPos = _holderPosition.position;
        _holderLimit = _itemHolderSO.ItemHolderLimit;
        _jumpDuration = _itemHolderSO.JumpDuration;
    }
    #endregion

    private void OnEnable()
    {
        _skillManagerUI.ShoppingIncLimit += IncreaseHolderLimit;
        _skillManagerUI.ShoppingDecDuration += DecreaseJumpDuration;
    }
    private void IncreaseHolderLimit()
    {
        _holderLimit += _itemHolderSO.ItemHolderIncrement;
    }

    private void DecreaseJumpDuration()
    {
        _jumpDuration /= _itemHolderSO.JumpDurationDiv;
    }

    #region Taking Item Methods
    private void PullItems(GameObject item)
    {
        if (item != null)
        {
            _itemScaleY = item.transform.GetChild(0).transform.localScale.y; // TODO: Getchild(0) will change original item y size after modelling item model
            SortItem();
            item.transform.DOJump(_holderPosition.position, 1, 1, 0.15f).OnStart(() =>
            {
                _itemHolder.Add(item);
                item.transform.SetParent(_holderPosition.parent);
                item.transform.DORotateQuaternion(_lastItemTransform.rotation, 0.15f).OnComplete(() =>
                 {
                     item.transform.position = _holderPosition.position;
                     item.transform.rotation = _lastItemTransform.rotation;
                     _isSort = true;
                     _holderCount++;
                 });
            });
        }
    }

    private IEnumerator TakeItem(ICreatable triggeredObject)
    {
        PullItems(triggeredObject.GetLastItem());
        yield return _waitTakeItemTimeSeconds;

    }
    #endregion

    #region Sort Methods
    private void SortItem()
    {
        _isSort = false;
        if (_itemHolder.Count < 2)
        {
            _holderPosition.position += new Vector3(0, _itemScaleY, 0);
            _lastItemTransform = _holderPosition;
        }
        else
        {
            _holderPosition.position += new Vector3(0, _itemScaleY, 0);
            _lastItemTransform = _itemHolder[_itemHolder.Count - 2].transform;
        }
    }

    private void SortAllItems()
    {
        _holderPosition.position = new Vector3(_holderPosition.position.x, _startPos.y, _holderPosition.position.z);
        for (int i = 0; i < _itemHolder.Count; i++)
        {
            _holderPosition.position += new Vector3(0, _itemScaleY, 0);
            _itemHolder[i].transform.position = _holderPosition.position;
        }
    }
    #endregion

    #region OnTrigger Methods
    private void OnTriggerStay(Collider other)
    {
        if (_holderCount < _holderLimit && other.CompareTag("Creator") && _isSort)
        {
            StartCoroutine(TakeItem(_creator));
        }

        if (_holderCount > 0 && other.CompareTag("Converter") && _activeDrop)
        {
            Drop();
            SortAllItems();
        }
    }

    private void Drop()
    {
        for (int i = _itemHolder.Count - 1; i >= 0; i--)
        {
            if (_itemHolder[i].name.Equals(_currentItemType))
            {
                _activeDrop = false;
                _currentItem = _itemHolder[i];
                _itemHolder.RemoveAt(i);
                _holderCount--;
                _converter.Drop(_currentItem);
                _tempTime = 5;
                DOTween.To(x => _tempTime = x, _tempTime, 0, _jumpDuration).OnComplete(() => _activeDrop = true);
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICreatable>(out ICreatable creator))
        {
            _creator = creator;
        }

        if (other.TryGetComponent<IDropable>(out IDropable converter))
        {
            _converter = converter;
            _currentItemType = _converter.ItemType.ToString();
        }
    }
    #endregion

}