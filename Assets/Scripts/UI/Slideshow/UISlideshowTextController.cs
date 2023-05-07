using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class UISlideshowTextController : UISlideshowController<string>
{
    [Header("GameObject Components")]
    public TextMeshProUGUI displayedText;

    [Header("Settings")]
    public float charPrintDelay = 0.05f;
    public int charPrintByTic = 5;

    void Awake()
    {
        UISlideshowTextController._instance = this;
        this._playerInput = GameObject.FindFirstObjectByType<PlayerInput>();
        this.Open(new string[] { "Hello there", "Bien le bonjour", "Comment ça va?", "Trés bien!" });
    }

    public override void Open(string[] listText)
    {
        base.Open(listText);
        this.displayedText.text = this._listSlide[this._currentSlideIdx];
    }

    public override void PrintSlide(string text)
    {
        this._pendingSlide = text;
        this.animator.SetTrigger("switchText");
    }

    public override void PrintDefaultSlide()
    {
        this.PrintSlide("");
    }

    public override void DisplayPendingSlide()
    {
        this.displayedText.text = this._pendingSlide;
        base.DisplayPendingSlide();
        this._pendingSlide = "";
    }

}
