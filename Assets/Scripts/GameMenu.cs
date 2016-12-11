using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

    float oldTImeScale;

	// Use this for initialization
	void Start () {

        PauseManager.SubscribeOnPause(OnPause);
        PauseManager.SubscribeOnUnpause(OnUnpause);

        transform.gameObject.SetActive(false);

	}

    public void OnPause() {
        oldTImeScale = Time.timeScale;
        Time.timeScale = 0;
        Cursor.visible = true;
        transform.gameObject.SetActive(true);
    }

    public void OnUnpause() {
        Time.timeScale = oldTImeScale;
        Cursor.visible = false;
        transform.gameObject.SetActive(false);
    }

}
