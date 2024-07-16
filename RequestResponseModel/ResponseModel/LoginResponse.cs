namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class LoginResponse
    {
        public long userId { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
