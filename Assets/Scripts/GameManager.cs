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
    public bool Atached = true;
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
        Spawn();
    }

    public void GameOver()
    {
        Spawn();
    }


    public void Spawn()
    {
        foreach (Transform transform in PlayersTransform)
        {
            transform.position = Vector3.zero;
            transform.gameObject.SetActive(true);
        }
        if (_currentSquirrel == 0)
            PlayersTransform[_currentSquirrel + 1].SetParent(PlayersTransform[_currentSquirrel]);
        else
            PlayersTransform[_currentSquirrel - 1].SetParent(PlayersTransform[_currentSquirrel]);


        _lifes[0] = 1;
        _lifes[1] = 1;
        SwitchCameraTarget();
        Atach();
        Atached = true;
    }

    public void SwitchCameraTarget()
    {
        _camManager.GetComponent<CinemachineVirtualCamera>().Follow = PlayersTransform[_currentSquirrel];
        _camManager.GetComponent<CinemachineVirtualCamera>().LookAt = PlayersTransform[_currentSquirrel];


    }

    public void Atach()
    {
        if (_currentSquirrel == 1 && PlayersTransform[_currentSquirrel].position.x >= PlayersTransform[_currentSquirrel - 1].position.x - 0.25f && PlayersTransform[_currentSquirrel].position.x <= PlayersTransform[_currentSquirrel - 1].position.x + 0.25f)
        {
            PlayersTransform[_currentSquirrel - 1].parent = PlayersTransform[_currentSquirrel];
            PlayersTransform[_currentSquirrel - 1].gameObject.GetComponent<PlayerController>().controlEnabled = true;
            PlayersTransform[_currentSquirrel - 1].position = new Vector3(PlayersTransform[_currentSquirrel].position.x - 0.2f, PlayersTransform[_currentSquirrel].position.y, 0);
            PlayersTransform[_currentSquirrel - 1].gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            Atached = !Atached;
        }
        else if (_currentSquirrel == 0 && PlayersTransform[_currentSquirrel].position.x >= PlayersTransform[_currentSquirrel + 1].position.x - 0.25f && PlayersTransform[_currentSquirrel].position.x <= PlayersTransform[_currentSquirrel + 1].position.x + 0.25f)
        {
            PlayersTransform[_currentSquirrel + 1].parent = PlayersTransform[_currentSquirrel];
            PlayersTransform[_currentSquirrel + 1].gameObject.GetComponent<PlayerController>().controlEnabled = true;
            PlayersTransform[_currentSquirrel + 1].position = new Vector3(PlayersTransform[_currentSquirrel].position.x - 0.2f, PlayersTransform[_currentSquirrel].position.y, 0);
            PlayersTransform[_currentSquirrel + 1].gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            Atached = !Atached;
        }
    }
    public void Detach(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;
        if (Atached)
        {
            if (_currentSquirrel == 1)
            {
                PlayersTransform[_currentSquirrel - 1].gameObject.GetComponent<PlayerController>().controlEnabled = false;
                PlayersTransform[_currentSquirrel - 1].gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else
            {
                PlayersTransform[_currentSquirrel + 1].gameObject.GetComponent<PlayerController>().controlEnabled = false;
                PlayersTransform[_currentSquirrel + 1].gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            PlayersTransform[_currentSquirrel].DetachChildren();
            Atached = !Atached;
        }
        else
            Atach();
    }

    public void SwitchSquirrel(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;
        Vector3 temp;
        switch (_currentSquirrel)
        {
            case 0:
                temp = PlayersTransform[_currentSquirrel].position;
                PlayersTransform[_currentSquirrel].DetachChildren();

                if (Atached)
                {
                    PlayersTransform[_currentSquirrel].SetParent(PlayersTransform[_currentSquirrel + 1]);
                    PlayersTransform[_currentSquirrel + 1].position = temp;
                    PlayersTransform[_currentSquirrel].position = new Vector3(PlayersTransform[_currentSquirrel + 1].position.x - 0.2f, PlayersTransform[_currentSquirrel + 1].position.y, 0);
                }
                else
                {
                    PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>().controlEnabled = false;
                    PlayersTransform[_currentSquirrel + 1].gameObject.GetComponent<PlayerController>().controlEnabled = true;
                }
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<SpriteRenderer>().sortingOrder -= 2;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerInput>().enabled = false;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<BoxCollider2D>().isTrigger = !PlayersTransform[_currentSquirrel].gameObject.GetComponent<BoxCollider2D>().isTrigger;
                _currentSquirrel++;

                break;
            case 1:

                temp = PlayersTransform[_currentSquirrel].position;
                PlayersTransform[_currentSquirrel].DetachChildren();
                if (Atached)
                {

                    PlayersTransform[_currentSquirrel].SetParent(PlayersTransform[_currentSquirrel - 1]);
                    PlayersTransform[_currentSquirrel - 1].position = temp;
                    PlayersTransform[_currentSquirrel].position = new Vector3(PlayersTransform[_currentSquirrel - 1].position.x - 0.2f, PlayersTransform[_currentSquirrel - 1].position.y, 0);
                }
                else
                {
                    PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>().controlEnabled = false;
                    PlayersTransform[_currentSquirrel - 1].gameObject.GetComponent<PlayerController>().controlEnabled = true;
                }
                PlayersTransform[_currentSquirrel - 1].gameObject.GetComponent<SpriteRenderer>().sortingOrder += 2;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerInput>().enabled = false;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<BoxCollider2D>().isTrigger = !PlayersTransform[_currentSquirrel].gameObject.GetComponent<BoxCollider2D>().isTrigger;
                _currentSquirrel--;

                break;
        }

        PlayersTransform[_currentSquirrel].gameObject.GetComponent<BoxCollider2D>().isTrigger = !PlayersTransform[_currentSquirrel].gameObject.GetComponent<BoxCollider2D>().isTrigger;
        PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerInput>().enabled = true;
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

        if (Atached)
        {
            PlayersTransform[_currentSquirrel].DetachChildren();
            foreach (Transform transform in PlayersTransform)
            {
                transform.gameObject.SetActive(false);
            }
            GameOver();
        }
        else
        {
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
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>().controlEnabled = true;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerInput>().enabled = true;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                SwitchCameraTarget();
            }
        }
    }

}
