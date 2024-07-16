using HospitalMgmtService.RequestResponseModel.ResponseModel;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdateRoleRequest
    {
        public long roleId { get; set; }
        public string roleName { get; set; }

        public string description { get; set; }

        public List<UpdateFeatureDTO> features { get; set; }
    }
    public class UpdateFeatureDTO
    {
        public long featureId { get; set; }
        public bool add { get; set; }
        public bool edit { get; set; }
        public bool view { get; set; }
        public bool delete { get; set; }
    }
}
