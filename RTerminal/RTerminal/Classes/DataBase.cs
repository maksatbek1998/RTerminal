using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RTerminal.Classes
{
    class DataBase
    {
        public MySqlConnection connection = new MySqlConnection("datasource=localhost; port=3306;Initial Catalog='terminal';username=root;password=;CharSet=utf8;");
        public delegate void SendData(DataTable data);
        public event SendData del;
        public void SoursData(string s)
        {
            connection.Close();
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = s;
            cmd.ExecuteNonQuery();
            DataTable dta1 = new DataTable();
            MySqlDataAdapter dataadap = new MySqlDataAdapter(cmd);
            dataadap.Fill(dta1);
            del(dta1);
            connection.Close();
        }
        public void Registr(string s)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = s;
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void RegistrTurn(string servicesId, string language)
        {
            // берет последнее число из очереди
            int? lastNumber = int.Parse(DisplayReturnOne("SELECT t.number FROM `terminal`.`turns` AS t ORDER BY t.number DESC LIMIT 1"));
            if (lastNumber == null) lastNumber = 1; //это условии если в базе нет очереди, первый человек за это день
            //работа принтера
            ForPrint print = new ForPrint((lastNumber+1).ToString(), language);
            print.Check_Print();
            // вводит очередь добавляя +1 к последней очереди
            string sqlCommand = "INSERT INTO `terminal`.`turns` (`number`, `services_id`) VALUES(" + lastNumber + "+1, '"+ servicesId + "')";
            connection.Open();             
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlCommand;
            cmd.ExecuteNonQuery();
            connection.Close();
            //MessageBox.Show("время");
            AsynSocket();
        }
        private static void SocketConnect(string str= "{\"message\":\"hello OpenAI, тест Ɵ ɵ ө, ү, ң.\"}") 
        {
            //MessageBox.Show("2");
            int port = 3003; // порт сервера
            string address = "192.168.0.109"; // адрес сервера
            try 
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);

                //byte[] data = Encoding.Unicode.GetBytes(System.Convert.ToBase64String(plainTextBytes));
                socket.Send(plainTextBytes);

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                //MessageBox.Show("работает");
            }
            catch
            {
                //MessageBox.Show("не подкл");
                return;
            }
        }
        static async void AsynSocket()
        {
            //MessageBox.Show("1");
            await Task.Run(() => SocketConnect());                // выполняется асинхронно
            //MessageBox.Show("3");
        }
        private void WebSocketConnect() 
        {
            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost/");
            httpListener.Start();
        }
        public string[] DisplayReturn(string s)
        {
            connection.Open();
            string sql = s;
            string[] value = { "", "", "", "", "", "" };
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                value[0] = reader[0].ToString();
            }
            connection.Close();
            return value;
        }
        public string DisplayReturnOne(string s)
        {
            connection.Open();
            string sql = s;
            string value = "";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                value = reader[0].ToString();
            }
            connection.Close();
            return value;
        }
        

    }
}
