using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpeedTether : MonoBehaviour
{
    public float multiplier = 200;
    public float minRate = 10;
    public float maxRate;
    public int maxParticles = 1500;
    public bool stopIfSpeedIsLow;

    public AnimationCurve curve;

    ParticleSystem ps;
    ParticleSystem.EmissionModule psEmissionModule;
    BoatController boatController;

    private void Awake()
    {
        boatController = GameObject.FindWithTag("Player").GetComponent<BoatController>();
        ps = GetComponent<ParticleSystem>();
        psEmissionModule = ps.emission;
        var psMain = ps.main;
        psMain.maxParticles = maxParticles;
    }

    void LateUpdate()
    {
        if (boatController.NormalizedSpeed < 0.01)
        {
            psEmissionModule.rateOverTime = minRate;
        }
        else
        {
            psEmissionModule.rateOverTime = Mathf.Clamp((curve.Evaluate(boatController.NormalizedSpeed)) * multiplier, minRate, maxRate);
        }
    }
}
