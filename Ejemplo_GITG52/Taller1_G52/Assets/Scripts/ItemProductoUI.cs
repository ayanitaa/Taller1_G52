using UnityEngine;
using UnityEngine.UI;
using TMPro;
using packageProductosPila;

public class ItemProductoUI : MonoBehaviour
{
    public RawImage imagen;
    public TMP_Text txtNombre;
    public TMP_Text txtId;
    public TMP_Text txtTipo;
    public TMP_Text txtPeso;
    public TMP_Text txtPrecio;
    public TMP_Text txtTiempo;

    // Llamado al instanciar
    public void Configurar(Producto p)
    {
        // Cargar la imagen desde Resources
        Texture2D tex = Resources.Load<Texture2D>("Productos/" + p.Nombre);
        if (tex != null) imagen.texture = tex;

        // Asignar textos
        txtNombre.text = p.Nombre;
        txtId.text = "ID: " + p.Id;
        txtTipo.text = "Tipo: " + p.Tipo;
        txtPeso.text = "Peso: " + p.Peso;
        txtPrecio.text = "Precio: " + p.Precio;
        txtTiempo.text = "Tiempo: " + p.Tiempo;
    }
}
