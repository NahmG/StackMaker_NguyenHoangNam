using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Ins;
    private void Awake()
    {
        Ins = this;
    }

    [SerializeField] AudioSource source;
    Dictionary<SFX_TYPE, AudioClip> _sfxAudioDict;

    public void Play_SFX(SFX_TYPE id, float volume)
    {

    }
}

public enum SFX_TYPE
{

}