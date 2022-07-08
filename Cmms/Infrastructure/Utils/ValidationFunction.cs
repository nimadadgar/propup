using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Utils
{

    public class HttpResponseBody<T>
    {
        public bool IsValid { get; set; }
        public T Value { get; set; }

        public IEnumerable<ValidationResult> ValidationResults { get; set; }
    }

    public static class ModelValidationExtension
    {
        public static async Task<HttpResponseBody<T>> GetBodyParameterAsync<T>(this HttpRequestData request)
        {
            var body = new HttpResponseBody<T>();
            body.Value = await request.ReadFromJsonAsync<T>();


            var results = new List<ValidationResult>();
            body.IsValid = Validator.TryValidateObject(body.Value, new ValidationContext(body.Value, null, null), results, true);
            body.ValidationResults = results;
            return body;
        }
        public static async Task<HttpResponseBody<T>> GetBodyAsync<T>(this HttpRequestData request)
        {
            var body = new HttpResponseBody<T>();
            body.Value = await request.ReadFromJsonAsync<T>();


            var results = new List<ValidationResult>();
            body.IsValid = Validator.TryValidateObject(body.Value, new ValidationContext(body.Value, null, null), results, true);
            body.ValidationResults = results;
            return body;
        }
    }


}



