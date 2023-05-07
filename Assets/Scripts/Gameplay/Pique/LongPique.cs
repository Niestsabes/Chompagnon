using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongPique : Pique
{
    public float _speed, _maxdist, _timeOffSet;
    float _oldDist;
    bool Up = false;
    float _time = 0;
    bool Active = false;

    private void Awake()
    {
        _oldDist = transform.localPosition.y;
        Up = true;
    }
    private void Update()
    {
        _time += Time.deltaTime;
        if(_time >= _timeOffSet)
        {
            Active = true;
        }
        if(Active == true)
        {
            if (transform.localPosition.y >= _oldDist + _maxdist && Up == true)
            {
                Up = false;
            }
            else if (transform.localPosition.y <= _oldDist && Up == false)
            {
                Up = true;

            }
            if (Up)
                transform.localPosition += new Vector3(0, _speed* Time.deltaTime, 0);
            else
                transform.localPosition -= new Vector3(0, _speed * Time.deltaTime, 0);
        }
        
    }

}
