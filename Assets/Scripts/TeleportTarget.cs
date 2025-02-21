using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTarget : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("projectile"))
        {
            Vector3[] Floor1 = { new Vector3(-15, .25f, -5), new Vector3(-15, .25f, 0), new Vector3(-15, .25f, 5), new Vector3(-15, .25f, 10), new Vector3(-15, .25f, 15), new Vector3(-10, .25f, -5), new Vector3(-10, .25f, 0), new Vector3(-10, .25f, 5), new Vector3(-10, .25f, 10), new Vector3(-10, .25f, 15), new Vector3(-5, .25f, -10), new Vector3(-5, .25f, -5), new Vector3(-5, .25f, 0), new Vector3(-5, .25f, 5), new Vector3(-5, .25f, 10), new Vector3(-5, .25f, 15), new Vector3(30, .25f, -10), new Vector3(30, .25f, -5), new Vector3(30, .25f, 0), new Vector3(30, .25f, 5), new Vector3(30, .25f, 10), new Vector3(30, .25f, 15), new Vector3(35, .25f, -10), new Vector3(35, .25f, -5), new Vector3(35, .25f, 0), new Vector3(35, .25f, 5), new Vector3(35, .25f, 10), new Vector3(35, .25f, 15), new Vector3(40, .25f, -10), new Vector3(40, .25f, -5), new Vector3(40, .25f, 0), new Vector3(40, .25f, 5), new Vector3(40, .25f, 10), new Vector3(40, .25f, 15) };
            Vector3[] Floor2 = { new Vector3(-15, 5.25f, -5), new Vector3(-15, 5.25f, 0), new Vector3(-15, 5.25f, 5), new Vector3(-15, 5.25f, 10), new Vector3(-15, 5.25f, 15), new Vector3(-10, 5.25f, -5), new Vector3(-10, 5.25f, 0), new Vector3(-10, 5.25f, 5), new Vector3(-10, 5.25f, 10), new Vector3(-10, 5.25f, 15), new Vector3(-5, 5.25f, -10), new Vector3(-5, 5.25f, -5), new Vector3(-5, 5.25f, 0), new Vector3(-5, 5.25f, 5), new Vector3(-5, 5.25f, 10), new Vector3(-5, 5.25f, 15), new Vector3(25, 5.25f, -10), new Vector3(25, 5.25f, -5), new Vector3(25, 5.25f, 0), new Vector3(25, 5.25f, 5), new Vector3(25, 5.25f, 10), new Vector3(25, 5.25f, 15), new Vector3(30, 5.25f, -10), new Vector3(30, 5.25f, -5), new Vector3(30, 5.25f, 0), new Vector3(30, 5.25f, 5), new Vector3(30, 5.25f, 10), new Vector3(30, 5.25f, 15), new Vector3(35, 5.25f, -10), new Vector3(35, 5.25f, 10), new Vector3(35, 5.25f, 15), new Vector3(40, 5.25f, -10), new Vector3(40, 5.25f, 10), new Vector3(40, 5.25f, 15) };
            int spawnPoint = Random.Range(0, Floor1.Length + Floor2.Length);
            if (spawnPoint < Floor1.Length)
            {
                enemy.transform.position = Floor1[spawnPoint];
            }
            else
            {
                enemy.transform.position = Floor2[spawnPoint - Floor1.Length];
            }
        }
    }
}
