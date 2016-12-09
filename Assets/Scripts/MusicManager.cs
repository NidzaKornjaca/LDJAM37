using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : Singleton<MusicManager> {
    private AudioSource audioSource;
    private float baseVolume;
    private float targetVolume;
    private bool done = false;

    protected MusicManager() { }

	void Awake () {
        audioSource = transform.GetOrAddComponent<AudioSource>();
        baseVolume = audioSource.volume;
	}

    public static void PlayMusic(AudioClip music, float volume = 1.0f) {
        Instance.audioSource.PlayOneShot(music, volume);
    }
    
    public static void Fade(AudioClip music, float volume = 1.0f, bool loop = false) {
        Instance.targetVolume = Instance.baseVolume * volume;
        Instance.audioSource.loop = loop;
        Instance.FadeTo(music);
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
