using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnPlayer : MonoBehaviour
{
    public AudioClip ClickClip;

    public void ClickOn()
    {
        SFXManager.Instance.Play(ClickClip, transform.position);
    }
}
