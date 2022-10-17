using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private NotePool notePool;
    [SerializeField] private BlackHole[] blackHoles;
    [SerializeField] private SceneManager sceneManager;

    private NoteType[] allNotes = { NoteType.A, NoteType.B, NoteType.C, NoteType.D, NoteType.E, NoteType.F, NoteType.G };

    private int currentCorrectLetterIndex = 0;

    private Coroutine currentNewRoundCoroutine;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentNewRoundCoroutine = StartCoroutine(NewRound());
    }

    private IEnumerator NewRound()
    {
        Shuffle(allNotes);
        notePool.ShuffleSpawnPoints();

        yield return new WaitForSeconds(3f);

        for(int i = 0; i < notePool.GetSpawnPointsCount(); i++)
        {
            notePool.Spawn(allNotes[i]);
            yield return new WaitForSeconds(1f);
        }

        currentCorrectLetterIndex = 0;
    }

    public static void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    public bool IsCurrentCorrectNote(NoteType noteType)
    {
        return noteType == allNotes[currentCorrectLetterIndex];
    }

    public void MoveCurrentCorrectNoteIndex()
    {
        currentCorrectLetterIndex++;

        if(currentCorrectLetterIndex >= notePool.GetSpawnPointsCount())
        {
            currentNewRoundCoroutine = StartCoroutine(NewRound());

            foreach (var blackHole in blackHoles)
            {
                blackHole.Shrink();
            }
        }
    }

    public void FailRound()
    {
        StopCoroutine(currentNewRoundCoroutine);

        for (int i = 0; i < notePool.GetSpawnPointsCount(); i++)
        {
            notePool.Despawn(allNotes[i]);
        }

        currentNewRoundCoroutine = StartCoroutine(NewRound());
    }

    public void GameOver()
    {
        sceneManager.LoadScene("End");
    }

}
