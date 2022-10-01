using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePool : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    private int currentSpawnIndex = -1;

    [SerializeField]
    private AudioManager audioManager;

    private readonly Dictionary<NoteType, GameObject> notes = new();    

    private void Awake()
    {
        foreach (var note in GetComponentsInChildren<Note>())
        {
            notes.Add(note.noteType, note.gameObject);
            note.gameObject.SetActive(false);
        }
    }

    public void Spawn(NoteType noteType)
    {
        GameObject note = notes[noteType];
        note.SetActive(true);

        currentSpawnIndex++;
        if(currentSpawnIndex >= spawnPoints.Length)
        {
            currentSpawnIndex = 0;
        }

        note.transform.position = spawnPoints[currentSpawnIndex].transform.position;
        audioManager.PlayNote(noteType);
    }

    public void Despawn(NoteType noteType)
    {
        GameObject note = notes[noteType];
        note.transform.position = transform.position;
        note.SetActive(false);
    }

    public void ShuffleSpawnPoints()
    {
        GameManager.Shuffle(spawnPoints);
    }

    public int GetSpawnPointsCount()
    {
        return spawnPoints.Length;
    }
}
