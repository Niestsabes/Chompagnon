using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject _camManager;
    public Transform[] PlayersTransform;
    public int _currentSquirrel;
    public int[] _lifes;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        _lifes = new int[2];
        _lifes[0] = 1;
        _lifes[1] = 1;
    }

    public void GameOver()
    {

    }

    public void SwitchCameraTarget()
    {
        _camManager.GetComponent<CinemachineVirtualCamera>().Follow = PlayersTransform[_currentSquirrel];
        _camManager.GetComponent<CinemachineVirtualCamera>().LookAt = PlayersTransform[_currentSquirrel];
    }

    public void DecreaseHealth()
    {
        _lifes[_currentSquirrel] -= 1;
        if (_lifes[_currentSquirrel] == 0)
        {
            PlayersTransform[_currentSquirrel].gameObject.SetActive(false);
            switch (_currentSquirrel)
            {
                case 0:
                    _currentSquirrel++;
                    break;
                case 1:
                    _currentSquirrel--;
                    break;
            }

            if(_lifes[_currentSquirrel] == 0)
            {
                GameOver();
            }
            else
            {
                SwitchCameraTarget();
            }
        }
    }
}
