using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class ExportJSON : MonoBehaviour
{
    public int total_generado;        
    public int total_despachados;   
    public int total_pila;           
    public int tiempo_promedio_despacho;
    public Dictionary<string, int> despachadosPorTipo;
    public string tipoMasDespachado;
}

public class Exportador
{
    public static void Guardar(ExportJSON j)
    {
       
        ExportJSONSerializable serializable = new ExportJSONSerializable(j);

        string json = JsonUtility.ToJson(serializable, true);

        string ruta = Path.Combine(Application.streamingAssetsPath, "resultados.json");
        File.WriteAllText(ruta, json);

        Debug.Log("Resultados guardados en: " + ruta);
        Debug.Log(json);
    }

    [System.Serializable]
   
    private class ExportJSONSerializable
    {
        public int total_generado;         
        public int total_despachados;      
        public int total_pila;           
        public int tiempo_promedio_despacho;
        public List<TipoCantidad> despachadosPorTipo;
        public string tipoMasDespachado;

        public ExportJSONSerializable(ExportJSON j)
        {
            total_generado = j.total_generado;
            total_despachados = j.total_despachados;
            total_pila = j.total_pila;
            tiempo_promedio_despacho = j.tiempo_promedio_despacho;
            tipoMasDespachado = j.tipoMasDespachado;

            despachadosPorTipo = new List<TipoCantidad>();
            if (j.despachadosPorTipo != null)
            {
                foreach (var kvp in j.despachadosPorTipo)
                {
                    despachadosPorTipo.Add(new TipoCantidad() { tipo = kvp.Key, cantidad = kvp.Value });
                }
            }
        }
    }

    [System.Serializable]
    private class TipoCantidad
    {
        public string tipo;
        public int cantidad;
    }
}

