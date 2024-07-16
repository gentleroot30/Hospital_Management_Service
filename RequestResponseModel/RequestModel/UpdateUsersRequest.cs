namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdateUsersRequest
    {


        public long userId { get; set; }
        public long roleId { get; set; }
        public string name { get; set; }
        public string emailId { get; set; }
        public string address { get; set; }
        public string contactNo_1 { get; set; }
        //public string password { get; set; }
        public string contactNo_2 { get; set; }
        public string contactNo_3 { get; set; }
    }
}
