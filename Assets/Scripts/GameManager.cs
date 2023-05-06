using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using Platformer.Model;
using Platformer.Mechanics;
using UnityEngine.InputSystem;
using UnityEditor.Rendering.LookDev;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject _camManager;
    public Transform[] PlayersTransform;
    public bool Attached = true;
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

    public void Detach(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;
        if (Attached)
        {
            if (_currentSquirrel == 1)
                PlayersTransform[_currentSquirrel - 1].gameObject.GetComponent<PlayerController>().controlEnabled = false;
            else
                PlayersTransform[_currentSquirrel + 1].gameObject.GetComponent<PlayerController>().controlEnabled = false;
            PlayersTransform[_currentSquirrel].DetachChildren();
            Attached = !Attached;
        }
        else if (_currentSquirrel == 1 && PlayersTransform[_currentSquirrel].position.x >= PlayersTransform[_currentSquirrel - 1].position.x - 0.25f && PlayersTransform[_currentSquirrel].position.x <= PlayersTransform[_currentSquirrel - 1].position.x + 0.25f)
        {
            PlayersTransform[_currentSquirrel - 1].parent = PlayersTransform[_currentSquirrel];
            PlayersTransform[_currentSquirrel - 1].gameObject.GetComponent<PlayerController>().controlEnabled = true;
            Attached = !Attached;
        }
        else if (_currentSquirrel == 0 && PlayersTransform[_currentSquirrel].position.x >= PlayersTransform[_currentSquirrel + 1].position.x - 0.25f && PlayersTransform[_currentSquirrel].position.x <= PlayersTransform[_currentSquirrel + 1].position.x + 0.25f)
        {
            PlayersTransform[_currentSquirrel + 1].parent = PlayersTransform[_currentSquirrel];
            PlayersTransform[_currentSquirrel + 1].gameObject.GetComponent<PlayerController>().controlEnabled = true;
            Attached = !Attached;
        }
    }

    public void SwitchSquirrel(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;
        switch (_currentSquirrel)
        {
            case 0:
                PlayersTransform[_currentSquirrel + 1].gameObject.GetComponent<SpriteRenderer>().sortingOrder += 1;
                PlayersTransform[_currentSquirrel].DetachChildren();
                
                if (Attached)
                {
                    PlayersTransform[_currentSquirrel].SetParent(PlayersTransform[_currentSquirrel + 1]);
                    PlayersTransform[_currentSquirrel].parent.TransformPoint(PlayersTransform[_currentSquirrel].position.x - 0.2f, PlayersTransform[_currentSquirrel].position.y, 0);
                }
                else
                {
                    PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>().controlEnabled = false;
                    PlayersTransform[_currentSquirrel + 1].gameObject.GetComponent<PlayerController>().controlEnabled = true;
                }
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerInput>().enabled = false;
                _currentSquirrel++;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerInput>().enabled = true;

                break;
            case 1:
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<SpriteRenderer>().sortingOrder -= 1;
                PlayersTransform[_currentSquirrel].DetachChildren();
                if (Attached)
                {
                    PlayersTransform[_currentSquirrel].SetParent(PlayersTransform[_currentSquirrel - 1]);
                    PlayersTransform[_currentSquirrel].parent.TransformPoint(PlayersTransform[_currentSquirrel].position.x - 0.2f, PlayersTransform[_currentSquirrel].position.y, 0);
                    PlayersTransform[_currentSquirrel - 1].position = new Vector2(PlayersTransform[_currentSquirrel - 1].position.x + 0.2f, PlayersTransform[_currentSquirrel - 1].position.y);
                }
                else
                {
                    PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>().controlEnabled = false;
                    PlayersTransform[_currentSquirrel-1].gameObject.GetComponent<PlayerController>().controlEnabled = true;
                }
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerInput>().enabled = false;
                _currentSquirrel--;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerInput>().enabled = true;

                break;
        }
        SwitchCameraTarget();
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
