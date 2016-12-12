using UnityEngine;
using UnityEngine.Events;
using NidzaKornjaca.Bars.BasicBar;

public class Timer : MonoBehaviour {

    public ValueBarDisplay timeBar;
    float maxTime;
    float timeLeft;
    private UnityEvent timerOff;
    private UnityEvent countdown;
    private int countodownStartAt = 5;
    private int currentCD;

    public void StartTimer(float time) {
        maxTime = time;
        timeLeft = time;
        currentCD = countodownStartAt; 
    }

    public void Subscribe(UnityAction action) {
        timerOff.AddListener(action);
    }

    public void Unsubscribe(UnityAction action) {
        timerOff.RemoveListener(action);
    }

    public void SubscribeCountdown(UnityAction action)
    {
        countdown.AddListener(action);
    }

    public void UnsubscribeCountdown(UnityAction action)
    {
        countdown.RemoveListener(action);
    }

    void Awake() {
        timerOff = new UnityEvent();
        countdown = new UnityEvent();
    }

    public float TimeLeft() {
        return timeLeft;
    }
	
	// Update is called once per frame
	void Update () {
        if (PauseManager.IsPaused()) {
            return;
        }
        timeLeft -= Time.deltaTime;
        if (timeLeft < currentCD) {
            countdown.Invoke();
            currentCD--;
        }
        if (timeLeft < 0) {
            timerOff.Invoke();
            if (timeBar != null) timeBar.Set(0f);
        }
        else if (timeBar != null) timeBar.Set(timeLeft / maxTime);
    }
}
