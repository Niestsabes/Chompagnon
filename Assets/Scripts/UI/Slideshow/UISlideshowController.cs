using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public abstract class UISlideshowController<T> : MonoBehaviour
{
    public static UISlideshowController<T> instance { get { return UISlideshowController<T>._instance; } }
    protected static UISlideshowController<T> _instance;

    [Header("GameObject Components")]
    public CanvasGroup canvasGroup;
    public Animator animator;
    public Image backgroundImage;

    protected PlayerInput _playerInput;
    protected bool _pendingClose = false;
    protected bool _isListening = false;
    protected int _currentSlideIdx;
    protected T[] _listSlide;
    protected T _pendingSlide;

    void Awake()
    {
        this._playerInput = GameObject.FindFirstObjectByType<PlayerInput>();
    }

    public virtual void Open(T[] listSlide)
    {
        this._playerInput.SwitchCurrentActionMap("UI");
        this._currentSlideIdx = 0;
        this._listSlide = listSlide;
        this.animator.SetBool("isOpened", true);
        StartCoroutine(this.ListenController());
    }

    public virtual void Close()
    {
        this._currentSlideIdx = 0;
        this._listSlide = new T[0];
        this._isListening = false;
        this.PrintDefaultSlide();
        this._pendingClose = true;
    }

    public abstract void PrintSlide(T slide);

    public abstract void PrintDefaultSlide();

    public virtual void DisplayPendingSlide()
    {
        if (this._pendingClose)
        {
            this.animator.SetBool("isOpened", false);
            this._playerInput.SwitchCurrentActionMap("Player");
        }
    }

    protected virtual IEnumerator ListenController()
    {
        this._isListening = true;
        bool isClicked = false;
        do
        {
            if (this._playerInput.actions["Submit"].ReadValue<float>() > 0 && !isClicked)
            {
                isClicked = true;
                this.NextSlide();
                yield return new WaitForSeconds(0.800f);
            }
            else
            {
                isClicked = false;
            }
            yield return new WaitForFixedUpdate();
        } while (this._isListening);
    }

    protected virtual void NextSlide()
    {
        this._currentSlideIdx++;
        if (this._currentSlideIdx >= this._listSlide.Length) this.Close();
        else this.PrintSlide(this._listSlide[this._currentSlideIdx]);
    }
}
