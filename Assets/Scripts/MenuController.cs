using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private GameObject posInicial;
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
    [SerializeField]
    private GameObject Carro;

    private static bool pausado = false;
    private static float velocidadeCarro = 0f;

    private CarController m_Car; // the car controller we want to use

    public static bool getPausado()
    {
        return pausado;
    }

    public static float getVelocidade()
    {
        return velocidadeCarro;
    }

    // Use this for initialization
    void Start () {
        telaPause.SetActive(false);
        telaAjuda.SetActive(false);
        telaShowMulta.SetActive(false);
        telaGameOver.SetActive(false);
        telaJogo.SetActive(true);
        telaMulta.SetActive(true);

        // get the car controller
        m_Car = Carro.GetComponent<CarController>();
    }
	
	// Update is called once per frame
	void Update () {
        velocidadeCarro = m_Car.CurrentSpeed * 1.15f;

        //atualiza velocidade
        if (getPausado() == false)
        {
            TextVelocidade.text = "Speed: " + Mathf.FloorToInt(velocidadeCarro) + " km/h";
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
        {   //pausa o jogo
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
        {   //tira do pause
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
        //reseta posição do carro
        Carro.transform.position = posInicial.transform.position;
        Carro.transform.rotation = posInicial.transform.rotation;
        //zera a velocidade
        m_Car.SetCurrentSpeedToZero = 0f;
        //zera as multas
        MultasControll.ResetMultas();
        //tira o jogo do pause
        PausaJogo();
    }
}
