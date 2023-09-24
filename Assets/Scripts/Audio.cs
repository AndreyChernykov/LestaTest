using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play
{
    public class Audio : MonoBehaviour
    {
        [SerializeField] AudioSource audioSourceMusic;
        [SerializeField] AudioSource audioSourceEffects;
        [SerializeField] List<AudioClip> audioEffect;

        public bool isPlay { get; set; }

        public void PlayMusic()
        {
            if(isPlay)audioSourceMusic.Play();
            else audioSourceMusic.Stop();
        }

        public void PlayEffect(int num)
        {
            if(isPlay)audioSourceEffects.PlayOneShot(audioEffect[num]);

        }

    }
}


