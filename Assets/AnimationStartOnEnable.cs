using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStartOnEnable : MonoBehaviour {
    Animator anim;
    public AnimationClip clip;
	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        if (!anim) Destroy(this);
	}
	
    void OnEnable() {
        Debug.Log(clip.name);
        anim.Play(clip.name);
    }
}
