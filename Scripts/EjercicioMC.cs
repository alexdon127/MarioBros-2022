using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjercicioMC : MonoBehaviour
{
    float anguloC;
    Vector3 posR, posP, vAng;
    GameObject player;
    public bool cogido;
    public bool isSeta, isMoneda;
    public GameObject Champ;
    public GameObject Coin;

    public Material GetColo;

    public Sprite bloqueC;
    Animator anim;
    bool monedaC, cajaM;

    float timer;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        cogido = false;
        posR = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        posP = player.transform.position;
        vAng = posP - posR;
        anguloC = Vector3.Angle(vAng, transform.up);
        if (isMoneda)
        {
            anim.SetBool("CajaM", cajaM);
        }

        if (cajaM)
        {
            timer += Time.deltaTime;
            monedaC = true;

            if(timer > 2)
            {
                cogido = false;
                cajaM = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                timer = 0;
            }
        }
    }

   public void Respawn()
    {
        //print("Se ha ejecitado respawn caja");
        cogido = false;
        anim.enabled = true;

        //posR = transform.position;
        posP = player.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (anguloC > 140f)
            {
                if (!cogido)
                {
                    if (isSeta)
                    {
                        Instantiate(Champ.gameObject, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);
                        cogido = true;
                        //cambia al otro sprite
                        anim.enabled = false;
                        gameObject.GetComponent<SpriteRenderer>().sprite = bloqueC;
                    }
                    else if (isMoneda)
                    {
                        if (!monedaC)
                        {
                            Instantiate(Coin.gameObject, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
                            gameObject.GetComponent<BoxCollider2D>().enabled = false;
                            cogido = true;
                            cajaM = true;
                        }
                        else if (monedaC)
                        {
                            gameObject.GetComponent<BoxCollider2D>().enabled = false;
                            cogido = true;
                            cajaM = true;
                        } 
                    }
                }
            }
        }
    }


}
