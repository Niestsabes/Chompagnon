using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using System.Drawing;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public GameObject[] _camManagers;
    public Transform[] PlayersTransform;
    public GameObject GlobalPlayerParent;
    public bool Atached = true;
    public int _currentSquirrel;
    public int[] _lifes;
    public Transform SpawnPoint;
    public GameObject TombPrefab;
    public List<GameObject> TombGroup;
    public float MultiplierOffsetY;

    public float maxAttachDistance = 0.25f;
    public Vector3 attachOffset = new Vector3(-0.3f, 0f, 0f);

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Spawn();
    }

    public void GameOver() {
        Spawn();
    }


    public void Spawn() {
        TombGroup.Clear();
        foreach (Transform transform in PlayersTransform) {
            transform.position = SpawnPoint.position;
            transform.gameObject.SetActive(true);
        }
        if (_currentSquirrel == 0) {
            PlayersTransform[_currentSquirrel + 1].SetParent(PlayersTransform[_currentSquirrel]);
        }
        else {
            PlayersTransform[_currentSquirrel - 1].SetParent(PlayersTransform[_currentSquirrel]);
        }
        PlayersTransform[_currentSquirrel].SetParent(GlobalPlayerParent.transform);
        _lifes[0] = 1;
        _lifes[1] = 1;
        SwitchCameraTarget();
        Atach();
        Atached = true;
    }

    public void SwitchCameraTarget() {
        foreach (var _camManager in _camManagers) { 
            _camManager.GetComponent<CinemachineVirtualCamera>().Follow = PlayersTransform[_currentSquirrel];
            _camManager.GetComponent<CinemachineVirtualCamera>().LookAt = PlayersTransform[_currentSquirrel];
        }
    }

    public void Atach()
    {
        int _otherSquirrel = 1 - _currentSquirrel;

        if ((PlayersTransform[_currentSquirrel].position - PlayersTransform[_otherSquirrel].position).magnitude <= maxAttachDistance) {
            PlayersTransform[_otherSquirrel].parent = PlayersTransform[_currentSquirrel];
            PlayersTransform[_otherSquirrel].gameObject.GetComponent<PlayerController>().controlEnabled = true;
            PlayersTransform[_otherSquirrel].position = PlayersTransform[_currentSquirrel].position + attachOffset;
            PlayersTransform[_otherSquirrel].gameObject.GetComponent<Collider2D>().isTrigger = true;
            Atached = !Atached;
        }
    }

    public void Detach()
    {
        if (Atached)
        {
            int _otherSquirrel = 1 - _currentSquirrel;
            PlayersTransform[_otherSquirrel].gameObject.GetComponent<PlayerController>().move.x = 0;
            PlayersTransform[_otherSquirrel].gameObject.GetComponent<PlayerController>().controlEnabled = false;
            PlayersTransform[_otherSquirrel].gameObject.GetComponent<Collider2D>().isTrigger = true;
            PlayersTransform[_currentSquirrel].DetachChildren();
            Atached = !Atached;
        }
        else
            Atach();
    }

    public void SwitchSquirrel()
    {
        Vector3 temp;
        //if (_lifes[_currentSquirrel])
        temp = PlayersTransform[_currentSquirrel].position;
        PlayersTransform[_currentSquirrel].DetachChildren();
        PlayersTransform[_currentSquirrel].parent.DetachChildren();
        switch (_currentSquirrel)
        {
            case 0:


                if (Atached)
                {
                    PlayersTransform[_currentSquirrel].SetParent(PlayersTransform[_currentSquirrel + 1]);
                    PlayersTransform[_currentSquirrel + 1].position = temp;
                    PlayersTransform[_currentSquirrel].position = PlayersTransform[_currentSquirrel + 1].position + attachOffset;

                }
                else
                {
                    PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>().controlEnabled = false;
                    PlayersTransform[_currentSquirrel + 1].gameObject.GetComponent<PlayerController>().controlEnabled = true;
                    PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>().move.x = 0;

                }
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<SpriteRenderer>().sortingOrder -= 2;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<Collider2D>().isTrigger = !PlayersTransform[_currentSquirrel].gameObject.GetComponent<Collider2D>().isTrigger;
                _currentSquirrel++;

                break;
            case 1:
                if (Atached)
                {

                    PlayersTransform[_currentSquirrel].SetParent(PlayersTransform[_currentSquirrel - 1]);
                    PlayersTransform[_currentSquirrel - 1].position = temp;
                    PlayersTransform[_currentSquirrel].position = PlayersTransform[_currentSquirrel - 1].position + attachOffset;
                }
                else
                {

                    PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>().controlEnabled = false;
                    PlayersTransform[_currentSquirrel - 1].gameObject.GetComponent<PlayerController>().controlEnabled = true;
                    PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>().move.x = 0;
                }
                PlayersTransform[_currentSquirrel - 1].gameObject.GetComponent<SpriteRenderer>().sortingOrder += 2;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<Collider2D>().isTrigger = !PlayersTransform[_currentSquirrel].gameObject.GetComponent<Collider2D>().isTrigger;
                _currentSquirrel--;

                break;
        }
        PlayersTransform[_currentSquirrel].gameObject.GetComponent<Collider2D>().isTrigger = !PlayersTransform[_currentSquirrel].gameObject.GetComponent<Collider2D>().isTrigger;
        PlayersTransform[_currentSquirrel].SetParent(GlobalPlayerParent.transform);
        SwitchCameraTarget();
    }

    public void Death()
    {
        if (Atached)
        {
            PlayersTransform[_currentSquirrel].DetachChildren();
            PlayersTransform[_currentSquirrel].parent.DetachChildren();
            foreach (Transform transform in PlayersTransform)
            {
                transform.gameObject.SetActive(false);
            }
            GameOver();
        }
        else
        {
            _lifes[_currentSquirrel]--;
            TombGroup.Add(Instantiate(TombPrefab, new Vector2(PlayersTransform[_currentSquirrel].position.x, PlayersTransform[_currentSquirrel].position.y + (PlayersTransform[_currentSquirrel].GetComponent<BoxCollider2D>().offset.y *MultiplierOffsetY)), Quaternion.Euler(0, 0, 0)));
            foreach (GameObject tomb in TombGroup)
            {
                tomb.SetActive(true);
                tomb.GetComponent<TombObject>().targetTomb = null;
            }
            PlayersTransform[_currentSquirrel].gameObject.SetActive(false);
            PlayersTransform[_currentSquirrel].parent.DetachChildren();
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
                TombGroup[0].GetComponent<TombObject>().targetTomb = TombGroup[1].GetComponent<TombObject>();
                TombGroup[1].GetComponent<TombObject>().targetTomb = TombGroup[0].GetComponent<TombObject>();
                GameOver();
            }
            else
            {
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>().controlEnabled = true;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<PlayerController>().Alone = true;
                PlayersTransform[_currentSquirrel].gameObject.GetComponent<Collider2D>().isTrigger = false;
                PlayersTransform[_currentSquirrel].SetParent(GlobalPlayerParent.transform);
                SwitchCameraTarget();
            }
            
        }
    }

}
