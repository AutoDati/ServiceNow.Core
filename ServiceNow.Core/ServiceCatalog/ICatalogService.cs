using SNow.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SNow.Core.ServiceCatalog
{
    public interface ICatalogService
    {
        Task<JsonElement> Request(object data);

        IServiceNow SN { get; set; }
    }

    public interface ICatalogService<TModel> where TModel : ServiceNowBaseModel
    {
        Task<TModel> Request(object data);
    }
}
