using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ClientSystem.Pages.Views
{
    public class IndexModel : PageModel
    {
        public List<Client> Clients = new List<Client>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ClientSystemdb;Integrated Security=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Client cl = new Client();
                                cl.clientID = reader.GetInt32(0);
                                Console.WriteLine(cl);
                                cl.fullName = reader.GetString(1);
                                cl.email = reader.GetString(2);
                                cl.phoneNumber = reader.GetString(3);
                                cl.address = reader.GetString(4);
                                cl.created_at = reader.GetDateTime(5);
                                Clients.Add(cl);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    public class Client
    {
        public int clientID { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public DateTime created_at { get; internal set; }
    }
}
