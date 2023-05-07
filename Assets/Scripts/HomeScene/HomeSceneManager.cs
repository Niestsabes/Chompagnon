using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    public void NavigateToGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void NavigateToGallery()
    {
        SceneManager.LoadScene("GalleryScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
