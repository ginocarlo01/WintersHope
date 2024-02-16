using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AudioData
{
    public SFX audioName;
    public AudioClip audioClip;

}

public class SoundPlayer : MonoBehaviour
{
    public AudioData[] audioDataArray;

    [SerializeField] private AudioSource _musicSource, _effectsSource;

    private Dictionary<SFX, AudioClip> audioClips;

    [SerializeField] private Slider _slider;

    private void Start()
    {
        if (_musicSource != null) _musicSource.gameObject.SetActive(true);
        if (_effectsSource != null) _effectsSource.gameObject.SetActive(true);

        _slider.onValueChanged.AddListener(val => ChangeMasterVolume(val));

        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        audioClips = new Dictionary<SFX, AudioClip>();

        foreach (var audioData in audioDataArray)
        {
            audioClips.Add(audioData.audioName, audioData.audioClip);
        }
    }

    public void PlaySound(SFX sfx)
    {
        if (audioClips.ContainsKey(sfx))
        {
            _effectsSource.PlayOneShot(audioClips[sfx]);
        }
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    #region ObserverSubscription
    private void OnEnable()
    {
        SoundTrigger.exampleActionSFX += PlaySound;
    }

    private void OnDisable()
    {
        SoundTrigger.exampleActionSFX -= PlaySound;
    }
    #endregion

}
