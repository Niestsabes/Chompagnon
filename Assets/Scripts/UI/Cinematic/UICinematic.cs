using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UICinematic : MonoBehaviour
{
    [Header("GameObject Components")]
    [SerializeField] private Animator _animator;

    public void Open()
    {
        this._animator.SetBool("isOpened", true);
    }

    public void Close()
    {
        this._animator.SetBool("isOpened", false);
    }
}
