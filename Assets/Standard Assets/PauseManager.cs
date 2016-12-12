using UnityEngine;
using UnityEngine.Events;

public class PauseManager : Singleton<PauseManager> {
	private bool paused = false;
    private UnityEvent onPause, onUnpause;
    private bool isGameOver = false;

    protected PauseManager() { }

    void Awake() {
        if (onPause == null) onPause = new UnityEvent();
        if (onUnpause == null) onUnpause = new UnityEvent();
    }

    void Update(){
        if (isGameOver) return;
        if (!paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
                onPause.Invoke();
            }
        }
	}

    public static void SubscribeOnPause(UnityAction action) {
        Instance.onPause.AddListener(action);
    }

    public static void SubscribeOnUnpause(UnityAction action) {
        Instance.onUnpause.AddListener(action);
    }

    public static void UnsubscribeOnPause(UnityAction action) {
        Instance.onPause.RemoveListener(action);
    }

    public static void UnsubscribeOnUnpause(UnityAction action) {
        Instance.onUnpause.RemoveListener(action);
    }

    public static bool IsPaused(){
		return Instance.paused;
	}

    public static void TogglePause()
    {
        Instance.paused = !(Instance.paused);
        if (Instance.paused) Instance.onPause.Invoke();
        else Instance.onUnpause.Invoke();
    }

    public static void setGameOver(bool val) {
        Instance.isGameOver = val;
    }

}
