using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private AnimList _animNowSelect = AnimList.IDLE;
    
    public void PlayAnim(AnimList animName)
    {
        if (_animNowSelect == animName)
            return;

        foreach (AnimList item in (AnimList[])Enum.GetValues(typeof(AnimList)))
            animator.SetBool(item.ToString(), item == animName);

        _animNowSelect = animName;
    }
}
