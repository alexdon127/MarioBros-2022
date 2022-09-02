using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjercicioMEM : MonoBehaviour
{
    GameObject player;
    GameObject[] enemigos, cajas, champs;
    public GameObject[] posicionEnemy;
    EjercicioMario EjercicioMario;
    float timer;
    public GameObject enemyPref;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EjercicioMario = player.GetComponent<EjercicioMario>();
        posicionEnemy = GameObject.FindGameObjectsWithTag("PosEnem");
    }

    // Start is called before the first frame update
    void Start()
    {
        RespawnE();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        enemigos = GameObject.FindGameObjectsWithTag("Enem");
        cajas = GameObject.FindGameObjectsWithTag("Tam");
        champs = GameObject.FindGameObjectsWithTag("Champ");

        if (EjercicioMario.tiempoR)
        {
            timer += Time.deltaTime;
            //print(timer);
            if (timer > 5)
            {
                EjercicioMario.Respawn();
                RespawnE();

                for (int i = 0; i < cajas.Length; i++)
                {
                    cajas[i].GetComponent<EjercicioMC>().Respawn();
                }
                for (int i = 0; i < champs.Length; i++)
                {
                    champs[i].GetComponent<EjercicioMEnemigo>().Respawn();
                }


                EjercicioMario.tiempoR = false;
                timer = 0;
            }
        }
    }

    void RespawnE()
    {
        //print("EmpiezaRespawn");
        enemigos = GameObject.FindGameObjectsWithTag("Enem");

        for (int i = 0; i < enemigos.Length; i++)
        {
            Destroy(enemigos[i]);
            //print("Destruye Enemys");
        }
        for (int i = 0; i < posicionEnemy.Length; i++)
        {
            Instantiate(enemyPref.gameObject, posicionEnemy[i].transform.position, Quaternion.identity);
            //print("Instancia Enemys");
        }
    }
}
