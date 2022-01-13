using System.Threading.Tasks;

namespace SNow.Core.SetImport
{
    public interface IImportSet
    {
        Task<ImportSetResponse> Import(object data);
    }
}