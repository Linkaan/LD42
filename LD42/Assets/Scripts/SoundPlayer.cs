using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

    public AudioSource source;

    public AudioClip saveFileSFX;
    public AudioClip failSFX;
    public AudioClip loseSFX;
    public AudioClip wormSFX;

    public AudioClip clickSFX;
    public AudioClip hoverSFX;

    public void PlaySFX(AudioClip sfx) {
        if (source.isPlaying) return;

        source.clip = sfx;
        source.Play();
    }

    public void PlayClick() {
        PlaySFX(clickSFX);
    }

    public void PlayHover() {
        PlaySFX(hoverSFX);
    }
}
