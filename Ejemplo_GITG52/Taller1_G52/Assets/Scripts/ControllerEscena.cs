using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using packageProductosPila;

public class ControllerEscena : MonoBehaviour
{
    [Header("Sistemas")]
    public PilaProductos pilaProductos; 
    public ExportJSON exportJSON;       
    private List<Producto> catalogo = new List<Producto>();

    [Header("UI - Indicadores en vivo")]
    public TMP_Text txtIndicadores; 

    [Header("UI - Panel final")]
    public GameObject panelResultados;
    public TMP_Text txtPanelResultados;

    private Coroutine coGenerar;
    private Coroutine coDespachar;
    private bool simulacionActiva = false;

    private int totalGenerados = 0;
    private int totalDespachados = 0;
    private float tiempoTotalDespacho = 0f;
    private int conteoDespachos = 0;
    private Dictionary<string, int> despachadosPorTipo = new Dictionary<string, int>();

    void Start()
    {
        if (panelResultados != null)
            panelResultados.SetActive(false); 

        CargarCatalogoDesdeTxt();
        ActualizarIndicadores();
    }

    
    public void IniciarSimulacion()
    {
        if (simulacionActiva) return;
        simulacionActiva = true;

        coGenerar = StartCoroutine(GenerarProductos());
        coDespachar = StartCoroutine(DespacharProductos());

        Debug.Log("✅ Simulación iniciada");
    }
    public void CerrarInteraccion()
    {
        if (!simulacionActiva) return;
        simulacionActiva = false;

        if (coGenerar != null) StopCoroutine(coGenerar);
        if (coDespachar != null) StopCoroutine(coDespachar);

        MostrarResultadosFinales();
    }
    void CargarCatalogoDesdeTxt()
    {
        string ruta = Path.Combine(Application.streamingAssetsPath, "productos.txt");
        if (!File.Exists(ruta))
        {
            Debug.LogError("Archivo productos.txt no encontrado en: " + ruta);
            return;
        }

        string[] lineas = File.ReadAllLines(ruta);
        foreach (string linea in lineas)
        {
            var datos = linea.Split('|');
            if (datos.Length == 6)
            {
                Producto p = new Producto(
                    datos[0], datos[1], datos[2],
                    float.Parse(datos[3]),
                    float.Parse(datos[4]),
                    int.Parse(datos[5])
                );
                catalogo.Add(p);
            }
        }

        Debug.Log("cargado: " + catalogo.Count + " productos.");
    }

    IEnumerator GenerarProductos()
    {
        while (simulacionActiva)
        {
            int cantidad = Random.Range(1, 4); 
            for (int i = 0; i < cantidad; i++)
            {
                if (catalogo.Count == 0) break;
                var baseProd = catalogo[Random.Range(0, catalogo.Count)];
                Producto p = new Producto(
                    baseProd.Id, baseProd.Nombre, baseProd.Tipo,
                    baseProd.Peso, baseProd.Precio, baseProd.Tiempo
                );

                pilaProductos.Apilar(p);
                totalGenerados++;
            }

            ActualizarIndicadores();
            yield return new WaitForSeconds(1f); 
        }
    }

    IEnumerator DespacharProductos()
    {
        while (simulacionActiva)
        {
            if (pilaProductos.tamanoPila() > 0)
            {
                var prod = pilaProductos.Desapilar();
                if (prod != null)
                {
                    totalDespachados++;
                    conteoDespachos++;
                    tiempoTotalDespacho += prod.Tiempo;

                    if (!despachadosPorTipo.ContainsKey(prod.Tipo))
                        despachadosPorTipo[prod.Tipo] = 0;
                    despachadosPorTipo[prod.Tipo]++;

                    ActualizarIndicadores();
                    yield return new WaitForSeconds(prod.Tiempo);
                }
            }
            else
            {
                yield return null;
            }
        }
    }
    void ActualizarIndicadores()
    {
        if (txtIndicadores != null)
        {
            var tope = pilaProductos.VerTope();
            string textoTope = tope != null ? $"{tope.Nombre} ({tope.Tipo})" : "-";

            txtIndicadores.text =
                "📊 INDICADORES EN VIVO\n\n" +
                $"Pila: {pilaProductos.tamanoPila()}\n" +
                $"Tope: {textoTope}\n" +
                $"Generados: {totalGenerados}\n" +
                $"Despachados: {totalDespachados}";
        }
    }

    void MostrarResultadosFinales()
    {
        float promedio = conteoDespachos > 0 ? tiempoTotalDespacho / conteoDespachos : 0f;
        string tipoMas = despachadosPorTipo.OrderByDescending(kv => kv.Value).FirstOrDefault().Key ?? "-";

        exportJSON.total_generado = totalGenerados;
        exportJSON.total_despachados = totalDespachados;
        exportJSON.total_pila = pilaProductos.tamanoPila();
        exportJSON.tiempo_promedio_despacho = (int)promedio;
        exportJSON.despachadosPorTipo = despachadosPorTipo;
        exportJSON.tipoMasDespachado = tipoMas;

        Exportador.Guardar(exportJSON);

        if (panelResultados != null && txtPanelResultados != null)
        {
            panelResultados.SetActive(true);
            txtPanelResultados.text =
                "RESULTADOS\n\n" +
                $"Generados: {exportJSON.total_generado}\n" +
                $"Despachados: {exportJSON.total_despachados}\n" +
                $"En pila: {exportJSON.total_pila}\n" +
                $"Promedio Despacho: {exportJSON.tiempo_promedio_despacho}\n" +
                $"Tipo más despachado: {exportJSON.tipoMasDespachado}";
        }

        Debug.Log("Resultados finales");
    }
}




