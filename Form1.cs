using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProduccionesGramatica
{
    public partial class Form1 : Form
    {
        List<string> cabezas = new List<string>();
        List<string> cabezasOtrasProducciones = new List<string>();
        List<string> cuerpos = new List<string>();
        List<Producciones> MostradoDatos = new List<Producciones>();//lista de objetos que el usuario va a percibir
        List<string> cuerpoPrincipal = new List<string>();
        //DataTable producciones = new DataTable();
        string letrasIniciales = ""; //toma el cuerpo de la primera produccion.
        string cadenaFinal = "";//almacena lo que es la derivacion final de la gramatica ingresada.
        List<int> size = new List<int>();
        int cantCabezas;
        List<string> derivado = new List<string>();//son las derivaciones de cada cabeza
        public Form1()
        {
            InitializeComponent();
          
        }
        public  void refrescarGridView()
        {
            this.Prods.DataSource = "";
        }
        public void refrescarTextbox()
        {
            this.txtCabezas.Clear();
            this.txtCuerpos.Clear();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            Producciones objeto = new Producciones();
            objeto.Cabeza = this.txtCabezas.Text;
            objeto.Cuerpo = this.txtCuerpos.Text;
            MostradoDatos.Add(objeto);
            this.Prods.DataSource = null;
            this.Prods.DataSource = MostradoDatos;
            //for (int i = 0; i < cabezas.Count; i++)
            //{
            //    producciones.Rows.Add(cabezas[i], cuerpos[i]);
            //}
            cabezas.Add(this.txtCabezas.Text);
            cuerpos.Add(this.txtCuerpos.Text);
            refrescarTextbox();
        }

        private void btnDerivar_Click(object sender, EventArgs e)
        {
             letrasIniciales = cuerpos[0].ToString();//almacena el primer cuerpo que en teoria es el principal de la gramatica

            for (int a = 0; a < letrasIniciales.Length; a++)
            {
                cuerpoPrincipal.Add(letrasIniciales[a].ToString());//se desgloza letra por letra del cuerpo princpal para despues hacer la derivacion en base a esas letras
            }
            cantCabezas = cabezas.Count - 1;//toas las cabezas menos la primera que es la principal.
            //guardamos las cabezas y los cuerpos a operar:
            for (int z = 1; z < cabezas.Count; z++)
            {
                cabezasOtrasProducciones.Add(cabezas[z]);
                derivado.Add(cuerpos[z]);
            }
            //se ordena lo que son las cabezas para luego compararlas y asi ir sustituyendo las variables.
            
            for (int b = 0; b < cantCabezas; b++)
            {
                size.Add(derivado[b].Count());
            }
            int aux1;
            string aux2, aux3;
            for (int m = 1; m < size.Count; m++)
            {
                aux1 = size[m];
                aux2 = cabezasOtrasProducciones[m];
                aux3 = derivado[m];
                for (int z = m - 1; z >= 0 && size[z] < aux1; z--)
                {
                    size[z + 1] = size[z];
                    cabezasOtrasProducciones[z + 1] = cabezasOtrasProducciones[z];
                    derivado[z + 1] = derivado[z];

                    size[z] = aux1;
                    cabezasOtrasProducciones[z] = aux2;
                    derivado[z] = aux3;
                }
            }
            //reemplazo de las derivaciones
            for (int b = 0; b < cantCabezas; b++)
            {
                for (int t = 0; t < cuerpoPrincipal.Count; t++)
                {
                    cuerpoPrincipal[t] = cuerpoPrincipal[t].Replace(cabezasOtrasProducciones[b], derivado[b]);
                }
            }
            //almacena el derivado de la gramatica 
            for (int y = 0; y < cuerpoPrincipal.Count; y++)
            {
                cadenaFinal = cadenaFinal + cuerpoPrincipal[y];
            }
            this.txtResultado.Text = cadenaFinal.ToString();
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            //this.txtCabezas.Text = "";
            //this.txtCuerpos.Text = "";
            //this.txtResultado.Text = "";
            //this.Prods.Rows.Clear();
            this.Close();
        }

        //private void btnLimpiar_Click(object sender, EventArgs e)
        //{
        //    refrescarGridView();
        //    cabezas.Clear();
        //    cuerpos.Clear();
        //    cuerpoPrincipal.Clear();
        //    derivado.Clear();
        //    this.txtResultado.Text = "";
        //    MostradoDatos.Clear();
        //    this.Prods.DataSource = null;
        //    size.Clear();
        //    letrasIniciales = "";
        //    cadenaFinal = "";
        //}
    }
}
