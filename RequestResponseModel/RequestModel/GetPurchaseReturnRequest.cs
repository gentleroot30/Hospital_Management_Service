﻿using System;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class GetPurchaseReturnRequest
    {
        public long searchByType { get; set; }
        public string searchByValue { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
    }
}
