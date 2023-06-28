using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ClientSystem.Pages.Views
{
	public class EditModel : PageModel
	{
		public Client client = new Client();
		public string errorMessage = "";
		public string successMessage = "";
		public void OnGet()
		{
			int id = Convert.ToInt32(Request.Query["id"]);
			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ClientSystemdb;Integrated Security=True;TrustServerCertificate=True;";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM clients WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								client.clientID = reader.GetInt32(0);
								client.fullName = reader.GetString(1);
								client.email = reader.GetString(2);
								client.phoneNumber = reader.GetString(3);
								client.address = reader.GetString(4);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}
		}
		public void OnPost()
		{
			client.clientID = Convert.ToInt32(Request.Form["id"]);
			Console.WriteLine(client.clientID);
			client.fullName = Request.Form["name"];
			client.email = Request.Form["email"];
			client.phoneNumber = Request.Form["phone"];
			client.address = Request.Form["address"];
			if (string.IsNullOrEmpty(client.fullName) || string.IsNullOrEmpty(client.email) || string.IsNullOrEmpty(client.phoneNumber) || string.IsNullOrEmpty(client.address))
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
					String sql = "UPDATE clients SET name=@name, email=@email, phone=@phone, address=@address WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", client.fullName);
						command.Parameters.AddWithValue("@email", client.email);
						command.Parameters.AddWithValue("@phone", client.phoneNumber);
						command.Parameters.AddWithValue("@address", client.address);
						command.Parameters.AddWithValue("@id", client.clientID);

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
			successMessage = "Client modified succesfully";

			Response.Redirect("/Views/Index");
		}
	}
}
