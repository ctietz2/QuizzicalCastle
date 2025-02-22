using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeper : MonoBehaviour
{
    public GameObject gate1;
    public GameObject gate2; 
    public AudioSource Floor1;
    public AudioSource Floor2;
    public AudioSource[] scores;
    public int F1Score;
    public int F2Score;
    public int F1Debounce;
    public int F2Debounce;
    public bool gateRaised;
    // Start is called before the first frame update
    void Start()
    {
        F1Score = 0;
        F1Debounce = 0;
        F2Score = 0;
        F2Debounce = 0;
        gateRaised = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (F1Score > F1Debounce)
        {
            F1Debounce++;
            StartCoroutine(announceScore(Floor1, scores[F1Score - 1]));
        }
        if (F2Score > F2Debounce)
        {
            F2Debounce++;
            StartCoroutine(announceScore(Floor2, scores[F2Score - 1]));
        }
        if (F1Score + F2Score == 16 && !gateRaised) {
            gate1.transform.position = new Vector3(gate1.transform.position.x, gate1.transform.position.y + 5, gate1.transform.position.z);
            gate2.transform.position = new Vector3(gate2.transform.position.x, gate2.transform.position.y + 5, gate2.transform.position.z);
            gateRaised = true;
        }
    }

    IEnumerator announceScore(AudioSource floor, AudioSource score) {
        floor.Play();
        while (floor.isPlaying) {
            yield return null;
        }
        score.Play();
        while (floor.isPlaying)
        {
            yield return null;
        }
    }
}
