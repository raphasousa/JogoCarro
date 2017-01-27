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

    private static bool pausado = false;

    public static bool getPausado()
    {
        return pausado;
    }

    // Use this for initialization
    void Start () {
        telaPause.SetActive(false);
        telaAjuda.SetActive(false);
        telaJogo.SetActive(true);
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
    }

    public void PausaJogo ()
    {
        if (getPausado() == false)
        {
            Time.timeScale = 0f;
            pausado = true;
            telaJogo.SetActive(false);
            telaAjuda.SetActive(false);
            telaPause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausado = false;
            telaPause.SetActive(false);
            telaAjuda.SetActive(false);
            telaJogo.SetActive(true);
        }
    }

    public void ShowTelaAjuda ()
    {
        telaPause.SetActive(false);
        telaJogo.SetActive(false);
        telaAjuda.SetActive(true);
    }

    public void VoltaTelaAjuda()
    {
        telaAjuda.SetActive(false);
        telaJogo.SetActive(false);
        telaPause.SetActive(true);
    }

    public void ResetPosicao ()
    {
        carro.transform.position = posInicial.transform.position;
        carro.transform.rotation = posInicial.transform.rotation;
        RotatePontes.estado = 0;
        MovePlayerCar.velocidadeRe = 0f;
        MovePlayerCar.velocidadeFrente = 1f;
        PausaJogo();
    }
}
