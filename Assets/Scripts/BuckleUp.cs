using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BuckleUp : MonoBehaviour
{
    public GameObject playerHolder;
    public GameObject playerCollider;
    private Player player;

    private bool sitting;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        sitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            sitting = true;
            player.transform.parent = playerHolder.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            sitting = false;
            player.transform.parent = null;
        }
    }
}
