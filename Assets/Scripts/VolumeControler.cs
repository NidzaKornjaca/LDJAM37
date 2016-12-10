using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void MuteAudio()
    {
        AudioListener.volume = 0f;
    }

    void ChangeVolume(float val) {
        AudioListener.volume = val;
    }

}
