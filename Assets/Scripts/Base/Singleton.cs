using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static volatile T _instance;

    #region Static T Instance
    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(T)) as T;

            return _instance;
        }
    } 
    #endregion
}
