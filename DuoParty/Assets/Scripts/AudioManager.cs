using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound x in sounds)
        {
            x.source = gameObject.AddComponent<AudioSource>();
            x.source.clip = x.clip;

            x.source.volume = x.volume;
            x.source.pitch = x.pitch;
            x.source.loop = x.loop;
        }
    }

    public void PlaySound (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    //placer dans nimporte quel scrypt avec le bon nom dans les "" pour jouer un son
    //FindObjectOfType<AudioManager>().PlaySound("");
}
