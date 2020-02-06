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
    //Para enviar los datos sin necesidad de de convertirlo
    [Serializable]
    //Para agregar y conseguir el nombre de la tabla que se usara en las consultas
    [Table(Name = "Piezas")]
    class Piezas
    {
        //Agregando los atributos de las las columnas de las tablas, el ID se  le asignara como clave primaria para
        //evitar repeticiones
        [Column(IsPrimaryKey = true)]
        public int Id;

        [Column]
        public string Nombre_Pieza;

        [Column]
        public string Descripcion;

        [Column]
        public int Costo;
    }
}