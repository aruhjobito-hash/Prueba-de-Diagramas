using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Sistema
{
    public class APILEON
    {
        //private readonly string _urlBase = "https://172.15.2.15:8087"; /*// Host de Produccion*/
        private readonly string _urlBase = "http://172.17.2.53:8084"; /*// Host de Desarrollo*/
        //private readonly string _urlBase = "https://localhost:7153"; /*// Host Local*/
        //private readonly string _urlBase = "https://localhost:7153";
        private readonly HttpClient _client;

        // Creamos el constructor HttpClient a utilizar
        public APILEON()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        // Establecemos un metodo Task(tarea) async (espera la respuesta independientemente de lo ejecutado en su propio Thread)
        //
        private async Task<T> ExecuteRequest<T>(string url, HttpMethod method, object? body = null)
        {
            HttpResponseMessage? response ;
            StringContent? content = null;

            if (body != null)
            {
                string jsonContent = JsonConvert.SerializeObject(body);
                //jsonContent = jsonContent.Replace("/", ""); // eliminamos las posibles direcciones generadas al pasar el objeto
                content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            switch (method.Method)
            {
                case "POST":
                    response = await _client.PostAsync(url, content);
                    break;
                case "GET":
                    response = await _client.GetAsync(url);
                    break;
                case "PUT":
                    response = await _client.PutAsync(url, content);
                    break;
                case "DELETE":
                    response = await _client.DeleteAsync(url);
                    break;
                default:
                    throw new InvalidOperationException("Invalid HTTP method.");
            }

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseData);
            }
            else
            {
                // Mensaje de Error
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API call failed. Status: {response.StatusCode}. Response: {error}");
            }
        }

        // POST función para obtener mediante post (single Model)
        public async Task<T> APILEONPOST<T>(string urldir, object body)
        {
            string url = _urlBase + urldir;
            return await ExecuteRequest<T>(url, HttpMethod.Post, body);
        }

        // POST función para obtener mediante post (List Model)
        public async Task<T> APILEONPOSTList<T>(string urldir, List<object> body)
        {
            string url = _urlBase + urldir;
            return await ExecuteRequest<T>(url, HttpMethod.Post, body);
        }

        // GET funcion para obtener mediante get (Single Model)
        public async Task<T> APILEONGET<T>(string urldir)
        {
            string url = _urlBase + urldir;
            return await ExecuteRequest<T>(url, HttpMethod.Get);
        }

        // PUT funcion para obtener mediante put (Single Model)
        public async Task<T> APILEONPUT<T>(string urldir, object body)
        {
            string url = _urlBase + urldir;
            return await ExecuteRequest<T>(url, HttpMethod.Put, body);
        }
        // PUT funcion para obtener mediante put (List Model)
        public async Task<T> APILEONPUTList<T>(string urldir, List<object> body)
        {
            string url = _urlBase + urldir;
            return await ExecuteRequest<T>(url, HttpMethod.Delete, body);
        }

        // DELETE funcion para obtener mediante Delete (Single Model)
        public async Task<T> APILEONDEL<T>(string urldir)
        {
            string url = _urlBase + urldir;
            return await ExecuteRequest<T>(url, HttpMethod.Delete);
        }
        // DELETE funcion para obtener mediante Delete (List Model)
        public async Task<T> APILEONDELList<T>(string urldir, List<object> body)
        {
            string url = _urlBase + urldir;
            return await ExecuteRequest<T>(url, HttpMethod.Delete, body);
        }

    }
}
