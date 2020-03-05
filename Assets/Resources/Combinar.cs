using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Combinar : MonoBehaviour
{

    public bool m_comb;

    private bool enter = false;
    private bool sw = false;
    GameObject element1;
    GameObject ox;
    GameObject element2;

    // Start is called before the first frame update
    void Start()
    {
        m_comb = false;
        element1 = GameObject.Find("Oxigeno");
    }

    // Update is called once per frame
    void Update()
    {
        comprobar();

        if (m_comb)
        {

            var elementOne = element1.GetComponentsInChildren<Renderer>(true);
            var elementTwo = element2.GetComponentsInChildren<Renderer>(true);

            foreach (var component in elementOne)
                component.enabled = false;

            foreach (var component in elementTwo)
                component.enabled = false;

            var Comb = ox.GetComponentsInChildren<Renderer>(true);

            foreach (var component in Comb)
                component.enabled = true;

            var pos1 = element1.transform.position;
            var rot1 = element1.transform.rotation;

            Vector3 pos2 = element2.transform.position;

            Vector3 pos3, dist;

            float distx = Mathf.Abs(pos1.x - pos2.x) / 2;
            float disty = Mathf.Abs(pos1.y - pos2.y) / 2;
            float distz = Mathf.Abs(pos1.z - pos2.z) / 2;

            dist = new Vector3(distx, disty, distz);

            pos3.x = (pos1.x > pos2.x) ? pos2.x + dist.x : pos1.x + dist.x;
            pos3.y = (pos1.y > pos2.y) ? pos2.y + dist.y : pos1.y + dist.y;
            pos3.z = (pos1.z > pos2.z) ? pos2.z + dist.z : pos1.z + dist.z;

            var rot3 = rot1;

            ox.transform.SetPositionAndRotation(pos3, rot3);

        }
        else
        {
            var Comb = GameObject.Find("Oxidos").GetComponentsInChildren<Renderer>(true);            

            foreach (var component in Comb)
                component.enabled = false;
            
        }

        if (enter == false)
        {
            StartCoroutine(CoroutineExample());
        }



    }

    private IEnumerator CoroutineExample()
    {
        enter = true;
        GetComponent<Elements>().MostrarLista();
        comprobar();
        yield return new WaitForSeconds(3f);
        enter = false;
    }

    private bool buscar(string s)
    {
        if (GetComponent<Elements>().ListaElementos.Contains(s))
        {
            return true;
        }
        else
            return false;
    }

    private void comprobar()
    {
        activar();
        int size;
        size = GetComponent<Elements>().ListaElementos.Count;
        if (size == 2)
        {
            if (buscar("Oxigeno"))
            {
                m_comb = true;
                if (buscar("Hierro"))
                {
                    if (verificar("Hierro"))
                    {
                        ox = GameObject.Find("InfoOxido");
                    }
                    element2 = GameObject.Find("Hierro");
                }
                if (buscar("Cobre"))
                {
                    if (verificar("Cobre"))
                    {
                        ox = GameObject.Find("InfoOxido2");
                    }
                    element2 = GameObject.Find("Cobre");
                }
                if (buscar("Oro"))
                {
                    if (verificar("Oro"))
                    {
                        ox = GameObject.Find("InfoOxidoOro");
                    }
                    element2 = GameObject.Find("Oro");
                }
                if (buscar("Plata"))
                {
                    if (verificar("Plata"))
                    {
                        ox = GameObject.Find("InfoOxidoPlata");
                    }
                    element2 = GameObject.Find("Plata");
                }
                if (buscar("Plomo"))
                {
                    if (verificar("Plomo"))
                    {
                        ox = GameObject.Find("InfoOxidoPlomo");
                    }
                    element2 = GameObject.Find("Plomo");
                }
            }
            else
            {
                m_comb = false;
            }
        }
        else
        {
            m_comb = false;
        }
    }


    private bool verificar(string nombre)
    {

        if (!GetComponent<Elements>().Find(nombre).cb)
        {
            ox = GameObject.Find("Espera");
            if (!sw)
            {
                StartCoroutine(MensajeEspera(nombre));
            }
            return false;
        }
        else
            return true;
    }

    private IEnumerator MensajeEspera(string s)
    {
        sw = true;
        var Comb = ox.GetComponentsInChildren<Renderer>(true);

        foreach (var component in Comb)
            component.enabled = true;
        yield return new WaitForSeconds(3f);
        

        foreach (var component in Comb)
            component.enabled = false;
        GetComponent<Elements>().Find(s).cb = true;
        sw = false;
    }


    private void activar()
    {
        Manage("Plomo");
        Manage("Oro");
        Manage("Plata");
        Manage("Cobre");
        Manage("Hierro");
        Manage("Oxigeno");
    }


    private void Manage(string ele)
    {
        if (GameObject.Find(ele).GetComponent<Rastreo>().e_active)
        {
            if (!GetComponent<Elements>().ListaElementos.Contains(ele))
            {
                GetComponent<Elements>().ListaElementos.Add(ele);
            }
        }
        else
        {
            GetComponent<Elements>().ListaElementos.Remove(ele);
        }
    }
}
