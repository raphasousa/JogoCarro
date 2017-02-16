using UnityEngine;
using System.Collections;

public class SetasControll : MonoBehaviour {
    
    private int seta = 0;

    [SerializeField]
    private GameObject setaDireita;
    [SerializeField]
    private GameObject setaEsquerda;
	
	// Update is called once per frame
	void Update () {
        if (MenuController.getPausado() == false)
        {
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
