using System.Diagnostics;
using UnityEngine;

public class SwitchTrack : MonoBehaviour
{
    [SerializeField] private GameObject trackSwitch;
    [SerializeField] private float currentTrack = 1;
    [SerializeField] private GameObject track1;
    [SerializeField] private GameObject track2;

    [SerializeField]AudioClip trackClip;
    AudioSource trackSource;

    private void Awake()
    {
        trackSource = GetComponent<AudioSource>();
    }
    public void ChangeTrack()
    {
        if (currentTrack == 2)
        {
            track1.SetActive(true);
            track2.SetActive(false);
            currentTrack = 1;
            transform.localRotation = Quaternion.Euler(0f,0f,-25f);
        }
        else if (currentTrack == 1)
        {
            track1.SetActive(false);
            track2.SetActive(true);
            currentTrack = 2;
            transform.localRotation = Quaternion.Euler(0f, 0f, 25f);
        }
        trackSource?.Play();
    }
}
