using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundPlayer : MonoBehaviour
{
    public AudioClip[] HitClip;

    public void Play(int index)
    {
        SFXManager.Instance.Play(HitClip[index], transform.position);
    }
}
