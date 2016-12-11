using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {

        PauseManager.SubscribeOnPause(OnPause);
        PauseManager.SubscribeOnUnpause(OnUnpause);

        transform.gameObject.SetActive(false);

	}

    public void OnPause() {
        transform.gameObject.SetActive(true);
    }

    public void OnUnpause() {
        transform.gameObject.SetActive(false);
    }

}
