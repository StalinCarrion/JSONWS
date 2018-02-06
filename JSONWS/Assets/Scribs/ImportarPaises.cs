﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using UnityEngine.UI;


public class ImportarPaises : MonoBehaviour
{
    public InputField texbox1;
    public Material materialO;
    public Material materialS;
    public Material materialOL;
    public Material materialP;
   
    //public GameObject canvas;
    public GameObject origin;
    public GameObject destino;
   // public GameObject texto;
    public Text nombreObjeto;
    //Objeto donde se guarda la esfera
    public GameObject sphere;
    public GameObject cube;
    //para generar el minimo y maximo en la posicion random
    public int min, max;
    //Creacino de la lista que contendra todo
    public List<Nombres> nombre = new List<Nombres>();
    public int i;
    public int nEsferas = 8;


    public Color colorStart = Color.red;
    public Color colorEnd = Color.green;
    public Renderer rend;

    IEnumerator Start()
    {
        
       
        //link de la consulta donde se sustraen los datos
        WWW www = new WWW("http://es-la.dbpedia.org/sparql?default-graph-uri" +
            "=&query=select+%3Chttp%3A%2F%2Fes-la.dbpedia.org%2Fresource%2FEcuador%" +
            "3E+%3Fp+%3Fo+where+%7B%3Chttp%3A%2F%2Fes-la.dbpedia.org%2Fresource%2FEcuador%3E" +
            "+%3Fp+%3Fo%7D+LIMIT+" + 8 + "&format=application%2Fsparql-results%2Bjson&timeout=0&debug=on");
        //espera cuando se carge los datos
        yield return www;
        //para presentar en consola
        
        print(www.text);
        //leer los datos presentados
        JsonData data = JsonMapper.ToObject(www.text);
        
        var pRed = GeneratedPosition();
        for (i = 0; i < nEsferas; i++)
        {
            GameObject textGo = new GameObject("Objeto");
            GameObject textSujeto = new GameObject("Sujeto");

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
            string strSujeto;
            strObjeto = nom.Objeto;
            strSujeto = nom.Sujeto;

            texbox1.text = ("Posicion del Objeto: " + i + " nombre objeto: " + strObjeto);

            if (nom.Sujeto != " " && nom.TypeSujeto != " " && nom.Objeto != " " && nom.TypePredicado == "uri")
            {
                origin = Instantiate(sphere, pRed, Quaternion.identity);
                origin.GetComponent<MeshRenderer>().material = materialS;


                var traza = origin.AddComponent<LineRenderer>();
                traza.startWidth = traza.endWidth = .2f;
                traza.useWorldSpace = true;

                traza.positionCount = 2;
                traza.SetPosition(0, pRed);
                //Para generar una textura creada llama linea 
                //Material whiteDiffuseMat = new Material(Shader.Find("Unlit/linea"));
                //traza.material = whiteDiffuseMat;
                //Asignarle un color predeterminado en este caso black
                traza.material = materialP ;
                //destino.GetComponent<MeshRenderer>().material = materialO;

                if (nom.TypeObjeto == "uri")
                {
                    destino = Instantiate(sphere, pBlue, Quaternion.identity);
                    //destino.GetComponent<Renderer>().material.color = Color.blue;
                    destino.GetComponent<MeshRenderer>().material = materialO;

                }
                else
                {
                    destino = Instantiate(cube, pBlue, Quaternion.identity);
                    destino.GetComponent<MeshRenderer>().material = materialOL;
                }

                
                
                

                traza.SetPosition(1, pBlue);
                //textoo.SetActive(destino);
                //textoo.transform.position = destino.transform.position;
                
                Debug.Log("Posicion del Objeto: " + i + " nombre objeto: " + strObjeto);
                //Para poner el texto de las esferas de objeto
                textGo.transform.position = destino.transform.position;
                //Para poner el texto de las esferas de objeto
                textSujeto.transform.position = origin.transform.position;

                TextMesh textMesh = textGo.AddComponent<TextMesh>();
                TextMesh textMeshSujeto = textSujeto.AddComponent<TextMesh>();

                textMesh.text = strObjeto;
                textMeshSujeto.text = strSujeto;
                //textGo.Color = new Color(1, 0, 1, 0.5f); //violeta transparente al 50%   100%, 64.7%, 0%, 1
                textMesh.color = new Color(0, 255, 0, 1);
                textMeshSujeto.color = new Color(100, 64.7f, 0, 1);

            }

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

    public void MyPointerEnter()
    {
        for (int i = 0; i < nEsferas; i++)
        {
            origin.GetComponent<Renderer>().material.color = Color.black;
            Debug.Log("funciona");
        }
        
    }

    public void MyPointerLeave()
    {
        for (int i = 0; i < nEsferas; i++)
        {
            origin.GetComponent<Renderer>().material.color = Color.white;
            Debug.Log("Sigue Funcionando");
        }
        
    }

}








