using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Platformer.UI
{
    /// <summary>
    /// A simple controller for switching between UI panels.
    /// </summary>
    public class MainUIController : MonoBehaviour
    {
        public GameObject[] panels;
        public Button[] defaultButtons;
        public EventSystem eventSystem;

        public void SetActivePanel(int index)
        {
            for (var i = 0; i < panels.Length; i++)
            {
                var active = i == index;
                var g = panels[i];
                if (g.activeSelf != active) g.SetActive(active);
                if (active && this.defaultButtons[i]) eventSystem.SetSelectedGameObject(this.defaultButtons[i].gameObject);
            }
        }

        public void NavigateToMainMenu()
        {
            SceneManager.LoadScene("HomeScene");
        }

        void OnEnable()
        {
            SetActivePanel(0);
        }
    }
}