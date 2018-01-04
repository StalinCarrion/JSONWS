using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class ImportarPaises : MonoBehaviour
{
    public InputField texbox1;
    public GameObject origin;
    public GameObject destino;
    public GameObject texto;
    //Objeto donde se guarda la esfera
    public GameObject sphere;
    //para generar el minimo y maximo en la posicion random
    public int min, max;
    //Creacino de la lista que contendra todo
    public List<Nombres> nombre = new List<Nombres>();
    public int i;
    IEnumerator Start()
    {
        int nEsferas = 15;
       
        //link de la consulta donde se sustraen los datos
        WWW www = new WWW("http://es-la.dbpedia.org/sparql?default-graph-uri" +
            "=&query=select+%3Chttp%3A%2F%2Fes-la.dbpedia.org%2Fresource%2FEcuador%" +
            "3E+%3Fp+%3Fo+where+%7B%3Chttp%3A%2F%2Fes-la.dbpedia.org%2Fresource%2FEcuador%3E" +
            "+%3Fp+%3Fo%7D+LIMIT+" + 20 + "&format=application%2Fsparql-results%2Bjson&timeout=0&debug=on");
        //espera cuando se carge los datos
        yield return www;
        //para presentar en consola
        print(www.text);
        //leer los datos presentados
        JsonData data = JsonMapper.ToObject(www.text);
        
        var pRed = GeneratedPosition();
        for (i = 0; i < nEsferas; i++)
        {
            GameObject textoo = Instantiate(texto, pRed, Quaternion.identity);
            textoo.GetComponent<Renderer>().material.color = Color.black;
            
            

            var pBlue = GeneratedPosition();

            //se crear una variable de Nombre
            Nombres nom = new Nombres();

            //se ingresa a cada variable el dato que se sustrae
            nom.Sujeto = data["results"]["bindings"][i]["callret-0"]["value"].ToString();
            nom.TypeSujeto = data["results"]["bindings"][i]["callret-0"]["type"].ToString();
            nom.Predicado = data["results"]["bindings"][i]["p"]["value"].ToString();
            nom.TypePredicado = data["results"]["bindings"][i]["p"]["type"].ToString();
            nom.Objeto = data["results"]["bindings"][i]["o"]["value"].ToString();
            nom.TypeObjeto = data["results"]["bindings"][i]["o"]["type"].ToString();
            nombre.Add(nom);
            //Para ver el nombre del objeto
            string strObjeto;
            strObjeto = nom.Objeto;

            Debug.Log("Posicion del Objeto: "+i+" nombre objeto: " + strObjeto);
            texbox1.text = ("Posicion del Objeto: " + i + " nombre objeto: " + strObjeto);



            if (nom.Sujeto != " " && nom.TypeSujeto == "uri" && nom.Objeto != " " && nom.TypePredicado == "uri")
            {
                origin = Instantiate(sphere, pRed, Quaternion.identity);
                origin.GetComponent<Renderer>().material.color = Color.red;
                var traza = origin.AddComponent<LineRenderer>();
                traza.startWidth = traza.endWidth = .2f;
                traza.useWorldSpace = true;

                traza.positionCount = 2;
                traza.SetPosition(0, pRed);
                destino = Instantiate(sphere, pBlue, Quaternion.identity);
                destino.GetComponent<Renderer>().material.color = Color.blue;

                traza.SetPosition(1, pBlue);

                
            }
            Debug.Log("Posicion: "+i+" y la esfera " + destino);
        }



    }
    Vector3 GeneratedPosition()
    {
        int x, y, z;
        x = Random.Range(min, max);
        y = Random.Range(min, max);
        z = Random.Range(min, max);
        return new Vector3(x, y, z);

    }

}








