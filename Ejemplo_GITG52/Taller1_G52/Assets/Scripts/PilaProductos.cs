using UnityEngine;
using System.Collections.Generic;
using packageProductosPila;

public class PilaProductos : MonoBehaviour
{
    private Stack<Producto> pila;

    public void Apilar (Producto producto)
    {
        pila.Push(producto);
    }

    public Producto Desapilar()
    {
        if (pila.Count > 0)
        {
            return pila.Pop();
        }
        else
        {
            Debug.LogWarning("La pila est� vac�a. No se puede desapilar.");
            return null;
        }
    }

    public int tama�oPila()
    {
        return pila.Count;
    }
    public Producto VerTope()
    {
        if (pila.Count > 0)
        {
            return pila.Peek();
        }
        else
        {
            Debug.LogWarning("La pila est� vac�a. No hay tope.");
            return null;
        }
    }
}
