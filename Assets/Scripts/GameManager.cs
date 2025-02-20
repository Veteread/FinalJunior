using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public PlayerController player;
    public List<Enemy> enemies;
    public List<Bullet> bullets;
    public AudioMixer audioMixer;
    public GameObject pausePanel;

    void FixedUpdate()
    {
        if (!isPaused)
        {
            //CheckCollisions();
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        // ��������� ����������� ������
        player.enabled = !isPaused;

        // ��������� ������
        foreach (var enemy in enemies)
        {
            enemy.enabled = !isPaused;
        }

        // ��������� ����
        foreach (var bullet in bullets)
        {
            bullet.StopMovement();
        }

        // ���������� ������
        if (isPaused)
        {
            audioMixer.SetFloat("MasterVolume", -80); // ��������� ���������� ����
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", 0); // ���������� ������� ������� ���������
        }

        // �����/������� ������ �����
        pausePanel.SetActive(isPaused);
    }

    public void ResumeGame()
    {
        isPaused = false;
        audioMixer.SetFloat("MasterVolume", 0);
        pausePanel.SetActive(false);
    }
}