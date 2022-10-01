using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private Coroutine currentScaleUpCoroutine = null;
    private bool isShrinking = false;

    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(TimeToScale());
    }

    private IEnumerator TimeToScale()
    {
        while(true)
        {
            if(isShrinking)
            {
                yield return new WaitForSeconds(10f);
            }

            currentScaleUpCoroutine = StartCoroutine(Scale(1.25f, 1f));
            yield return new WaitForSeconds(10f);
        }
    }

    public void Shrink()
    {
        isShrinking = true;

        if (!currentScaleUpCoroutine.Equals(null))
        {
            StopCoroutine(currentScaleUpCoroutine);
        }

        StartCoroutine(Scale(0.5f, 1f));
    }

    private IEnumerator Scale(float factor, float duration)
    {
        if(factor > 1.0f)
        {
            audioSource.Play();
        }

        float counter = 0;

        Vector3 startScale = transform.localScale;
        Vector3 tagetScale = startScale * factor;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, tagetScale, counter / duration);
            yield return null;
        }

        if(factor < 1.0f)
        {
            isShrinking = false;
        }
    }
}
