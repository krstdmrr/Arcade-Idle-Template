using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<ComponentUIAbstract> _UIList = new List<ComponentUIAbstract>();

    private InGameUIComponents _inGameUIComponents;
    void Awake()
    {
        _inGameUIComponents = new InGameUIComponents();
        InitUIs();
    }

    public ComponentUIAbstract GetInGameUIComponent(InGameUITypes inGameUIComponent)
    {
        return _inGameUIComponents.GetUIComponent(inGameUIComponent);
    }

    private void InitUIs()
    {
        foreach (var item in _UIList)
        {
            item.Init(_inGameUIComponents);
        }
    }


}
