using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeper : MonoBehaviour
{
    public GameObject gate;
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
            //play floor 1 audio + score status
        }
        if (F2Score > F2Debounce)
        {
            F2Debounce++;
            //play floor 2 audio + score status
        }
        if (F1Score + F2Score == 16 && !gateRaised) {
            gate.transform.position = new Vector3(gate.transform.position.x, gate.transform.position.y + 0.1f, gate.transform.position.z);
            gateRaised = true;
        }
    }
}
