using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    public VisualEffect footstep;
    public ParticleSystem Blade01;
    public ParticleSystem Blade02;
    public ParticleSystem Blade03;
    public VisualEffect Slash;

    public void UpdateFootstep(bool state)
    {
        if (state)
        {
            footstep.Play();
        }
        else
            footstep.Stop(); 
    }

    public void PlayBlade01()
    {
        Blade01.Play();
    }
    public void PlayBlade02()
    {
        Blade02.Play();
    }
    public void PlayBlade03()
    {
        Blade02.Play();
    }

    public void StopBlade()
    {
        Blade01.Simulate(0);
        Blade01.Stop();

        Blade02.Simulate(0);
        Blade02.Stop();

        Blade03.Simulate(0);
        Blade03.Stop();
    }

    public void PlayerSlash()
    {
        
        Slash.Play();
    }
}
