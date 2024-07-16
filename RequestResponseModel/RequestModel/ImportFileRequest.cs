using Microsoft.AspNetCore.Http;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class ImportFileRequest
    {
        public IFormFile file { get; set; }

    }
}
