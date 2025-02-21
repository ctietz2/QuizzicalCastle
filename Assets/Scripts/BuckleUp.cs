using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject playerSeat;
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
        if (other.gameObject.name == "player_seat")
        {
            sitting = true;
            player.transform.parent = player_holder.transform;
        }
    }
}
