using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.DTOs;

namespace Q2.Pages.Student
{
    public class DetailModel : PageModel
    {
        public void OnGet(int id)
        {
            HttpClient _httpCilent = new HttpClient();
            HttpResponseMessage response = _httpCilent.GetAsync($"http://localhost:5000/api/Student/Detail?id={id}").Result;
            ViewData["student"] = response.Content.ReadFromJsonAsync<StudentDetailDTO>().Result;
        }
    }
}
