using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LYASINTAXIS
{
    public class Sintaxis : Lexico
    {
        public Sintaxis()
        {
            nextToken();
        }

        public Sintaxis(string nombre) : base(nombre)
        {
            nextToken();
        }

        public void match(string espera)
        {
            if (getContenido() == espera)
            {
                nextToken();
            }
            else
            {
                throw new Error($"Sintaxis: Se espera '{espera}' en la línea {linea} de tu codigo", log);
            }
        }

        public void match(Tipos espera)
        {
            if (getClasificacion() == espera)
            {
                nextToken();
            }
            else
            {
                throw new Error($"Sintaxis: Se espera un '{espera}' en la línea {linea} de tu codigo", log);
            }
        }
    }
}
