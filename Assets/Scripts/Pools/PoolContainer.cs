using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolContainer : Singleton<PoolContainer>
{
    #region Pool Inner Class 
    [System.Serializable]
    public class Pool
    {
        public ItemType type;
        public GameObject prefab;
        public int maxSize;
    }
    #endregion

    [SerializeField]private List<Pool> pools;
    private Dictionary<ItemType, Queue<GameObject>> _poolContainerNew;

    #region MonoBehaviour Methods
    private void Awake()
    {
        _poolContainerNew = new Dictionary<ItemType, Queue<GameObject>>();
        foreach (Pool p in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < p.maxSize; i++)
            {
                GameObject go = Instantiate(p.prefab);
                go.name = p.type.ToString();
                go.SetActive(false);
                objectPool.Enqueue(go);
            }

            _poolContainerNew.Add(p.type, objectPool);
        }
    }
    #endregion

    #region Get From Pool
    public GameObject GetItemFromPool(ItemType type)
    {
        return _poolContainerNew[type]?.Dequeue();

    }

    public bool CheckITemFromPool(ItemType type)
    {
        return (_poolContainerNew.ContainsKey(type) && _poolContainerNew[type].Count > 0) ? true : false;
    }

    public Vector3 ItemScale(ItemType type)
    {
        foreach (Pool item in pools)
        {
            if (item.type.Equals(type))
                return item.prefab.transform.GetChild(0).transform.localScale; // TODO: Getchild(0) will change original item y size after modelling item model
        }
        return Vector3.zero;
    }
    #endregion

    #region Add Pool
    public void AddItemToPool(ItemType type, GameObject item)
    {
        item.SetActive(false);
        _poolContainerNew[type].Enqueue(item);
    } 
    #endregion
}
