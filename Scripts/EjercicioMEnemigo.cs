using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjercicioMEnemigo : MonoBehaviour
{
    float vel, dist;
    int dir;
    public bool champ, koopa, goomba, moneda;
    Vector3 posR, posP, vAng;
    float anguloG;
    GameObject player;
    EjercicioMario EjercicioMario;
    public bool golpe, des;
    Animator anim;
    Transform cabezaE;
    Rigidbody2D rigidM;
    private float fuerza = 200f;

    // Start is called before the first frame update
    void Start()
    {
        dir = -1;
        vel = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        EjercicioMario = player.GetComponent<EjercicioMario>();
        rigidM = player.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        golpe = false;
        des = false;
        if (!champ)
        {
            cabezaE = this.gameObject.transform.GetChild(0);
        }
    }
    // Update is called once per frame
    void Update()
    {

        dist = Mathf.Abs(transform.position.x - player.transform.position.x);
        if (goomba)
        {
            transform.Translate(dir * vel, 0, 0);
            if (dist < 2f)
            {
                vel = 0.0015f;
            }
        }
        else if (koopa)
        {
            anim.SetBool("Golpe", golpe);
            anim.SetBool("Des", des);
            transform.Translate(dir * vel, 0, 0);
            if (dist < 2f && !golpe)
            {
                vel = 0.0015f;
            }
        }
        else if (champ)
        {
            vel = 0.001f;
            transform.Translate(dir * vel, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        if (!champ)
        {
            posR = cabezaE.transform.position;
            posP = player.transform.position;
            vAng = posP - posR;
            anguloG = Vector3.Angle(vAng, transform.up);
        }
 
    }

    void Muerte()
    {
        vel = 0.0f;
        dir = 0;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        //gameObject.GetComponent<>().enabled = false;
    }
    public void Respawn()
    {        
        if (champ)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dir") || collision.gameObject.CompareTag("Enem"))
        {
            dir = dir * -1;
            Flip();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (champ)
            {
                Muerte();
            }
            if (moneda)
            {
                Muerte();
            }
            if (goomba)
            {
                EjercicioMario.anguloE = anguloG;
                if(anguloG < 30f)
                {
                    Muerte();
                    rigidM.AddForce(transform.up * fuerza / 3);
                }
                else
                {
                    dir *= -1;
                    Flip();
                }
            }
            if (koopa)
            {
                if (!golpe)
                {
                    EjercicioMario.anguloE = anguloG;
                    if (anguloG < 30f)
                    {
                        //Empeñece
                        vel = 0.0f;
                        golpe = true;
                        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.169f, 0.14f);
                        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.00076f, 0.00013f);
                        rigidM.AddForce(transform.up * fuerza / 3);
                    }
                    else
                    {
                        dir *= -1;
                        Flip();
                    }
                }
                else if (golpe)
                {
                    EjercicioMario.anguloE = anguloG;
                    if (anguloG < 30f)
                    {
                        //Se lanza
                        des = true;
                        vel = 0.002f;
                        transform.Translate(dir * vel, 0, 0);
                        rigidM.AddForce(transform.up * fuerza / 3);
                    }
                    else
                    {
                        dir *= -1;
                        Flip();
                    }
                }
            }
        }
    }

    void Flip()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

}
