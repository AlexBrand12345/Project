using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource source;
    public void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void Blow(float time2die)
    {
        StartCoroutine(BlowUp(time2die));
    }
    IEnumerator BlowUp(float time2die)
    {
        source.Play();
        yield return new WaitForSeconds(time2die);
        Destroy(gameObject);
    }
}
