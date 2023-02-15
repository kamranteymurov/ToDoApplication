using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using ToDoApplication.Models;

namespace ToDoApp.Pages
{
    public class ReadTasksModel : PageModel
    {

        private List<ToDoTask> toDoTasks = new List<ToDoTask>();
        public bool PostSuccess { get; set; }
        public string? responseBody { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            string apiUrl = "https://localhost:7083";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var response = await client.GetAsync("/ToDo");
                if (response.IsSuccessStatusCode)
                {
                    toDoTasks = await response.Content.ReadAsAsync<List<ToDoTask>>();
                }
                else
                {
                    toDoTasks = new List<ToDoTask>();
                }
            }
            if (toDoTasks != null)
            {
                ViewData["toDoTasks"] = toDoTasks;
                return Page();
            }
            else
            {
                return NotFound();
            }
        }
        // add task
        public async Task<IActionResult> OnPostAddAsync(ToDoTask task)
        {
            // Send the Post request to the API
            string apiUrl = "https://localhost:7083";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var response = await client.PostAsJsonAsync("/ToDo", task);
                PostSuccess = response.IsSuccessStatusCode;

                var responseText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseText);
                if (PostSuccess)
                {
                    responseBody = result.message.value;
                }
                else responseBody = result.value;
                
                var refreshResponse = await client.GetAsync("/ToDo");
                if (refreshResponse.IsSuccessStatusCode)
                {
                    toDoTasks = await refreshResponse.Content.ReadAsAsync<List<ToDoTask>>();
                    if (toDoTasks != null)
                    {
                        ViewData["toDoTasks"] = toDoTasks;
                    }
                    return Page();
                }
            }
            return Page();
        }
        // save task
        public async Task<IActionResult> OnPostAsync(int id, string taskName, PriorityTypes priority, StatusTypes status)
        {
            ToDoTask updatedTask = new ToDoTask
            {
                Id = id,
                Name = taskName,
                Priority = priority,
                Status = status,
            };
            // Serialize the object to be sent as a JSON string
            var content = new StringContent(JsonConvert.SerializeObject(updatedTask), Encoding.UTF8, "application/json");
            // Send the PUT request to the API
            string apiUrl = "https://localhost:7083";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var response = await client.PutAsync("/ToDo/" + id, content);
                PostSuccess = response.IsSuccessStatusCode;
                var responseText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseText);
                if (PostSuccess)
                {
                    responseBody = result.message.value;
                }
                else responseBody = result.value;
                
                var refreshTasks = await client.GetAsync("/ToDo");
                if (refreshTasks.IsSuccessStatusCode)
                {
                    toDoTasks = await refreshTasks.Content.ReadAsAsync<List<ToDoTask>>();
                    if (toDoTasks != null)
                    {
                        ViewData["toDoTasks"] = toDoTasks;
                    }
                    return Page();
                }
            }
            return Page();
        }
        //delete task
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Send the DELETE request to the API
            string apiUrl = "https://localhost:7083";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var response = await client.DeleteAsync("/ToDo/" + id);
                PostSuccess = response.IsSuccessStatusCode;
                var responseText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseText);

                responseBody = result.message.value;

                var refreshTasks = await client.GetAsync("/ToDo");
                if (refreshTasks.IsSuccessStatusCode)
                {
                    toDoTasks = await refreshTasks.Content.ReadAsAsync<List<ToDoTask>>();
                    if (toDoTasks != null)
                    {
                        ViewData["toDoTasks"] = toDoTasks;
                    }
                    return Page();
                }
            }
            return Page();
        }
    }
}
