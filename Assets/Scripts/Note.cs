using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] 
    private GameManager gameManager;
    
    [SerializeField] 
    private NotePool pool;

    [SerializeField]
    private AudioManager audioManager;

    public NoteType noteType;

    private float rotationSpeed = 50f;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(gameManager.IsCurrentCorrectNote(noteType))
            {
                audioManager.PlayNote(noteType);
                pool.Despawn(noteType);
                gameManager.MoveCurrentCorrectNoteIndex();
            }
            else
            {
                audioManager.PlayHiss();
                gameManager.FailRound();
            }
        }
    }
}
