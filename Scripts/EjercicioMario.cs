using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjercicioMario : MonoBehaviour
{
    //Clases
    private float fuerza = 200f;
    public float vel = 0.03f;
    Rigidbody2D myRigid;
    public bool ground, tamaño = false, daño = false;
    public bool tiempoR = false;
    public GameObject Champ, respawn;
    public float anguloE;
    public int hp = 1;
    float timer;
    bool control;
    Transform cabexon;
    Animator anim;
    int mov;
    bool grow, jump;
    public bool fin;
    //EjercicioMEnemigo EjercicioMEnemigo;
    //GameObject enem;
    //public bool enemigoC;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializo las variables
        myRigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cabexon = this.gameObject.transform.GetChild(0);

        control = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Vinculo la variable Hp de mario, con la variable HP del animator
        anim.SetInteger("HP", hp);

        //enem = GameObject.FindGameObjectWithTag("Enem");
        //EjercicioMEnemigo = enem.GetComponent<EjercicioMEnemigo>();
        //Pulsa espacio para que se active la funcion del salto
        if (Input.GetKeyDown("space"))
        {
            Saltar();
        }
    }

    private void FixedUpdate()
    {
        //Vinculo las variables de mario, con las variables del animator
        //EjercicioMEnemigo.golpe = enemigoC;

        anim.SetInteger("Mov", mov);
        anim.SetBool("Grow", grow);
        anim.SetBool("Jump", jump);
        anim.SetBool("Fin", fin);

        //Controlador para el movimiento o el final
        if (control)
        {
            float movX = Input.GetAxis("Horizontal");
            transform.Translate(movX * vel, 0, 0);
            mov = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
        }
        else if (!control)
        {
            transform.Translate(1.1f * vel, 0f, 0f);
            mov = 1;
            timer += Time.deltaTime;
            print(timer);
            if (timer > 1f)
            {
                print("Acabo el timer");
                vel = 0f;
                fin = true;
                timer = 0;
                print("Has acabado el nivel");
            }
        }      

        //Flips para cuando quieras cambiar de sentido 
        if(Input.GetAxis("Horizontal") < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        //Controlador para el crecimiento
        if (tamaño)
        {
            if(hp == 1)
            {
                hp++;
                tamaño = false;
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.157f, 0.217f);
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0f, -0.01f);
                //animacion de crecer
                grow = true;
            }
            else if (hp > 1)
            {
                tamaño = false;
                //print("Tamaño max");
            }
        }

        //Controlador para el daño
        if (daño)
        {
            if (hp>1)
            {
                hp--;
                //animacion de decrecer
                grow = false;
                //print("Decreces");
                daño = false;
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.14f, 0.18f);
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.01f);              
            }
            else if (hp <= 1)
            {
                hp--;
                daño = false;
                //print("Has muerto");
                vel = 0.0f;
                tiempoR = true;
                myRigid.AddForce(transform.up * fuerza/3);
                gameObject.GetComponent<Collider2D>().enabled = false;
                StartCoroutine("FixedErrorRespawn");
            }
        }
    }

    //Funcion de Respawn
    public void Respawn()
    {
        hp = 1;
        gameObject.GetComponent<Collider2D>().enabled = true;
        transform.position = respawn.gameObject.transform.position;
        vel = 0.03f;
        control = true;
    }

    //Funcion de salto
    void Saltar()
    {
        if (ground)
        {
            jump = true;
            myRigid.AddForce(transform.up * fuerza);
            ground = false;
            //print("Salto");
        }
    }

    //Collisiones
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Tam") || other.gameObject.CompareTag("Dir") || other.gameObject.CompareTag("Salt"))
        {
            ground = true;
            jump = false;
        }
        if (other.gameObject.CompareTag("Enem"))
        {
            if (anguloE > 30f /*&& !enemigoC*/)
            {
                daño = true;
            }
        }
        if (other.gameObject.CompareTag("Champ"))
        {
            tamaño = true;
            //print("Has cogido tamaño");
        }
    }

    //Triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Muerte"))
        {
            //print("Has muerto, vuelves a empezar en 5 segundos");
            tiempoR = true;
        }
        if (other.gameObject.CompareTag("Final"))
        {
            print("Entro al final");
            control = false;
        }

    }

    //Corutina para la muerte 
    IEnumerator FixedErrorRespawn()
    {
        cabexon.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        myRigid.simulated = false;
        myRigid.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(2);
        cabexon.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        myRigid.simulated = true;
    }
}
