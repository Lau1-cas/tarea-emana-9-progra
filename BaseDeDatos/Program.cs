using BaseDeDatos.Clases.BaseDatos;
using System;
using System.Data;

namespace BaseDeDatos
{
    class Program
    {
        static void Main(string[] args)
        {
            ClsConexion cn = new ClsConexion();

            DataTable dt = cn.consultaTablaDirecta("SELECT *  FROM [tb_alumnos]");

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr[0]);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
