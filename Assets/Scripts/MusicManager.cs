using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {
    private static MusicManager musicManager;
    private AudioSource audioSource;
    private float baseVolume;
    private float targetVolume;
    private bool done = false;

	void Start () {
        if (musicManager != null && musicManager != this) {
            Destroy(gameObject);
        }
        else musicManager = this;
        audioSource = GetComponent<AudioSource>();
        baseVolume = audioSource.volume;
        DontDestroyOnLoad(gameObject);
	}

    private static MusicManager GetInstance() {
        if (musicManager == null) {
            GameObject obj = new GameObject();
            musicManager = obj.AddComponent<MusicManager>();
            musicManager.audioSource = obj.AddComponent<AudioSource>();
            obj.name = "AudioManager";
        }
        return musicManager;
    }

    public static void PlayMusic(AudioClip music, float volume = 1.0f) {
        GetInstance().audioSource.PlayOneShot(music, volume);
    }
    
    public static void Fade(AudioClip music, float volume = 1.0f, bool loop = false) {
        GetInstance().targetVolume = GetInstance().baseVolume * volume;
        GetInstance().audioSource.loop = loop;
        GetInstance().FadeTo(music);
    }

    private void FadeTo(AudioClip music) {
        StartCoroutine(_FadeTo(music));
    }

    private IEnumerator _FadeTo(AudioClip clip) {
        StartCoroutine(FadeOut());
        if (!audioSource.isPlaying) audioSource.Play();
        yield return new WaitUntil(() => done);
        audioSource.clip = clip;
        StartCoroutine(FadeIn());
        audioSource.Play();
    }
     
    private IEnumerator FadeOut() {
        done = false;
        while(audioSource.volume > 0.01) {
            audioSource.volume = Mathf.Clamp(Mathf.Lerp(audioSource.volume, 0, 5 * Time.deltaTime), 0, 1);
            yield return new WaitForEndOfFrame();
        }
        done = true;
    }

    private IEnumerator FadeIn() {
        done = false;
        float initial = audioSource.volume;
        while (audioSource.volume < targetVolume) {
            audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume + 0.1f*targetVolume , 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        done = true;
    }
}
