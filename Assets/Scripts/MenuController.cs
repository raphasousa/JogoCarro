using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private GameObject posInicial;
    [SerializeField]
    private GameObject carro;
    [SerializeField]
    private Text TextVelocidade;
    [SerializeField]
    private GameObject telaPause;
    [SerializeField]
    private GameObject telaJogo;
    [SerializeField]
    private GameObject telaAjuda;
    [SerializeField]
    private GameObject telaMulta;
    [SerializeField]
    private GameObject telaShowMulta;
    [SerializeField]
    private GameObject telaGameOver;

    private static bool pausado = false;

    public static bool getPausado()
    {
        return pausado;
    }

    // Use this for initialization
    void Start () {
        telaPause.SetActive(false);
        telaAjuda.SetActive(false);
        telaShowMulta.SetActive(false);
        telaGameOver.SetActive(false);
        telaJogo.SetActive(true);
        telaMulta.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (getPausado() == false)
        {
            //velocimetro frente
            if (MovePlayerCar.velocidadeFrente > 0f && MovePlayerCar.velocidadeRe == 0f)
                TextVelocidade.text = "Speed: " + Mathf.FloorToInt(MovePlayerCar.velocidadeFrente) + " km/h";

            //velocimetro ré
            if (MovePlayerCar.velocidadeRe > 0f && MovePlayerCar.velocidadeFrente == 0f)
                TextVelocidade.text = "Speed: " + Mathf.FloorToInt(MovePlayerCar.velocidadeRe) + " km/h";
        }

        //pause
        if (Input.GetKeyDown("p"))
        {
            PausaJogo();
        }

        //gameover
        if (MultasControll.total_pontos >= 20)
        {
            pausado = true;
            Time.timeScale = 0f;
            telaShowMulta.SetActive(false);
            telaGameOver.SetActive(true);
        }
    }

    public void PausaJogo ()
    {
        if (getPausado() == false)
        {
            Time.timeScale = 0f;
            pausado = true;
            telaJogo.SetActive(false);
            telaAjuda.SetActive(false);
            telaShowMulta.SetActive(false);
            telaGameOver.SetActive(false);
            telaPause.SetActive(true);
            telaMulta.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausado = false;
            telaPause.SetActive(false);
            telaAjuda.SetActive(false);
            telaShowMulta.SetActive(false);
            telaGameOver.SetActive(false);
            telaJogo.SetActive(true);
            telaMulta.SetActive(true);
        }
    }

    public void ShowTelaAjuda ()
    {
        telaPause.SetActive(false);
        telaJogo.SetActive(false);
        telaMulta.SetActive(false);
        telaAjuda.SetActive(true);
    }

    public void VoltaTelaAjuda()
    {
        telaAjuda.SetActive(false);
        telaJogo.SetActive(false);
        telaMulta.SetActive(true);
        telaPause.SetActive(true);
    }

    public void ResetPosicao ()
    {
        //reseta o jogo
        carro.transform.position = posInicial.transform.position;
        carro.transform.rotation = posInicial.transform.rotation;
        ColisaoControll.estado = 0;
        MovePlayerCar.velocidadeRe = 0f;
        MovePlayerCar.velocidadeFrente = 1f;
        //zera as multas
        MultasControll.ResetMultas();
        //tira o jogo do pause
        PausaJogo();
    }
}
