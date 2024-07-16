using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetAllRolesResponse
    {
        public long roleId { get; set; }
        public string roleName { get; set; }
        public string description { get; set; }

        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }
    }
}
