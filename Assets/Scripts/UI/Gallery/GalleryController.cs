using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RectTransform))]
public class GalleryController : MonoBehaviour
{
    [Header("Settings")]
    public Vector2 minCoordinates = Vector2.zero;
    public Vector2 maxCoordinates = Vector2.one;
    public float moveSpeed = 2f;

    private RectTransform _rectTransform;
    private Vector2 _navDirection;

    void Awake()
    {
        this._rectTransform = this.GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 baseMove = this._navDirection * moveSpeed;
        //Vector2 toMin = new Vector2(this._rectTransform.position.x, this._rectTransform.position.y) - minCoordinates;
        //Vector2 toMax = maxCoordinates - new Vector2(this._rectTransform.position.x, this._rectTransform.position.y);
        //baseMove.x = Mathf.Max(toMin.x, baseMove.x);
        //baseMove.y = Mathf.Max(toMin.y, baseMove.y);
        //baseMove.x = Mathf.Min(toMax.x, baseMove.x);
        //baseMove.y = Mathf.Min(toMax.y, baseMove.y);
        this._rectTransform.Translate(baseMove);
    }

    public void OnNavigate(InputValue value)
    {
        this._navDirection = -value.Get<Vector2>();
    }

    public void OnCancel(InputValue value)
    {
        SceneManager.LoadScene("HomeScene");
    }
}
