using UnityEngine;

public class DestroyTimer : MonoBehaviour {
    public float duration = 5f;
    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if(PauseManager.IsPaused()) {
            startTime += Time.deltaTime;
        }
        if (Time.time - startTime >= duration) Destroy(gameObject);
	}
}
