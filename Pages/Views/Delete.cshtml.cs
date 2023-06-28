using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ClientSystem.Pages.Views
{
    public class DeleteModel : PageModel
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
					String sql = "DELETE FROM clients WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						command.ExecuteNonQuery();

					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}
			Response.Redirect("/Views/Index");
		}
	}
}

