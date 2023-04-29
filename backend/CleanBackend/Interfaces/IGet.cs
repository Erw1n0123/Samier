using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanBackend.Interfaces
{
    public interface IGet
    {
       Task<IActionResult> Get(string uId);
    }
}
