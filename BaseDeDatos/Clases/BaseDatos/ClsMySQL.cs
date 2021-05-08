using MySqlConnector;

namespace Parcial2.clases
{
    class ClsMySQL
    {
        private string servidor = "localhost"; //Nombre o ip del servidor de MySQL
        private string bd = "prograAlumnos"; //Nombre de la base de datos
        private string usuario = "root"; //Usuario de acceso a MySQL
        private string password = ""; //Contraseña de usuario de acceso a MySQL
        private string datos = null; //Variable para almacenar el resultado
        MySqlConnection conexionBD;
        MySqlDataReader reader;
        public ClsMySQL()
        {
            //Crearemos la cadena de conexión concatenando las variables
            string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password + "";
            //Instancia para conexión a MySQL, recibe la cadena de conexión
            conexionBD = new MySqlConnection(cadenaConexion);
        }
        private string ConsultaSimple(string consulta, int columna = 0)
        {
            reader = null; //Variable para leer el resultado de la consulta
            //Agregamos try-catch para capturar posibles errores de conexión o sintaxis.
            try
            {
                MySqlCommand comando = new MySqlCommand(consulta); //Declaración SQL para ejecutar contra una base de datos MySQL
                comando.Connection = conexionBD; //Establece la MySqlConnection utilizada por esta instancia de MySqlCommand
                conexionBD.Open(); //Abre la conexión
                reader = comando.ExecuteReader(); //Ejecuta la consulta y crea un MySqlDataReader

                while (reader.Read()) //Avanza MySqlDataReader al siguiente registro
                {
                    datos += reader.GetString(columna) + "\n"; //Almacena cada registro con un salto de linea
                }
                return datos; //Si existen datos retorna los datos
            }
            catch (MySqlException ex)
            {
                return ex.Message; //Si existe un error aquí retorna el error
            }
            finally
            {
                conexionBD.Close(); //Cierra la conexión a MySQL
            }
        }

        public void ProbarConexion()
        {
            string consulta = "SHOW DATABASES"; //Consulta a MySQL (Muestra las bases de datos que tiene el servidor)

            MessageBox.Show(ConsultaSimple(consulta));
        }
        public string Insertar(int id, string nombre, int parcial1, int parcial2, int parcial3, string seccion)
        {
            string consulta = $"insert into alumnos (id, nombre, parcial1, parcial2, parcial3, seccion) VALUES ('{id}','{nombre}', '{parcial1}', '{parcial2}', '{parcial3}', '{seccion}');";
            return ConsultaSimple(consulta);
        }
        public string Consultar()
        {
            string consulta = "select * from alumnos";
            return ConsultaSimple(consulta);
        }

        /***
         * ESTO ES CODIGO MAS AVANZADO LO QUE HACE ES QUE DEVUELVE UN ARREGLO DE STRING CON LOS CAMPOS DE LA BASE DE DATOS
         */
        private string[] ConsultaCompuesta(string consulta, int[] columnas)
        {
            string[] respuesta = new string[columnas.Length];
            reader = null; //Variable para leer el resultado de la consulta
            //Agregamos try-catch para capturar posibles errores de conexión o sintaxis.
            try
            {
                MySqlCommand comando = new MySqlCommand(consulta); //Declaración SQL para ejecutar contra una base de datos MySQL
                comando.Connection = conexionBD; //Establece la MySqlConnection utilizada por esta instancia de MySqlCommand
                conexionBD.Open(); //Abre la conexión
                reader = comando.ExecuteReader(); //Ejecuta la consulta y crea un MySqlDataReader

                while (reader.Read()) //Avanza MySqlDataReader al siguiente registro
                {
                    for (int i = 0; i < columnas.Length; i++)
                    {
                        respuesta[i] = reader.GetString(columnas[i]);
                    }
                    //datos +=  + "\n"; //Almacena cada registro con un salto de linea
                }
                return respuesta; //Si existen datos retorna los datos
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return respuesta; //Si existe un error aquí retorna el error
            }
            finally
            {
                conexionBD.Close(); //Cierra la conexión a MySQL
            }
        }
        public string[] ConsultaPorId(int id, int[] columna)
        {
            string consulta = $"select * from alumnos where id={id}";
            return ConsultaCompuesta(consulta, columna);
        }
    }
}