using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using ShopBridge.Application.Common.Exceptions;

namespace ShopBridge.WebApi.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static string GetErrorMessage(this ModelStateDictionary model)
        {
            var errorList = model.ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage)).ToArray();

            return JsonConvert.SerializeObject(errorList);
        }
    }
}