using Microsoft.AspNetCore.Http;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class ProfilePhotoUploadRequest
    {
        public IFormFile file { get; set; }
        public long UserId { get; set; }


    }
}
