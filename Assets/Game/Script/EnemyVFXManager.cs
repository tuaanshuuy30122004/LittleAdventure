using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFXManager : MonoBehaviour
{
    public VisualEffect FootStep;
    public ParticleSystem BeingHit;
    public VisualEffect Splash;

    public void BurstFootStep()
    {
        FootStep.SendEvent("OnPlay");
    }

    public void PlayBeingHit()
    {
        BeingHit.transform.rotation = Random.rotation;
        BeingHit.Play();

    }
}
