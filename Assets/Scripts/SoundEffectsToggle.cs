using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectsToggle : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void ToggleSoundEffects(bool enabled)
    {
        if (enabled)
        {
            audioMixer.SetFloat("SFXVolume", 0); // Включаем эффекты
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", -80); // Выключаем эффекты
        }
    }
}