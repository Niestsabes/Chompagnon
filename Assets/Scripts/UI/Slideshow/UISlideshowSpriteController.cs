using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class UISlideshowSpriteController : UISlideshowController<Sprite>
{
    [Header("GameObject Components")]
    public Image displayedImage;

    void Awake()
    {
        UISlideshowSpriteController._instance = this;
        this._playerInput = GameObject.FindFirstObjectByType<PlayerInput>();
    }

    public override void Open(Sprite[] listSlide)
    {
        base.Open(listSlide);
        this.displayedImage.sprite = this._listSlide[this._currentSlideIdx];
    }

    public override void PrintSlide(Sprite slide)
    {
        this._pendingSlide = slide;
        this.animator.SetTrigger("switchText");
    }

    public override void PrintDefaultSlide()
    {
        this.PrintSlide(null);
    }

    public override void DisplayPendingSlide()
    {
        this.displayedImage.sprite = this._pendingSlide;
        base.DisplayPendingSlide();
        this._pendingSlide = null;
    }

}
