using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem explosion;
    public ParticleSystem firePop;
    public ParticleSystem fireRain;
    public ParticleSystem fireball;
    public ParticleSystem smallElectric;
    public ParticleSystem bigElectric;
    public ParticleSystem lightRay;
    public ParticleSystem iceRing;
    public ParticleSystem iceball;
    public ParticleSystem healGround;
    // Start is called before the first frame update

    void Awake()
    {
        StopAllParticles();   
    }
    public void PlayParticleEffect(string name, Vector3 pos)
    {
        StopAllParticles();
        gameObject.transform.position = pos;
        switch (name)
        {
            case "Explosion":
                explosion.Play();
                break;
            case "Fire Pop":
                firePop.Play();
                break;
            case "Fire Rain":
                fireRain.Play();
                break;
            case "Small Electric":
                smallElectric.Play();
                break;
            case "Big Electric":
                bigElectric.Play();
                break;
            case "Light Ray":
                lightRay.Play();
                break;
            case "Ice Ring":
                iceRing.Play();
                break;
            case "Ice Ball":
                iceball.Play();
                break;
            case "Heal Ground":
                healGround.Play();
                break;
        }
    }

    public void StopAllParticles()
    {
        explosion.Stop();
        firePop.Stop();
        fireRain.Stop();
        fireball.Stop();
        smallElectric.Stop();
        bigElectric.Stop();
        lightRay.Stop();
        iceRing.Stop();
        iceball.Stop();
        healGround.Stop();
    }

}
