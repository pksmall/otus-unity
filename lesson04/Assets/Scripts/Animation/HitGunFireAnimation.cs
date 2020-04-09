using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGunFireAnimation : MonoBehaviour
{
    public ParticleSystem Effect;

    public void PlayEffect()
    {
        Effect.Play();
    }
}
