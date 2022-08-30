using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace arbol_ito
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var direccionArchivo = "C:\\Users\\digif\\Downloads\\input.csv";
            Console.WriteLine("Ingrese la direccion del archivo.");
            direccionArchivo = Console.ReadLine();
            Persona raiz = null, tmpPersona = null;
            using (StreamReader reader = new StreamReader(direccionArchivo)) {
                var texto = reader.ReadToEnd();
                List<Persona> personas = new List<Persona>();
                if (!string.IsNullOrEmpty(texto)) {
                    string[] linea;
                    foreach (var item in texto.Split('\n')) {
                        linea = item.Split(';');
                        if (linea.Length == 2) {
                            tmpPersona = JsonConvert.DeserializeObject<Persona>(linea[1]);
                            switch (linea[0]) {
                                case "INSERT":
                                    if (raiz == null) {
                                        raiz = tmpPersona;
                                    }
                                    else {
                                        raiz.insertar(tmpPersona);
                                    }
                                    
                                    break;
                                case "PATCH":
                                    if(raiz != null)
                                    {
                                        personas = raiz.buscar(tmpPersona.name, personas, tmpPersona.dpi, tmpPersona, null, 1, 0);
                                    }
                                    break;
                                case "DELETE":
                                    if (raiz != null) {
                                        personas = raiz.buscar(tmpPersona.name, personas, tmpPersona.dpi, tmpPersona, null, 2, 0);
                                    }
                                    break;
                            }
                        }
                    }
                }
                int opcion = 0;
                do
                {
                    Console.WriteLine("Ingrese una opcion");
                    Console.WriteLine("1. Buscar");
                    Console.WriteLine("0. Salir");
                    opcion = Convert.ToInt32(Console.ReadLine());

                    switch (opcion)
                    {
                        case 1:
                            if (raiz != null) {
                                Console.WriteLine("Ingrese el nombre a buscar:");
                                var nombre = Console.ReadLine();
                                List<Persona> res = new List<Persona> ();
                                res = raiz.buscar(nombre, res);
                                foreach (var item in res) {
                                    Console.WriteLine("{ \"name\":\"" + item.name + "\", \"dpi\":\"" + item.dpi + "\", \"dateBirth\":\"" + item.dateBirth + "\", \"address\":\"" + item.address + "\" }");
                                }
                            }    
                            break;
                    }
                } while (opcion != 0);
            }
        }

        
    }
}
