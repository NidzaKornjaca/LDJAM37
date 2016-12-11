using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour {

    public class ColliderEvent : UnityEvent<Collider> { }

    private ColliderEvent m_event;

    void Awake() {
        if (m_event == null) m_event = new ColliderEvent();
    }

    void OnTriggerEnter(Collider other) {
        m_event.Invoke(other);
    }

    public void Subscribe(UnityAction<Collider> action) {
        m_event.AddListener(action);
    }

    public void UnSubsribe(UnityAction<Collider> action) {
        m_event.RemoveListener(action);
    }

}
