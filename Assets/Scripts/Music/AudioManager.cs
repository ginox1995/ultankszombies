using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioSource BGM;

    void Awake() 
    {
        foreach (Sound s in sounds) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        DontDestroyOnLoad(transform.gameObject);
    }

    public void ChangeBGM(AudioClip music) 
    {
        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }

    public void Play(string name) 
    {

        //Sound s = Array.Find(sounds, sound => );
    }
}
