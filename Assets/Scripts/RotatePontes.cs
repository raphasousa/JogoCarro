using UnityEngine;
using System.Collections;

public class RotatePontes : MonoBehaviour {

    public static int estado = 0;
    private const int reto = 0;
    private const int subindo = 1;
    private const int descendo = -1;

    private float velocF;
    private float velocR;

    //colisões

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Predio")
        {
            DesaceleraColisao();
        }

        if (collision.transform.tag == "Planta")
        {
            DesaceleraColisao();
        }

        if (collision.transform.tag == "Poste")
        {
            DesaceleraColisao();
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
            if (estado == descendo) Reto();
            else if (estado == reto)
            {
                if (velocF > 0f) Sobe();
                else Desce();
            }
            else Reto();
        }

        if (col.transform.tag == "Topo")
        {
            if (estado == subindo) Reto();
            else if (estado == reto)
            {
                if (velocF > 0f) Desce();
                else Sobe();
            }
            else Reto();
        }

        if (col.transform.tag == "Meio")
        {
            if (estado == subindo) SobeMais();
            else if (estado == descendo) DesceMais();
        }
    }

    void Sobe ()
    {
        estado = subindo;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0.0f;
        temp.z = 10.0f;
        transform.rotation = Quaternion.Euler(temp);
    }

    void SobeMais ()
    {
        estado = subindo;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0.0f;
        temp.z = 15.0f;
        transform.rotation = Quaternion.Euler(temp);
    }

    void Desce ()
    {
        estado = descendo;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0.0f;
        temp.z = -10.0f;
        transform.rotation = Quaternion.Euler(temp);
    }

    void DesceMais()
    {
        estado = subindo;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0.0f;
        temp.z = -15.0f;
        transform.rotation = Quaternion.Euler(temp);
    }

    void Reto ()
    {
        estado = reto;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0.0f;
        temp.z = 0.0f;
        transform.rotation = Quaternion.Euler(temp);
    }
}
