using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ClientSystem.Pages.Views
{
    public class CreateModel : PageModel
    {
        public Client client = new Client();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            
        }
        public void OnPost()
        {
            client.fullName = Request.Form["name"];
            client.email = Request.Form["email"];
            client.phoneNumber = Request.Form["phone"];
            client.address = Request.Form["address"];
            if(string.IsNullOrEmpty(client.fullName) || string.IsNullOrEmpty(client.email) || string.IsNullOrEmpty(client.phoneNumber) || string.IsNullOrEmpty(client.address))
            {
                errorMessage = "All fields must be filled";
                return;
            }

			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ClientSystemdb;Integrated Security=True;TrustServerCertificate=True;";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "INSERT INTO clients(name, email, phone, address) VALUES(@name,@email,@phone,@address)";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", client.fullName);
						command.Parameters.AddWithValue("@email", client.email);
						command.Parameters.AddWithValue("@phone", client.phoneNumber);
						command.Parameters.AddWithValue("@address", client.address);

						command.ExecuteNonQuery();

					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			client.fullName = "";
			client.email = "";
			client.phoneNumber = "";
			client.address = "";
            successMessage = "Client added succesfully";

			Response.Redirect("/Views/Index");
		}
    }
 }

