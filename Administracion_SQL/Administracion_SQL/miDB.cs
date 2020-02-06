// ******************************************************************************************
// Arroyo Auz Christian Xavier.                                                             *
// 24/06/2016.                                                                              *
// ******************************************************************************************


using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System;

namespace Administracion_SQL
{
    class miDB : DataContext
    {
        //Metodo a la Tabla Piezas de la base de datos
        public Table<Piezas> Piezas;
        //Base de datos usando Servidor SQL
        //Base de datos creada de forma local
        public miDB() : base(@"CAENA DE CONEXIÓN") { }   
    }
}