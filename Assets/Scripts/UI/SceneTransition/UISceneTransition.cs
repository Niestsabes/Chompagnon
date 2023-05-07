using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class UISceneTransition : MonoBehaviour
{
    public static UISceneTransition instance { get { return UISceneTransition._instance; } }
    private static UISceneTransition _instance;

    private Animator _animator;
    private bool _isTransitionning = false;
    private string _sceneName;

    void Awake()
    {
        UISceneTransition._instance = this;
        this._animator = this.GetComponent<Animator>();    
    }

    public void Open()
    {
        this._animator.SetBool("isOpened", true);
    }

    public void Close()
    {
        this._animator.SetBool("isOpened", false);
    }

    public void CloseToScene(string scene)
    {
        if (this._isTransitionning) return;
        this._isTransitionning = true;
        this._sceneName = scene;
        this.Close();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(this._sceneName);
        this._isTransitionning = false;
    }

}
