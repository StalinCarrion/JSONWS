using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
public class ImportarPokes : MonoBehaviour
{
    //variable para ver el pokemon
    public Pokemon p;

    IEnumerator Start()
    {

        WWW www = new WWW("http://pokeapi.co/api/v2/pokemon/" + 1 + "/");

        //WWW www = new WWW("http://pokeapi.co/api/v2/pokemon/" + 65 + "/");
        yield return www;
        print(www.text);
        JsonData data = JsonMapper.ToObject(www.text);

        p.Nombre = data["name"].ToString();
        p.baseXP = int.Parse(data["base_experience"].ToString());

        if (data["types"].Count > 1)
        { 
            p.Tipo1 = data["types"][1]["type"]["name"].ToString();
            p.Tipo2 = data["types"][0]["type"]["name"].ToString();

        }
        else
        {
            p.Tipo1 = data["types"][0]["type"]["name"].ToString();
            p.Tipo2 = "NONE";
        }

    }


}

