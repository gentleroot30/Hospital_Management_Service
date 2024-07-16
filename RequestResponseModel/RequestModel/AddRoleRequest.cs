using Dynamitey.DynamicObjects;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class AddRoleRequest
    {
        public string roleName { get; set; }

        public string description { get; set; }

        public List<AddFeaturesDTO> features { get; set; }
    }
    public class AddFeaturesDTO
    {
        public long featureId { get; set; }
        public bool add { get; set; }
        public bool edit { get; set; }
        public bool view { get; set; }
        public bool delete { get; set; }
    }
}

