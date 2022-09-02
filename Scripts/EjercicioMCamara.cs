using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjercicioMCamara : MonoBehaviour
{
    public GameObject player;
    public float dist;
    public bool move;
    public Vector3 PosI;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PosI = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dist = transform.position.x - player.transform.position.x;

        if (dist < 0.8f)
        {
            move = true;
        }

        if (move)
        {
            transform.position = new Vector3(player.transform.position.x + 0.8f, -0.9f, -10f);
            //print("Gegegeg");
        }

        if (transform.position.x < PosI.x || transform.position.y < PosI.y || transform.position.x > 20.75)
        {
            move = false;
        }

    }
}
