using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongPique : Pique
{

    public AudioClip PiqueMovement;
    public float _speed, _maxdist, _timeOffSet;
    float _oldDist;
    bool Up = false;
    float _time = 0;
    public float _maxTime = Mathf.Infinity;
    bool Active = false;

    float _lastUp = 0;
    public float _timeOffsetSinceUp = 0;

    private void Awake()
    {
        _oldDist = transform.localPosition.y;
        Up = true;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _timeOffSet && _time <= _maxTime)
        {
            Active = true;
        }
        else
        {
            Active = false;
        }
        if (Active)
        {
            if (transform.localPosition.y >= _oldDist + _maxdist && Up)
            {
                Up = false;
            }
            else if (transform.localPosition.y <= _oldDist && !Up)
            {
                Up = true;
                _lastUp = Time.time;
                GetComponent<AudioSource>().PlayOneShot(PiqueMovement);
            }
            if (Up && Time.time >= _timeOffsetSinceUp + _lastUp)
                transform.localPosition += new Vector3(0, _speed * Time.deltaTime, 0);
            else if (!Up)
                transform.localPosition -= new Vector3(0, _speed * Time.deltaTime, 0);
        }

    }

}
