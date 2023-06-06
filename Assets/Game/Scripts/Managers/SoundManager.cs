using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource mainmenuMusic;
    public AudioSource gameplayMusic;
    public AudioSource hitSoundEffect;
    public AudioSource currentMusic;
    private void Start() {
        currentMusic = mainmenuMusic;
    }
    public void PlayHitSound(){
        hitSoundEffect.Play();
    }
    public void PlayMusic(AudioSource music){
        if(currentMusic != music){
            currentMusic.Stop();
            currentMusic = music;
        }
        if(!music.isPlaying){
            music.Play();
        }
    }
}
