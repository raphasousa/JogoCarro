using UnityEngine;
using System.Collections;

public class MovePlayerCar : MonoBehaviour {

    public static float velocidadeFrente = 0f;
    public static float velocidadeRe = 0f;
    private float velocidadeCurva = 10f;

    private float max = 100f;
    private float min = 0f;
    private float maxRe = 30f;

    private float aceleracao = 0.2f;
    private float freio = 1f;
    private float limitante = 0.25f;

    private int seta = 0;

    [SerializeField]
    private GameObject setaDireita;
    [SerializeField]
    private GameObject setaEsquerda;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (MenuController.getPausado() == false)
        {
            //acelera
            if (Input.GetKey("up") || Input.GetKey("w"))
            {
                if (velocidadeRe > min) velocidadeRe = velocidadeRe - freio;
                else if (velocidadeFrente < max) velocidadeFrente = velocidadeFrente + aceleracao;

                if (velocidadeRe < min + 2 && velocidadeRe > min - 2) velocidadeRe = min;
            }
            //freia
            else if (Input.GetKey("down") || Input.GetKey("s"))
            {
                if (velocidadeFrente > min) velocidadeFrente = velocidadeFrente - freio;
                else if (velocidadeRe < maxRe) velocidadeRe = velocidadeRe + aceleracao;

                if (velocidadeFrente < min + 2 && velocidadeFrente > min - 2) velocidadeFrente = min;
            }
            //desacelera
            else
            {
                if (velocidadeFrente > min) velocidadeFrente = velocidadeFrente - aceleracao * 2;
                if (velocidadeRe > min) velocidadeRe = velocidadeRe - aceleracao * 2;
            }

            //controla aceleração
            if (velocidadeFrente > 40f) aceleracao = 0.2f;
            else if (velocidadeFrente > 20f) aceleracao = 0.3f;
            else aceleracao = 0.4f;

            //anda para frente
            if (velocidadeFrente > min && velocidadeRe == min)
                transform.Translate((limitante * velocidadeFrente * Time.deltaTime), 0, 0);

            //ré
            if (velocidadeRe > min && velocidadeFrente == min)
                transform.Translate((-limitante * velocidadeRe * Time.deltaTime), 0, 0);

            //curvas
            if (Input.GetKey("right") || Input.GetKey("d"))
            {
                if (velocidadeFrente > min || velocidadeRe > min)
                    transform.Rotate(0, (5 * velocidadeCurva * Time.deltaTime), 0);
            }
            else if (Input.GetKey("left") || Input.GetKey("a"))
            {
                if (velocidadeFrente > min || velocidadeRe > min)
                    transform.Rotate(0, (-5 * velocidadeCurva * Time.deltaTime), 0);
            }

            //setas
            if (Input.GetKeyDown("z"))
            {
                if (seta == 0) StartCoroutine(PiscaSeta("esquerda"));
                else StartCoroutine(PiscaSeta("desliga"));
            }
            if (Input.GetKeyDown("x"))
            {
                if (seta == 0) StartCoroutine(PiscaSeta("direita"));
                else StartCoroutine(PiscaSeta("desliga"));
            }
        }
    }

    IEnumerator PiscaSeta(string lado)
    {
        int n;
        n = 5; //numero de piscadas
        if (lado == "esquerda")
        {
            seta = 1;
            do
            {
                setaEsquerda.SetActive(true);
                yield return new WaitForSeconds(0.3f);
                setaEsquerda.SetActive(false);
                yield return new WaitForSeconds(0.3f);
                n--;
            } while (n > 0 && seta == 1);
            seta = 0;
        }
        else if (lado == "direita")
        {
            seta = 2;
            do
            {
                setaDireita.SetActive(true);
                yield return new WaitForSeconds(0.3f);
                setaDireita.SetActive(false);
                yield return new WaitForSeconds(0.3f);
                n--;
            } while (n > 0 && seta == 2);
            seta = 0;
        }
        else //desliga as setas
        {
            seta = 0;
            setaDireita.SetActive(false);
            setaEsquerda.SetActive(false);
        }
    }
}
