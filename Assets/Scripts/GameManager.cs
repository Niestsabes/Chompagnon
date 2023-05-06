using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using Platformer.Model;
using Platformer.Mechanics;

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
        Spawn();
    }


    public void Spawn()
    {
        foreach (Transform transform in PlayersTransform)
        {
            transform.position = new Vector3(0 - transform.gameObject.GetComponent<PlayerController>().offSet, 0f, 0f);
            transform.gameObject.SetActive(true);
        }
        _lifes[0] = 1;
        _lifes[1] = 1;
        SwitchCameraTarget();
    }

    public void SwitchCameraTarget()
    {
        _camManager.GetComponent<CinemachineVirtualCamera>().Follow = PlayersTransform[_currentSquirrel];
        _camManager.GetComponent<CinemachineVirtualCamera>().LookAt = PlayersTransform[_currentSquirrel];

    }

    public void SwitchSquirrel()
    {
        GameObject Squirrel = PlayersTransform[_currentSquirrel].gameObject;
        Squirrel.GetComponent<SpriteRenderer>().sortingOrder--;
        switch (_currentSquirrel)
        {
            case 0:
                PlayersTransform[_currentSquirrel].SetParent(PlayersTransform[_currentSquirrel++]);
                Vector3 temp = PlayersTransform[_currentSquirrel].position;
                PlayersTransform[_currentSquirrel].position = PlayersTransform[_currentSquirrel++].position;
                PlayersTransform[_currentSquirrel++].position = temp;
                break;
            case 1:
                PlayersTransform[_currentSquirrel].SetParent(PlayersTransform[_currentSquirrel--]);
                Vector3 temps = PlayersTransform[_currentSquirrel].position;
                PlayersTransform[_currentSquirrel].position = PlayersTransform[_currentSquirrel--].position;
                PlayersTransform[_currentSquirrel--].position = temps;
                break;
        }

    }

    public void Death()
    {
        var playerController = PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>();
        //animation

        //playerController.controlEnabled = false;
        //if (playerController.audioSource && playerController.ouchAudio)
        //    playerController.audioSource.PlayOneShot(playerController.ouchAudio);
        //playerController.animator.SetTrigger("hurt");
        //playerController.animator.SetBool("dead", true);
        _lifes[_currentSquirrel]--;
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
        if (_lifes[_currentSquirrel] == 0)
        {
            GameOver();
        }
        else
        {
            SwitchCameraTarget();
        }
    }
}
