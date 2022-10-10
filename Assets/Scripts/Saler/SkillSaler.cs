using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillSaler : MonoBehaviour, ICustomizable
{

    [SerializeField] private Image _filler;

    #region Private Fields
    private Wallet _wallet;
    private bool _fill;
    private WaitForEndOfFrame _waitToFill;
    private SkillManagerUI _skillManagerUI;
    #endregion

    #region Unity Methods
    void Start()
    {
        _skillManagerUI = (SkillManagerUI)UIManager.Instance.GetInGameUIComponent(InGameUITypes.BuySkill);
    }
    #endregion

    #region Toggle Saler Methods
    public void LoadSkillSaler()
    {
        _fill = true;
        StartCoroutine(FillLoading());
    }

    public void ResetSkillSaler()
    {
        _fill = false;
        _filler.fillAmount = 0;
        _skillManagerUI.DisableSkillPanel();
    }
    #endregion

    #region Loading Coroutine
    IEnumerator FillLoading()
    {
        while (_fill)
        {
            _filler.fillAmount += 1 * Time.deltaTime;
            if (_filler.fillAmount == 1)
            {
                _skillManagerUI.EnableSkillPanel();
            }
            yield return _waitToFill;
        }
    }
    #endregion
}
