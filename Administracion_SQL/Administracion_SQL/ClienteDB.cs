// ******************************************************************************************
// Arroyo Auz Christian Xavier.                                                             *
// 24/06/2016.                                                                              *
// ******************************************************************************************


using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Administracion_SQL
{
    //MarshalByRefObject: Me permite el acceso a objetos atraves de una comunicacion remota
    class ClienteDB : MarshalByRefObject
    {
        //Cadena de conexion a la Base de datos
        private static string conexion = @"CADENA DE CONEXIÓN";
        
        public Piezas obtenerCliente(int id_DelCliente)
        {
            //No se retornara nada
            return null;
        }
    }
}