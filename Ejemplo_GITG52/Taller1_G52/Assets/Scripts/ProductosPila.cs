using UnityEngine;
using System;

namespace packageProductosPila
{
    [Serializable]
    public class Producto
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private string nombre;
        [SerializeField]
        private string tipo;
        [SerializeField]
        private float peso;
        [SerializeField]
        private float precio;
        [SerializeField]
        private int tiempo;

        public Producto() { }

        public Producto(string id, string nombre, string tipo, float peso, float precio, int tiempo)
        {
            this.id = id;
            this.nombre = nombre;
            this.tipo = tipo;
            this.peso = peso;
            this.precio = precio;
            this.tiempo = tiempo;
        }

        public string Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public float Peso { get => peso; set => peso = value; }
        public float Precio { get => precio; set => precio = value; }
        public int Tiempo { get => tiempo; set => tiempo = value; }
    }
}
