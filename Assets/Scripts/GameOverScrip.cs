using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScrip : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.gameObject.SetActive(false);
        DynamicGameManager.Subscribe(Show);
	}

    public void Show() {
        transform.gameObject.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
