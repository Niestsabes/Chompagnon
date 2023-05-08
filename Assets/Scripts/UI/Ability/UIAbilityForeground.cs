using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class UIAbilityForeground : MonoBehaviour
{
    public static UIAbilityForeground instance { get { return UIAbilityForeground._instance; } }
    private static UIAbilityForeground _instance;

    [Header("GameObject Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _foregroundImage;
    [SerializeField] private Image _backgroundImage;

    void Awake()
    {
        UIAbilityForeground._instance = this;
        this._animator.SetBool("isOpened", false);
    }

    public void Play()
    {
        this._animator.SetBool("isOpened", true);
    }

    public void Stop()
    {
        this._animator.SetBool("isOpened", false);
    }

    public void SetBackground(Color color)
    {
        this._backgroundImage.color = color;
    }

    public void SetForeground(Color color)
    {
        this._foregroundImage.color = color;
    }

}
