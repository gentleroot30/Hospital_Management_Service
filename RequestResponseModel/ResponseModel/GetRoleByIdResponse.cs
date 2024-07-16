using Dynamitey.DynamicObjects;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetRoleByIdResponse
    {
        public long roleId { get; set; }
        public string roleName { get; set; }
        public string description { get;set; }

        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }
        public List <FeaturesDTO> features { get; set; }
    }
    public class FeaturesDTO
    {

        public long featureId { get; set; }
        public string featureName { get; set; }
        public bool view { get; set; }
        public bool add { get; set; }
        public bool edit { get; set; }
        public bool delete { get; set; }
    }
}
    