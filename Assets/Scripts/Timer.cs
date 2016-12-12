using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour {

    float timeLeft;
    private UnityEvent timerOff;

    public void StartTimer(float time) {
        timeLeft = time;
        gameObject.SetActive(true);
    }

    public void Subscribe(UnityAction action) {
        timerOff.AddListener(action);
    }

    public void Unsubscribe(UnityAction action) {
        timerOff.RemoveListener(action);
    }

    void Awake() {
        timerOff = new UnityEvent();
    }

    public float TimeLeft() {
        return timeLeft;
    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) {
            timerOff.Invoke();
            gameObject.SetActive(false);
        }	
	}
}
