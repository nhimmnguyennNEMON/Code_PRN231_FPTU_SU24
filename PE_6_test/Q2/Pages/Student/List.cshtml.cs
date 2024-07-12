using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.DTOs;

namespace Q2.Pages.Student
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public string id_s { get; set; }
        public void OnGet()
        {
            HttpClient _httpCilent = new HttpClient();
            HttpResponseMessage response = _httpCilent.GetAsync("http://localhost:5000/api/Student/List").Result;
            ViewData["students"] = response.Content.ReadFromJsonAsync<List<StudentDTO>>().Result;

        }

        public void OnGetDelete(int id)
        {
            HttpClient _httpCilent = new HttpClient();
            HttpResponseMessage response = _httpCilent.PostAsync($"http://localhost:5000/api/Student/Delete/{id}", null).Result;
            OnGet();

        }

        public void OnPost(int id_s)
        {
            HttpClient _httpCilent = new HttpClient();
            HttpResponseMessage response = _httpCilent.GetAsync($"http://localhost:5000/api/Student/List").Result;
            ViewData["students"] = response.Content.ReadFromJsonAsync<List<StudentDTO>>().Result.Where(s => s.Id == id_s).ToList();
            ViewData["ids"] = id_s;


        }

        public void OnPostByDate(DateTime dob)
        {
            string dobStr = dob.ToString("MM/dd/yyyy");
            HttpClient _httpCilent = new HttpClient();
            HttpResponseMessage response = _httpCilent.GetAsync($"http://localhost:5000/api/Student/List").Result;
            ViewData["students"] = response.Content.ReadFromJsonAsync<List<StudentDTO>>().Result.Where(s => s.Dob == dobStr).ToList();
            ViewData["ids"] = id_s;


        }


    }
}
