using UnityEngine;

public class PauseManagerTest : MonoBehaviour {

    void Awake() {
        PauseManager.SubscribeOnPause(Log);
        PauseManager.SubscribeOnUnpause(Log);
    }

    void Log() {
        Debug.Log("hasjdhaksd");
    }
}
