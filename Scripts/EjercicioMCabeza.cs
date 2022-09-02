using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjercicioMCabeza : MonoBehaviour
{
    public EjercicioMario Mario;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Salt"))
        {
            Mario.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Salt"))
        {
            Mario.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}
