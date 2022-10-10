using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    public void AnimateRun(bool isRun)
    {
        _anim.SetBool("Run", isRun);
    }
}
