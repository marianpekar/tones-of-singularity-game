using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] noteClips;

    [SerializeField]
    private AudioClip hissClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayNote(NoteType note)
    {
        audioSource.clip = noteClips[(int)note];
        audioSource.Play();
    }

    public void PlayHiss()
    {
        audioSource.clip = hissClip;
        audioSource.Play();
    }
}
