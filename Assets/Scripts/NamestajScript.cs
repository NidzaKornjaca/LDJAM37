using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamestajScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DynamicGameManager.UpisiSe(this.gameObject);
	}
	
	// Update is called once per frame
	void OnDestory () {
        DynamicGameManager.IspisiSe(this.gameObject);
    }
}
