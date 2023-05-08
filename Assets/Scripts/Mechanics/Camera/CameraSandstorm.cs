using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSandstorm : MonoBehaviour
{
    [Header("GameObject Components")]
    [SerializeField] private ParticleSystem[] _listParticleSystem;

    [Header("Setting")]
    [SerializeField] private int _baseEmissionRate = 1000;
    [SerializeField] private float intensity = 1;

    void Awake()
    {
        this.ChangeParticleEmissionRate(this.intensity);
    }

    public void SetIntensity(float value)
    {
        this.intensity = value;
        if (this.intensity < 0) this.intensity = 0;
        this.ChangeParticleEmissionRate(this.intensity);
    }

    private void ChangeParticleEmissionRate(float value)
    {
        foreach (var pSystem in this._listParticleSystem) {
            var emission = pSystem.emission;
            emission.rateOverTime = this._baseEmissionRate * value;
        }
    }
}
