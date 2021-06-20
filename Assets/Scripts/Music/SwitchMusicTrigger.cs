using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicTrigger : MonoBehaviour
{

    public AudioClip newTrack;

    private AudioManager AM;

    // Start is called before the first frame update
    void Start()
    {
        AM = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    void OnTriggerEnter(Collider collider) 
    {
        if(collider.tag == "Player")
        {
            AM.ChangeBGM(newTrack);
        }
    }
    */
}
