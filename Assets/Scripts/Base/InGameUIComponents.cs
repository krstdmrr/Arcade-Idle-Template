using System.Collections.Generic;

public class InGameUIComponents
{

    private Dictionary<InGameUITypes, ComponentUIAbstract> _subInGameUIs;


    public InGameUIComponents()
    {
        _subInGameUIs = new Dictionary<InGameUITypes, ComponentUIAbstract>();
    }

    public void AddUIComponent(InGameUITypes UIComponentType, ComponentUIAbstract UIComponent)
    {
        if (!_subInGameUIs.ContainsKey(UIComponentType))
        {
            _subInGameUIs.Add(UIComponentType, UIComponent);
        }
    }

    public ComponentUIAbstract GetUIComponent(InGameUITypes UIComponentType)
    {
        if (_subInGameUIs.ContainsKey(UIComponentType))
        {
            return _subInGameUIs[UIComponentType];
        }
        else
        {
            return null;
        }
    }
}
