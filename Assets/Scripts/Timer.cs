using UnityEngine;
using UnityEngine.Events;
using NidzaKornjaca.Bars.BasicBar;

public class Timer : MonoBehaviour {

    public ValueBarDisplay timeBar;
    float maxTime;
    float timeLeft;
    private UnityEvent timerOff;

    public void StartTimer(float time) {
        maxTime = time;
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
            if (timeBar != null) timeBar.Set(0f);
        }
        else if (timeBar != null) timeBar.Set(timeLeft / maxTime);
    }
}
