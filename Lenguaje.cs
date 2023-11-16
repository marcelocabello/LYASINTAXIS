using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
    Requerimiento 1: Printf -> printf(cadena(, Identificador)?);
    Requerimiento 2: Scanf -> scanf(cadena,&Identificador);
    Requerimiento 3: Agregar a la Asignacion +=, -=, *=. /=, %=
                     Ejemplo:
                     Identificador IncrementoTermino Expresion;
                     Identificador IncrementoFactor Expresion;
    Requerimiento 4: Agregar el else optativo al if
    Requerimiento 5: Indicar el número de linea de los errores
*/

namespace LYASINTAXIS
{
    public class Lenguaje : Sintaxis
    {
        public Lenguaje()
        {
        }
        public Lenguaje(string nombre) : base(nombre)
        {
        }
        //Programa  -> Librerias? Variables? Main
        public void Programa()
        {
            if (getContenido() == "#")
            {
                Librerias();
            }
            if (getClasificacion() == Tipos.tipoDatos)
            {
                Variables();
            }
            Main();
        }
        //Librerias -> #include<identificador(.h)?> Librerias?
        private void Librerias()
        {
            match("#");
            match("include");
            match("<");
            match(Tipos.Identificador);
            if (getContenido() == ".")
            {
                match(".");
                match("h");
            }
            match(">");
            if (getContenido() == "#")
            {
                Librerias();
            }
        }
        //Variables -> tipoDato listaIdentificadores; Variables?
        private void Variables()
        {
            match(Tipos.tipoDatos);
            listaIdentificadores();
            match(";");
            if (getClasificacion() == Tipos.tipoDatos)
            {
                Variables();
            }
        }
        //listaIdentificadores -> Identificador (,listaIdentificadores)?
        private void listaIdentificadores()
        {
            match(Tipos.Identificador);
            if (getContenido() == ",")
            {
                match(",");
                listaIdentificadores();
            }
        }
        //bloqueInstrucciones -> { listaIntrucciones? }
        private void bloqueInstrucciones()
        {
            match("{");
            if (getContenido() != "}")
            {
                ListaInstrucciones();
            }
            match("}");
        }
        //ListaInstrucciones -> Instruccion ListaInstrucciones?
        private void ListaInstrucciones()
        {
            Instruccion();
            if (getContenido() != "}")
            {
                ListaInstrucciones();
            }
        }
        //Instruccion -> Printf | Scanf | If | While | do while | For | Asignacion
        private void Instruccion()
        {
            if (getContenido() == "printf")
            {
                Printf();
            }
            else if (getContenido() == "scanf")
            {
                Scanf();
            }
            else if (getContenido() == "if")
            {
                If();
            }
            else if (getContenido() == "while")
            {
                While();
            }
            else if (getContenido() == "do")
            {
                Do();
            }
            else if (getContenido() == "for")
            {
                For();
            }
            else
            {
                Asignacion();
            }
        }
        //Printf -> printf(cadena(, Identificador)?); REQUISITO 1
        private void Printf()
        {
            match("printf");
            match("(");
            match(Tipos.Cadena);

            if (getContenido() == ",")
            {
                match(",");
                match(Tipos.Identificador);
            }

            match(")");
            match(";");

        }
        // Scanf -> scanf(cadena,&Identificador); REQUISITO 2
        private void Scanf()
        {
            match("scanf");
            match("(");
            match(Tipos.Cadena);
            match(",");
            match("&");
            match(Tipos.Identificador);
            match(")");
            match(";");
        }

        //Asignacion -> Identificador (++ | --) | (= Expresion);
        private void Asignacion()
        {
            match(Tipos.Identificador);

            if (getClasificacion() == Tipos.OperadorTermino || getClasificacion() == Tipos.IncrementoTermino)
            {
                Incremento();  
            }
            else if (getClasificacion() == Tipos.IncrementoFactor)
            {
                Incremento();  
            }
            else
            {
                match(Tipos.Asignacion);
                Expresion();
            }

            match(";");
        }

        //If -> if (Condicion) instruccion | bloqueInstrucciones 
        //      (else instruccion | bloqueInstrucciones)? REQUISITO REQUERIMIENTO 4
        private void If()
        {
            match("if");
            match("(");
            Condicion();
            match(")");

            if (getContenido() == "{")
            {
                bloqueInstrucciones();
            }
            else
            {
                Instruccion();
            }
            if (getContenido() == "else")
            {
                match("else");

                if (getContenido() == "{")
                {
                    bloqueInstrucciones();
                }
                else
                {
                    Instruccion();
                }
            }
        }
        //Condicion -> Expresion operadoRelacional Expresion
        private void Condicion()
        {
            Expresion();
            match(Tipos.OperadorRelacional);
            Expresion();
        }
        //While -> while(Condicion) bloqueInstrucciones | Instruccion
        private void While()
        {

        }
        //Do -> do bloqueInstrucciones | Intruccion while(Condicion);
        private void Do()
        {

        }
        //For -> for(Asignacion Condicion; Incremento) BloqueInstruccones | Intruccion 
        private void For()
        {

        }
        //Incremento -> Identificador ++ | --
        private void Incremento()
        {
            match(";");
            if (getClasificacion() == Tipos.IncrementoTermino)
            {
                 
            }
            
                
        }
        //Main      -> void main() bloqueInstrucciones
        private void Main()
        {
            match("void");
            match("main");
            match("(");
            match(")");
            bloqueInstrucciones();
        }
        //Expresion -> Termino MasTermino
        private void Expresion()
        {
            Termino();
            MasTermino();
        }
        //MasTermino -> (OperadorTermino Termino)?
        private void MasTermino()
        {
            if (getClasificacion() == Tipos.OperadorTermino)
            {
                match(Tipos.OperadorTermino);
                Termino();
            }
        }
        //Termino -> Factor PorFactor
        private void Termino()
        {
            Factor();
            PorFactor();
        }
        //PorFactor -> (OperadorFactor Factor)?
        private void PorFactor()
        {
            if (getClasificacion() == Tipos.OperadorFactor)
            {
                match(Tipos.OperadorFactor);
                Factor();
            }
        }
        //Factor -> numero | identificador | (Expresion)
        private void Factor()
        {
            if (getClasificacion() == Tipos.Numero)
            {
                match(Tipos.Numero);
            }
            else if (getClasificacion() == Tipos.Identificador)
            {
                match(Tipos.Identificador);
            }
            else
            {
                match("(");
                Expresion();
                match(")");
            }
        }
    }
}