using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Movie", menuName = "Movie Script")]
public class MovieScript : ScriptableObject
{
    [SerializeField] private Sprite[] _listSprite;
    [SerializeField] private string[] _listText;

    public List<MovieFrame> GetFrames()
    {
        List<MovieFrame> listFrame = new List<MovieFrame>();
        for (int i = 0; i < this._listSprite.Length; i++) {
            listFrame.Add(new MovieFrame() { sprite = this._listSprite[i], text = this._listText[i] });
        }
        return listFrame;
    }
}

public class MovieFrame
{
    public Sprite sprite { get; set; }
    public string text { get; set; }
}