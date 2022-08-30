using System;
using System.Collections.Generic;
using System.Text;

namespace arbol_ito
{
    public class Persona
    {
        public string name { get; set; }
        public string dpi { get; set; }
        public DateTime dateBirth { get; set; }
        public string address { get; set; }

        public Persona left = null, right = null;

        public List<Persona> buscar(string nombre, List<Persona> lista, string dpi = "", Persona persona = null, Persona padre = null, int opcion = 0, int lado = 0) {
            if(String.Compare(nombre, this.name) > 0){
                if (string.IsNullOrEmpty(dpi))
                {
                    lista = this.right?.buscar(nombre, lista) ?? lista;
                }
                else
                {
                    lista = this.right?.buscar(nombre, lista, dpi, persona, this, opcion, 2) ?? lista;
                }
            }
            else if(nombre == this.name){
                if (string.IsNullOrEmpty(dpi)){
                    lista.Add(this);
                    lista = this.left?.buscar(nombre, lista, dpi, persona, this, opcion, 1) ?? lista;
                    lista = this.right?.buscar(nombre, lista, dpi, persona, this, opcion, 2) ?? lista;
                } else {
                    if(dpi == this.dpi)
                    {
                        if (opcion == 0)
                        {
                            lista.Add(this);
                        }
                        else if (opcion == 1)
                        {
                            this.dateBirth = persona.dateBirth;
                            this.address = persona.address;
                        }
                        else if (opcion == 2) {
                            if (lado == 1)
                            {
                                if (this.left == null) {
                                    if (this.right == null) {
                                        padre.left = null;
                                    }
                                    else {
                                        padre.left = this.right;
                                    }
                                }
                                else {
                                    if (this.right == null) {
                                        padre.left = this.left;
                                    }
                                    else {
                                        padre.left = this.left;
                                        padre.left.insertar(this.right);
                                    }
                                }
                            }
                            else if (lado == 2)
                            {
                                if (this.left == null)
                                {
                                    if (this.right == null)
                                    {
                                        padre.right = null;
                                    }
                                    else
                                    {
                                        padre.right = this.right;
                                    }
                                }
                                else
                                {
                                    if (this.right == null)
                                    {
                                        padre.right = this.left;
                                    }
                                    else
                                    {
                                        padre.right = this.left;
                                        padre.right.insertar(this.right);
                                    }
                                }
                            }
                        }
                    }else if(String.Compare(dpi, this.dpi) < 0)
                    {
                        lista = this.left?.buscar(nombre, lista, dpi, persona, this, opcion, 1) ?? lista;
                    }else if(String.Compare(dpi, this.dpi) > 0)
                    {
                        lista = this.right?.buscar(nombre, lista, dpi, persona, this, opcion, 2) ?? lista;
                    }
                }
            }else if(String.Compare(nombre, this.name) < 0){
                if (string.IsNullOrEmpty(dpi))
                {
                    lista = this.left?.buscar(nombre, lista) ?? lista;
                }
                else {
                    lista = this.left?.buscar(nombre, lista, dpi, persona, this, opcion, 1) ?? lista;
                }
            }            
            return lista;
        }

        public void insertar(Persona tmpPersona)
        {
            Persona tmp = this;
            if ((String.Compare(tmp.name, tmpPersona.name) > 0) || (tmpPersona.name == tmp.name && String.Compare(tmp.dpi, tmpPersona.dpi) > 0))
            {
                if (tmp.left != null)
                {
                    tmp.left.insertar(tmpPersona);
                }
                else
                {
                    tmp.left = tmpPersona;
                }
            }
            else if ((String.Compare(tmp.name, tmpPersona.name) < 0) || (tmpPersona.name == tmp.name && String.Compare(tmp.dpi, tmpPersona.dpi) < 0))
            {
                if (tmp.right != null)
                {
                    tmp.right.insertar(tmpPersona);
                }
                else
                {
                    tmp.right = tmpPersona;
                }
            }
        }
    }
}
