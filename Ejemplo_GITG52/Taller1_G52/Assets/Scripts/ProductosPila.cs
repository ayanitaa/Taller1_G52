using UnityEngine;
using System;

namespace packageProductosPila
{
    [Serializable]
    public class Producto
    {
        public string id;
        public string nombre;
        public string tipo;
        public float peso;
        public float precio;
        public int tiempo;

        public Producto()
        {
        }

        public Producto(string id, string nombre, string tipo, float peso, float precio, int tiempo)
        {
            this.id = id;
            this.nombre = nombre;
            this.tipo = tipo;
            this.peso = peso;
            this.precio = precio;
            this.tiempo = tiempo;
        }
    }
}
