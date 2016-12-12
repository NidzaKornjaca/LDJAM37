using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerBarDisplay : MonoBehaviour {

    [SerializeField]
    private AudioClip efx;
    private AudioSource source;
    // Use this for initialization
    void Start () {
        Timer t = DynamicGameManager.getTimer();
        t.SubscribeCountdown(playEfx);
        source = GetComponent<AudioSource>();
	}

    void playEfx() {
        source.PlayOneShot(efx);
    }

}
