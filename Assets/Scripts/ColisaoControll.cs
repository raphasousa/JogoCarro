using UnityEngine;
using System.Collections;

public class ColisaoControll : MonoBehaviour {

    private float velocCarro;
    private int velocRadar;

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
            velocCarro = MenuController.getVelocidade();

            if (velocCarro > 0f && velocCarro < 15f)
            {
                MultasControll.AplicaMulta("Parar veículo na calçada");
            }
        }
    }

    void Radar (int limite)
    {
        velocCarro = MenuController.getVelocidade();

        if (velocCarro > limite+4)
        {
            if (velocCarro / limite <= 1.2f) MultasControll.AplicaMulta("Velocidade até 20% maior que o limite");
            else if (velocCarro / limite <= 1.5f) MultasControll.AplicaMulta("Velocidade entre 20% e 50% maior que o limite");
            else MultasControll.AplicaMulta("Velocidade mais que 50% maior que o limite");
        }
    }
}
