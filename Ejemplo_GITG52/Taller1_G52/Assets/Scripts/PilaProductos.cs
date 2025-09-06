using UnityEngine;
using System.Collections.Generic;
using packageProductosPila;

public class PilaProductos : MonoBehaviour
{
    [Header("Visual")]
    public int maxVisual = 10;                // máximo visible
    public Transform contenedorUI;            // Panel (contenedor)
    public GameObject prefabProductoUI;       // Prefab del producto UI

    private Stack<Producto> pila = new Stack<Producto>();
    private List<GameObject> instanciasUI = new List<GameObject>();

    // ----------------- APILAR -----------------
    public void Apilar(Producto producto)
    {
        // Lógica
        pila.Push(producto);

        // Crear UI
        GameObject go = Instantiate(prefabProductoUI, contenedorUI);
        go.transform.SetAsLastSibling(); // siempre abajo en el panel

        var ui = go.GetComponent<ItemProductoUI>();
        if (ui != null) ui.Configurar(producto);

        instanciasUI.Add(go);

        Debug.Log($"📦 APILAR: {producto.Nombre} | pila lógica={pila.Count} | visibles={instanciasUI.Count}");

        // Si pasa el límite → borrar el que quedó arriba
        if (instanciasUI.Count > maxVisual)
        {
            GameObject eliminado = instanciasUI[0];
            instanciasUI.RemoveAt(0);
            Destroy(eliminado);

            Debug.Log($"❌ Eliminado de arriba (visual). Ahora visibles={instanciasUI.Count}");
        }
    }

    // ----------------- DESAPILAR -----------------
    public Producto Desapilar()
    {
        if (pila.Count == 0)
        {
            Debug.LogWarning("⚠️ La pila está vacía. No se puede desapilar.");
            return null;
        }

        // Lógica
        Producto prod = pila.Pop();

        // Visual → quitar el de abajo
        if (instanciasUI.Count > 0)
        {
            GameObject go = instanciasUI[instanciasUI.Count - 1];
            instanciasUI.RemoveAt(instanciasUI.Count - 1);
            Destroy(go);

            Debug.Log($"⬇️ DESAPILAR: {prod.Nombre} | pila lógica={pila.Count} | visibles={instanciasUI.Count}");
        }

        return prod;
    }

    // ----------------- CONSULTAS -----------------
    public int tamanoPila() => pila.Count;
    public Producto VerTope() => pila.Count > 0 ? pila.Peek() : null;
}