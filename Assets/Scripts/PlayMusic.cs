using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {
    public AudioClip music;
    [Range(0f, 1f)]public float volume;
    public bool loop;

	// Use this for initialization
	void OnEnable () {
        MusicManager.Fade(music, volume, loop);	
	}

}
