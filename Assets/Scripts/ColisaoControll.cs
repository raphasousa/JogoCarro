using UnityEngine;
using System.Collections;

public class ColisaoControll : MonoBehaviour {

    public static int estado = 0;

    private const int RETO = 0;
    private const int SUBINDO = 1;
    private const int DESCENDO = -1;

    private float velocF;
    private float velocR;

    private int velocRadar;

    //colisões

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Predio")
        {
            DesaceleraColisao();
        }
        else if (collision.transform.tag == "Planta")
        {
            DesaceleraColisao();
        }
        else if (collision.transform.tag == "Poste")
        {
            DesaceleraColisao();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "Radar")
        {
            if (col.transform.name == "Limite80") velocRadar = 80;
            else if (col.transform.name == "Limite60") velocRadar = 60;
            else if (col.transform.name == "Limite40") velocRadar = 40;
            else if (col.transform.name == "Limite20") velocRadar = 20;

            Radar(velocRadar);
        }
        else if (col.transform.tag == "Calçada")
        {
            velocF = MovePlayerCar.velocidadeFrente;

            if (velocF > 0f && velocF < 15f)
            {
                MultasControll.AplicaMulta("Parar veículo na calçada");
            }
        }
    }

    void Radar (int limite)
    {
        velocF = MovePlayerCar.velocidadeFrente;

        if (velocF > limite+2)
        {
            if (velocF/limite <= 1.2f) MultasControll.AplicaMulta("Velocidade até 20% maior que o limite");
            else if (velocF / limite <= 1.5f) MultasControll.AplicaMulta("Velocidade entre 20% e 50% maior que o limite");
            else MultasControll.AplicaMulta("Velocidade mais que 50% maior que o limite");
        }
    }

    void DesaceleraColisao ()
    {
        velocF = MovePlayerCar.velocidadeFrente;
        velocR = MovePlayerCar.velocidadeRe;

        if (velocF > 0f) //colisao frontal
        {
            MovePlayerCar.velocidadeFrente = 0f;
            MovePlayerCar.velocidadeRe = velocF * 0.4f;
        }
        else //colisao de re
        {
            MovePlayerCar.velocidadeRe = 0f;
            MovePlayerCar.velocidadeFrente = velocR * 0.4f;
        }
    }

    //rotações nas pontes

    void OnTriggerEnter(Collider col)
    {
        velocF = MovePlayerCar.velocidadeFrente;

        if (col.transform.tag == "Base")
        {
            if (estado == DESCENDO) Reto();
            else if (estado == RETO)
            {
                if (velocF > 0f) Sobe();
                else Desce();
            }
            else Reto();
        }

        if (col.transform.tag == "Topo")
        {
            if (estado == SUBINDO) Reto();
            else if (estado == RETO)
            {
                if (velocF > 0f) Desce();
                else Sobe();
            }
            else Reto();
        }

        if (col.transform.tag == "Meio")
        {
            if (estado == SUBINDO) SobeMais();
            else if (estado == DESCENDO) DesceMais();
        }
    }

    void Sobe ()
    {
        estado = SUBINDO;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0.0f;
        temp.z = 10.0f;
        transform.rotation = Quaternion.Euler(temp);
    }

    void SobeMais ()
    {
        estado = SUBINDO;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0.0f;
        temp.z = 15.0f;
        transform.rotation = Quaternion.Euler(temp);
    }

    void Desce ()
    {
        estado = DESCENDO;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0.0f;
        temp.z = -10.0f;
        transform.rotation = Quaternion.Euler(temp);
    }

    void DesceMais()
    {
        estado = SUBINDO;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0.0f;
        temp.z = -15.0f;
        transform.rotation = Quaternion.Euler(temp);
    }

    void Reto ()
    {
        estado = RETO;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0.0f;
        temp.z = 0.0f;
        transform.rotation = Quaternion.Euler(temp);
    }
}
