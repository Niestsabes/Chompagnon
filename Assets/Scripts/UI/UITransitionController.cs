using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class UITransitionController : MonoBehaviour
{
    public static UITransitionController instance { get { return UITransitionController._instance;  } }
    private static UITransitionController _instance;

    [Header("GameObject Components")]
    public CanvasGroup canvasGroup;
    public Animator animator;
    public Image backgroundImage;
    public TextMeshProUGUI text;

    [Header("Settings")]
    public float charPrintDelay = 0.05f;
    public int charPrintByTic = 5;

    private PlayerInput _playerInput;
    private string _pendingText;
    private bool _pendingClose = false;
    private bool _isListening = false;
    private string[] _listSlide;
    private int _currentSlideIdx;

    void Awake()
    {
        UITransitionController._instance = this;
        this._playerInput = GameObject.FindFirstObjectByType<PlayerInput>();
        this.Open(new string[] { "Hello there", "Bien le bonjour", "Comment ça va?", "Trés bien!" });
    }

    public void Open(string[] listText)
    {
        this._playerInput.SwitchCurrentActionMap("UI");
        this._currentSlideIdx = 0;
        this._listSlide = listText;
        this.text.text = this._listSlide[this._currentSlideIdx];
        this.animator.SetBool("isOpened", true);
        StartCoroutine(this.ListenController());
    }

    public void Close()
    {
        this._currentSlideIdx = 0;
        this._listSlide = new string[0];
        this._isListening = false;
        this.PrintText("");
        this._pendingClose = true;
    }

    public void PrintText(string text)
    {
        this._pendingText = text;
        this.animator.SetTrigger("switchText");
    }

    public void DisplayPendingText()
    {
        this.text.text = this._pendingText;
        if (this._pendingClose) {
            this.animator.SetBool("isOpened", false);
            this._playerInput.SwitchCurrentActionMap("Player");
        }
        this._pendingText = "";
    }

    private IEnumerator ListenController()
    {
        this._isListening = true;
        bool isClicked = false;
        do {
            if (this._playerInput.actions["Submit"].ReadValue<float>() > 0 && !isClicked) {
                isClicked = true;
                this.NextSlide();
                yield return new WaitForSeconds(0.800f);
            } else {
                isClicked = false;
            }
            yield return new WaitForFixedUpdate();
        } while (this._isListening);
    }

    private void NextSlide()
    {
        this._currentSlideIdx++;
        if (this._currentSlideIdx >= this._listSlide.Length) this.Close();
        else this.PrintText(this._listSlide[this._currentSlideIdx]);
    }
}
