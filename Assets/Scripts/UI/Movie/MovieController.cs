using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MovieController : MonoBehaviour
{
    [Header("GameObject Components")]
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    [Header("Settings")]
    [SerializeField] private MovieScript _movieScript;

    private int index = 0;
    private List<MovieFrame> _listFrame;

    void Awake()
    {
        if (this._movieScript == null) return;
        this._listFrame = this._movieScript.GetFrames();
        this.index = 0;
        this.Print(this.index);
    }

    public void Print(int index)
    {
        if (index >= this._listFrame.Count) { this.LoadNextScene(); return; }
        this._image.sprite = this._listFrame[index].sprite;
        this._text.text = this._listFrame[index].text;
    }

    public void OnSubmit(InputValue value)
    {
        if (value.Get<float>() == 0) return;
        this.index++;
        this.Print(this.index);
    }

    public void OnClick(InputValue value)
    {
        this.OnSubmit(value);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("LDPlayer");
    }
}