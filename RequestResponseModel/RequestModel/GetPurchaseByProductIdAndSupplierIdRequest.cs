namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class GetPurchaseByProductIdAndSupplierIdRequest
    {
        public long? SupplierId { get; set; }

        public long? ProductId { get; set; }
    }
}
