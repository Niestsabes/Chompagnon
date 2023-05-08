using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    public void NavigateToGame()
    {
        UISceneTransition.instance.CloseToScene("MovieScene");
    }

    public void NavigateToGallery()
    {
        UISceneTransition.instance.CloseToScene("GalleryScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
