using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllSemaforos : MonoBehaviour {

    [SerializeField]
    private GameObject semDireita;
    [SerializeField]
    private GameObject semEsquerda;
    [SerializeField]
    private GameObject semBaixo;
    [SerializeField]
    private GameObject semCimaLado;
    [SerializeField]
    private GameObject semCimaMeio;

    private Light[] luzes_semDireita;
    private Light[] luzes_semEsquerda;
    private Light[] luzes_semBaixo;
    private Light[] luzes_semCimaLado;
    private Light[] luzes_semCimaMeio;

    private float timeRed = 1f;
    private float timeGreen = 6f;
    private float timeYellow = 3f;

    void Start () {
        //busca todas as luzes dos semaforos
        luzes_semDireita = semDireita.GetComponentsInChildren<Light>();
        luzes_semEsquerda = semEsquerda.GetComponentsInChildren<Light>();
        luzes_semBaixo = semBaixo.GetComponentsInChildren<Light>();
        luzes_semCimaLado = semCimaLado.GetComponentsInChildren<Light>();
        luzes_semCimaMeio = semCimaMeio.GetComponentsInChildren<Light>();

        StartCoroutine(ControlaSemaforo());
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Semaforo")
        {
            if (col.transform.name == "Quad_Baixo")
            {
                if (IsRed("Baixo"))
                {
                    //Debug.Log("Passou no vermelho em baixo");
                    MultasControll.AplicaMulta("Passar no semaforo vermelho");
                }
            }
            else if (col.transform.name == "Quad_Cima")
            {
                if (IsRed("CimaLado"))
                {
                    //Debug.Log("Passou no vermelho em cima");
                    MultasControll.AplicaMulta("Passar no semaforo vermelho");
                }
            }
            else if (col.transform.name == "Quad_Direita")
            {
                if (IsRed("Direita"))
                {
                    //Debug.Log("Passou no vermelho em direita");
                    MultasControll.AplicaMulta("Passar no semaforo vermelho");
                }
            }
            else if (col.transform.name == "Quad_Esquerda")
            {
                if (IsRed("Esquerda"))
                {
                    //Debug.Log("Passou no vermelho em esquerda");
                    MultasControll.AplicaMulta("Passar no semaforo vermelho");
                }
            }
        }
    }

    IEnumerator ControlaSemaforo ()
    {
        //liga todos os vermelhos
        AtivaLuz(luzes_semBaixo, "Red");
        AtivaLuz(luzes_semDireita, "Red");
        AtivaLuz(luzes_semCimaLado, "Red");
        AtivaLuz(luzes_semCimaMeio, "Red");
        AtivaLuz(luzes_semEsquerda, "Red");

        while (true)
        {
            //liga o de baixo
            yield return new WaitForSeconds(timeRed);
            AtivaLuz(luzes_semBaixo, "Green");
            yield return new WaitForSeconds(timeGreen);
            AtivaLuz(luzes_semBaixo, "Yellow");
            yield return new WaitForSeconds(timeYellow);
            AtivaLuz(luzes_semBaixo, "Red");

            //liga o da direita
            yield return new WaitForSeconds(timeRed);
            AtivaLuz(luzes_semDireita, "Green");
            yield return new WaitForSeconds(timeGreen);
            AtivaLuz(luzes_semDireita, "Yellow");
            yield return new WaitForSeconds(timeYellow);
            AtivaLuz(luzes_semDireita, "Red");

            //liga os de cima
            yield return new WaitForSeconds(timeRed);
            AtivaLuz(luzes_semCimaLado, "Green");
            AtivaLuz(luzes_semCimaMeio, "Green");
            yield return new WaitForSeconds(timeGreen);
            AtivaLuz(luzes_semCimaLado, "Yellow");
            AtivaLuz(luzes_semCimaMeio, "Yellow");
            yield return new WaitForSeconds(timeYellow);
            AtivaLuz(luzes_semCimaLado, "Red");
            AtivaLuz(luzes_semCimaMeio, "Red");

            //liga o da esquerda
            yield return new WaitForSeconds(timeRed);
            AtivaLuz(luzes_semEsquerda, "Green");
            yield return new WaitForSeconds(timeGreen);
            AtivaLuz(luzes_semEsquerda, "Yellow");
            yield return new WaitForSeconds(timeYellow);
            AtivaLuz(luzes_semEsquerda, "Red");
        }
    }

    void AtivaLuz (Light[] luzes, string cor)
    {
        foreach (Light luz in luzes)
        {
            if (luz.name == cor + " light" || luz.name == cor + " light 1") luz.enabled = true;
            else luz.enabled = false;
        }
    }

    Light[] GetLuzes (string semaforo)
    {
        if (semaforo == "Baixo") return luzes_semBaixo;
        else if (semaforo == "Esquerda") return luzes_semEsquerda;
        else if (semaforo == "Direita") return luzes_semDireita;
        else if (semaforo == "CimaLado") return luzes_semCimaLado;
        else return luzes_semCimaMeio;
    }

    public bool IsRed (string semaforo)
    {
        bool ok = false;
        Light[] luzes = GetLuzes(semaforo);
        
        foreach (Light luz in luzes)
        {
            if (luz.name == "Red light") ok = luz.enabled;
        }
        return ok;
    }
}
