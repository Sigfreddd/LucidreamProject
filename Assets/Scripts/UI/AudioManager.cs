using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int currentIndex = 0;

    public AudioMixerGroup soundEffectMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance AudioManager dans la sc?ne");
            return;
        }
        instance = this;
    }

    void Start()
    {
        PlaySong();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlaySong();
        }
    }

    void PlaySong()
    {
        audioSource.clip = playlist[currentIndex];
        audioSource.Play();
    }

    public void PlaySpecificSong(int index)
    {
        currentIndex = index;
        PlaySong();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        //Cr?er un nouveau GameObject
        GameObject tempGO = new GameObject("TempAudio");

        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, clip.length);
        return audioSource;
    }
}
