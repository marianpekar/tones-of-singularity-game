using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField]
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Note"))
        {
            gameManager.GameOver();
        }
    }
}
