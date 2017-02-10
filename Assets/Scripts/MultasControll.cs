using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class MultasControll : MonoBehaviour {

    //textos dos tipos de multas na tela de multas
    [SerializeField]
    private Text TextMultas;
    [SerializeField]
    private Text TextGravissima;
    [SerializeField]
    private Text TextGrave;
    [SerializeField]
    private Text TextMedia;
    [SerializeField]
    private Text TextLeve;

    //tela de alerta da multa
    [SerializeField]
    private GameObject telaShowMulta;

    //textos da tela de alerta da multa
    [SerializeField]
    private Text TextMotivo;
    [SerializeField]
    private Text TextTipo;
    [SerializeField]
    private Text TextPontos;

    //quantidade de cada multa
    private static int multas_gravissima;
    private static int multas_grave;
    private static int multas_media;
    private static int multas_leves;

    //pontuação total de multas
    public static int total_pontos;

    //pontuação de cada multa
    private const int GRAVISSIMA = 7;
    private const int GRAVE = 5;
    private const int MEDIA = 4;
    private const int LEVE = 3;

    //usadas para aplicar multa
    private static bool multou;
    private static string motivo_multa;
    private static int tipo_multa;

    //som de mensagen
    private AudioSource som;
    public AudioClip audio_mensagem;

    // Use this for initialization
    void Start () {
        ResetMultas();

        //procura o componente de audio
        som = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		TextMultas.text = "Multas: " + total_pontos + " pontos";
        TextGravissima.text = "Gravíssimas: " + multas_gravissima;
        TextGrave.text = "Graves: " + multas_grave;
        TextMedia.text = "Médias: " + multas_media;
        TextLeve.text = "Leves: " + multas_leves;

        //aplica multas
        if (multou == true) {
            multou = false;
            AtivaMulta(motivo_multa, tipo_multa);
        }
    }

    public static void ResetMultas ()
    {
        multou = false;
        multas_gravissima = 0;
        multas_grave = 0;
        multas_media = 0;
        multas_leves = 0;
        total_pontos = 0;
    }

    public static void AplicaMulta (string motivo)
    {
        if (motivo == "Passar no semaforo vermelho")
        {
            multou = true;
            motivo_multa = motivo;
            tipo_multa = GRAVISSIMA;
        }
        else if (motivo == "Velocidade até 20% maior que o limite")
        {
            multou = true;
            motivo_multa = motivo;
            tipo_multa = MEDIA;
        }
        else if (motivo == "Velocidade entre 20% e 50% maior que o limite")
        {
            multou = true;
            motivo_multa = motivo;
            tipo_multa = GRAVE;
        }
        else if (motivo == "Velocidade mais que 50% maior que o limite")
        {
            multou = true;
            motivo_multa = motivo;
            tipo_multa = GRAVISSIMA;
        }
        else if (motivo == "Parar veículo na calçada")
        {
            multou = true;
            motivo_multa = motivo;
            tipo_multa = LEVE;
        }
    }

    private void AtivaMulta (string motivo, int tipo)
    {
        if (tipo == GRAVISSIMA)
        {
            //incrementa pontuação
            multas_gravissima++;
            total_pontos += GRAVISSIMA;
            //mostra tela de multa
            TextMotivo.text = motivo;
            TextTipo.text = "Multa GRAVÍSSIMA";
            TextPontos.text = "Mais " + tipo_multa + " pontos na carteira.";
            StartCoroutine(ShowTelaMulta());
        }
        else if (tipo == GRAVE)
        {
            //incrementa pontuação
            multas_grave++;
            total_pontos += GRAVE;
            //mostra tela de multa
            TextMotivo.text = motivo;
            TextTipo.text = "Multa GRAVE";
            TextPontos.text = "Mais " + tipo_multa + " pontos na carteira.";
            StartCoroutine(ShowTelaMulta());
        }
        else if (tipo == MEDIA)
        {
            //incrementa pontuação
            multas_media++;
            total_pontos += MEDIA;
            //mostra tela de multa
            TextMotivo.text = motivo;
            TextTipo.text = "Multa MÉDIA";
            TextPontos.text = "Mais " + tipo_multa + " pontos na carteira.";
            StartCoroutine(ShowTelaMulta());
        }
        else if (tipo == LEVE)
        {
            //incrementa pontuação
            multas_leves++;
            total_pontos += LEVE;
            //mostra tela de multa
            TextMotivo.text = motivo;
            TextTipo.text = "Multa LEVE";
            TextPontos.text = "Mais " + tipo_multa + " pontos na carteira.";
            StartCoroutine(ShowTelaMulta());
        }
    }

    IEnumerator ShowTelaMulta ()
    {
        TocaSom(audio_mensagem);
        telaShowMulta.SetActive(true);
        yield return new WaitForSeconds(3f);
        telaShowMulta.SetActive(false);
    }

    void TocaSom(AudioClip audio)
    {
        som.Stop();
        som.clip = audio;
        som.Play();
    }
}
