using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Elements : MonoBehaviour
{

    string path;
    string jsonString;

    public Text _Mytext1;
    public Text _Mytext2;
    private bool sw;
    Elemento[] elemento;
    private bool enter = false;

    public List<string> ListaElementos = new List<string>();

    public GameObject mensaje;

    public UnityEvent m_MyEvent;

    void Start()
    {
        
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();

        //m_MyEvent.AddListener(Señal);

        //_Mytext1.text = "";
        //_Mytext2.text = "";
        
        path = Application.dataPath + "/elements.json";
        jsonString = File.ReadAllText(path);
        elemento = JsonHelper.FromJson<Elemento>(jsonString);
        Debug.Log(elemento);

        

    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {                
        GameObject element1 = GameObject.Find("Hierro");
        GameObject element2 = GameObject.Find("Oxigeno");
        GameObject element3 = GameObject.Find("Cobre");
        //mensaje = GameObject.Find("Interfaz");
        sw = gameObject.GetComponent<Combinar>().m_comb;
        if (sw)
        {
            /*if (element1.transform.position.x < element2.transform.position.x)
            {
                ShowText(_Mytext1, "Hierro", sw);
                ShowText(_Mytext2, "Oxigeno", sw);
            }
            else
            {
                ShowText(_Mytext2, "Hierro", sw);
                ShowText(_Mytext1, "Oxigeno", sw);
            }*/
            /*if (element1.GetComponent<Rastreo>().e_active)
            {
                Debug.Log(Find("Hierro").cb);
            }*/
            
        }
        else
        {
            /*_Mytext2.text = "";
            if (!ShowText(_Mytext1, "Hierro", element1.GetComponent<Rastreo>().e_active))
            {
                ShowText(_Mytext1, "Oxigeno", element2.GetComponent<Rastreo>().e_active);
            }*/
        }
    }

    [System.Serializable]
    public class Elemento
    {
        public string Nombre;
        public string Simbolo;
        public int[] Valencia;
        public bool cb;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Elementos;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Elementos = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Elementos = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Elementos;
        }
    }

    public Elemento Find(string name)
    {
        Elemento elementSearch = new Elemento();
        foreach (Elemento x in elemento)
        {
            if (x.Nombre.Equals(name))
            {
                elementSearch = x;
                break;
            }
        }
        return elementSearch;
    }

    private bool ShowText(Text txt, string st, bool sw)
    {
        if (sw)
        {
            var ele = Find(st);
            txt.text = ele.Nombre + " " + ele.Simbolo + " " + ele.cb + "\n";
            for (int i = 0; i < ele.Valencia.Length; i++)
            {
                txt.text = txt.text + ele.Valencia[i] + "\n";
            }
            return true;
        }
        else
        {
            txt.text = "";
            return false;
        }
    }

    private IEnumerator CoroutineExample()
    {
        Debug.Log("CoroutineExample started at " + Time.time.ToString() + "s");
        enter = true;
        mensaje.SetActive(true);
        yield return new WaitForSeconds(3f);
        Find("Cobre").cb = true;
        mensaje.SetActive(false);
        Debug.Log("Coroutine Iteration Successful at " + Time.time.ToString() + "s");
    }

    public void MostrarLista()
    {
        _Mytext1.text = "";
        foreach (string s in ListaElementos)
        {
            Debug.Log(s);
            _Mytext1.text = _Mytext1.text + s + "\n";
        }
    }

}
