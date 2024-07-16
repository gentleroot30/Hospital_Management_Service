using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetAllUsersResponse
    {

        public long userId { get; set; }
        public long roleId { get; set; }
        public string name { get; set; }
        public string emailId { get; set; }
        public string address { get; set; }
        public string roleName { get; set; }
        public string contactNo_1 { get; set; }
        public string contactNo_2 { get; set; }
        public string contactNo_3 { get; set; }

        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }

    }
}
