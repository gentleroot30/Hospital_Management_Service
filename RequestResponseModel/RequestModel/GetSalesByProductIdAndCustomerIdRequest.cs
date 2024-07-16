namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class GetSalesByProductIdAndCustomerIdRequest
    {
        public long? ProductId { get; set;}

        public long? CustomerId { get; set;}
    }
}
