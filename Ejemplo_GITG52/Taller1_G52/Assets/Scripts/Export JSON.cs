using UnityEngine;
using System.Collections.Generic;
using System.IO;

[SerializeField]

public class ExportJSON : MonoBehaviour
{
    public float total_generado;
    public float total_despachados;
    public float total_pila;
    public int tiempo_promedio_despacho;
    public int despachados_por_tipo;
    public Dictionary<string, int> despachadosPorTipo;
    public string tipoMasDespachado;

}

public class Exportador
{
    public static void Guardar(ExportJSON j)
    {
        string json = JsonUtility.ToJson(r, true);

        string ruta = Path.Combine(Application.persistentDataPath, "resultados.json");
        File.WriteAllText(ruta, json);

        Debug.Log("Resultados guardados en: " + ruta);
        Debug.Log(json);
       }
    }