using BlazorInputFile;
using System.Threading.Tasks;

namespace BlazorRPG.Client.Services
{
    public interface IFileUpload
    {
        Task Upload(IFileListEntry file);
    }
}
