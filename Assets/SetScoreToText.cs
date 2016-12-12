using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScoreToText : MonoBehaviour {

    Text text;

    void Awake() {
        text = GetComponent<Text>();
    }

	void Update() {
        text.text = DynamicGameManager.Score().ToString();
    }
}
