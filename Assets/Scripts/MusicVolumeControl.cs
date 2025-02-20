using UnityEngine;
using UnityEngine.Audio;

public class MusicVolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Логарифмическая шкала
    }
}