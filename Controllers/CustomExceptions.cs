using System;

namespace HospitalMgmtService.Controllers
{
    public class CustomExceptions
    {

        public class InvalidSearchType : Exception
        { public InvalidSearchType() { } }


        public class FailedToFetchUserData : Exception
        { public FailedToFetchUserData() {} }

        public class UserIDDoesNotExists : Exception
        { public UserIDDoesNotExists() { } }

        public class FullNameCannotBeBlank : Exception
        { public FullNameCannotBeBlank() { } }

        public class InvalidFullName : Exception
        { public InvalidFullName() { } }

        public class EmailIdCannotBeBlank : Exception
        { public EmailIdCannotBeBlank() { } }

        public class InvalidEmailId : Exception
        { public InvalidEmailId() { } }

        public class UserCodeCannotBeBlank : Exception
        { public UserCodeCannotBeBlank() { } }


        public class FailedToSaveUser : Exception
        { public FailedToSaveUser() { } }

        public class FailToUpdateUser : Exception
        { public FailToUpdateUser() { } }

        public class FailedToDeleteUser : Exception
        { public FailedToDeleteUser() { } }

        public class FailedTOFetchRoleData : Exception
        { public FailedTOFetchRoleData() { } }


        public class RoleIdDoesnotExists : Exception
        { public RoleIdDoesnotExists() { } }

        public class RoleNameCannotBeBlank : Exception
        { public RoleNameCannotBeBlank() { } }

        public class FailedToSaveRoles : Exception
        { public FailedToSaveRoles() { } }


        public class FailedToUpdateRole : Exception
        { public FailedToUpdateRole() { } }

        public class FailedToDeleteRole : Exception
        { public FailedToDeleteRole() { } }


        public class FeatureIdDoesNotExists : Exception
        { public FeatureIdDoesNotExists() { } }

        public class RoleAlreadyExists : Exception
        { public RoleAlreadyExists() { } }

        public class FailedToFetchSupplierData : Exception
        { public FailedToFetchSupplierData() { } }


        public class SupplierIdDoesnotExists : Exception
        { public SupplierIdDoesnotExists() { } }

        public class SupplierNameCannotBeBlank : Exception
        { public SupplierNameCannotBeBlank() { } }

        public class SupplierContactNoCannotBeBlank : Exception
        { public SupplierContactNoCannotBeBlank() { } }


        public class FailedToSaveSupplier : Exception
        { public FailedToSaveSupplier() { } }

        public class FailedToUpdateSupplier : Exception
        { public FailedToUpdateSupplier() { } }


        public class FailedToDeleteSupplier : Exception
        { public FailedToDeleteSupplier() { } }

        public class FailedToFetchCustomerCategory : Exception
        { public FailedToFetchCustomerCategory() { } }

        public class CustomerCategoryAlreadyExists : Exception
        { public CustomerCategoryAlreadyExists() { } }

        public class CustomerCategoryIdDoesnotExists : Exception
        { public CustomerCategoryIdDoesnotExists() { } }


        public class CustomerCategoryNameCannotBeBlank : Exception
        { public CustomerCategoryNameCannotBeBlank() { } }

        public class FailedToSaveCustomerCategory : Exception
        { public FailedToSaveCustomerCategory() { } }

        public class FailedToUpdateCustomerCategory : Exception
        { public FailedToUpdateCustomerCategory() { } }

        public class FailedToDeleteCustomerCategory : Exception
        { public FailedToDeleteCustomerCategory() { } }

        public class FailedToFetchCustomerData : Exception
        { public FailedToFetchCustomerData() { } }


        public class CustomerIdDoesnotExists : Exception
        { public CustomerIdDoesnotExists() { } }

        public class CustomerNameCannotBeBlank : Exception
        { public CustomerNameCannotBeBlank() { } }

        public class CustomerContactNoCannotBeBlank : Exception
        { public CustomerContactNoCannotBeBlank() { } }


        public class FailedToSaveCustomer : Exception
        { public FailedToSaveCustomer() { } }

        public class FailedToUpdateCustomer : Exception
        { public FailedToUpdateCustomer() { } }


        public class FailedToDeleteCustomer : Exception
        { public FailedToDeleteCustomer() { } }


        public class FailedToFetchBrandData : Exception
        { public FailedToFetchBrandData() { } }


        public class BrandIdDoesnotExists : Exception
        { public BrandIdDoesnotExists(): base() { } }

        public class BrandNameCannotBeBlank : Exception
        { public BrandNameCannotBeBlank() { } }

        public class BrandAlreadyExists : Exception
        { public BrandAlreadyExists() { } }


        public class FailedToSaveBrand : Exception
        { public FailedToSaveBrand() { } }

        public class FailedToUpdateBrand : Exception
        { public FailedToUpdateBrand() { } }


        public class FailedToDeleteBrand : Exception
        { public FailedToDeleteBrand() { } }



        public class FailedToFetchProductData : Exception
        { public FailedToFetchProductData() { } }
        public class ProductIdDoesnotExists : Exception
        { public ProductIdDoesnotExists() { } }


        public class BrandIdCannotBeBlank : Exception
        { public BrandIdCannotBeBlank() { } }
        public class ProductCategoryIdCannotBeBlank : Exception
        { public ProductCategoryIdCannotBeBlank() { } }

        public class ProductNameCannotBeBlank : Exception
        { public ProductNameCannotBeBlank() { } }

        public class AlertQuanttyCannotBeBlank : Exception
        { public AlertQuanttyCannotBeBlank() { } }

        public class FailedToSaveProduct : Exception
        { public FailedToSaveProduct() { } }

        public class FailedToUpdateProduct : Exception
        { public FailedToUpdateProduct() { } }


        public class FailedToDeleteProduct : Exception
        { public FailedToDeleteProduct() { } }




        public class FailedToFetchProductCategory : Exception
        { public FailedToFetchProductCategory() { } }

        public class ProductCategoryIdDoesnotExists : Exception
        { public ProductCategoryIdDoesnotExists() { } }

        public class ProductCategoryAlreadyExists : Exception
        { public ProductCategoryAlreadyExists() { } }

        public class ProductCategoryNameCannotBeBlank : Exception
        { public ProductCategoryNameCannotBeBlank() { } }

        public class FailedToSaveProductCategory : Exception
        { public FailedToSaveProductCategory() { } }

        public class FailedToUpdateProductCategory : Exception
        { public FailedToUpdateProductCategory() { } }

        public class FailedToDeleteProductCategory : Exception
        { public FailedToDeleteProductCategory() { } }





        public class FailedToFetchExpenseCategory : Exception
        { public FailedToFetchExpenseCategory() { } }

        public class ExpenseCategoryIdDoesnotExists : Exception
        { public ExpenseCategoryIdDoesnotExists() { } }

        public class ExpensecategoryNameCannotBeBlank : Exception
        { public ExpensecategoryNameCannotBeBlank() { } }

        public class FailedToSaveExpensecategory : Exception
        { public FailedToSaveExpensecategory() { } }

        public class FailedToUpdateExpenseCategory : Exception
        { public FailedToUpdateExpenseCategory() { } }

        public class FailedToDeleteExpenseCategory : Exception
        { public FailedToDeleteExpenseCategory() { } }




        public class FailedToFetchExpense : Exception
        { public FailedToFetchExpense() { } }

        public class ExpenseIdDoesnotExists : Exception
        { public ExpenseIdDoesnotExists() { } }

        public class ExpenseNameCannotBeBlank : Exception
        { public ExpenseNameCannotBeBlank() { } }

        public class ExpenseDateCannotBeBlank : Exception
        { public ExpenseDateCannotBeBlank() { } }

        public class ExpenseAmountCannotBeBlank : Exception
        { public ExpenseAmountCannotBeBlank() { } }

        public class ExpenseCategoryCannotBeBlank : Exception
        { public ExpenseCategoryCannotBeBlank() { } }

        public class ExpenseNoteCannotBeBlank : Exception
        { public ExpenseNoteCannotBeBlank() { } }

        public class FailedToSaveExpenseData : Exception
        { public FailedToSaveExpenseData() { } }

        public class FailedToDeleteExpense : Exception
        { public FailedToDeleteExpense() { } }

        public class FailedToUpdateExpense : Exception
        { public FailedToUpdateExpense() { } }



        public class FailedToFetchPOS : Exception
        { public FailedToFetchPOS() { } }

        public class CustomerIdCannotBeBlank : Exception
        { public CustomerIdCannotBeBlank() { } }

        public class POSDateCannotBeBlank : Exception
        { public POSDateCannotBeBlank() { } }

        public class TotalBillCannotBeBlank : Exception
        { public TotalBillCannotBeBlank() { } }

        public class ProductIdCannotBeBlank : Exception
        { public ProductIdCannotBeBlank() { } }

        public class QuantityCannotBeBlank : Exception
        { public QuantityCannotBeBlank() { } }

        public class PaymentMethodCannotBeBlank : Exception
        { public PaymentMethodCannotBeBlank() { } }

        public class AmountCannotBeBlank : Exception
        { public AmountCannotBeBlank() { } }

        public class FailedToFetchExpiredData : Exception
        { public FailedToFetchExpiredData() { } }

        public class NearExpiryDataNotFound : Exception
        { public NearExpiryDataNotFound() { } }


        public class LowStockDataNotFound : Exception
        { public LowStockDataNotFound() { } }

        public class FailedToSavePOS : Exception
        { public FailedToSavePOS() { } }

        public class FailedTOUpdatePOS : Exception
        { public FailedTOUpdatePOS() { } }

        public class FailedToDeletePOS : Exception
        { public FailedToDeletePOS() { } }


        public class SalesIdDoesnotFound : Exception
        { public SalesIdDoesnotFound() { } }


        //SalesReturnErrorCodeAreRemaining
        public class FailedToFetchSalesReturnData : Exception
        { public FailedToFetchSalesReturnData() { } }

        public class SalesReturnIdDoesnotExists : Exception
        { public SalesReturnIdDoesnotExists() { } }









        public class FailedToFetchPurchaseData : Exception
        { public FailedToFetchPurchaseData() { } }

        public class PurchaseIdDoesnotExists : Exception
        { public PurchaseIdDoesnotExists() { } }

        public class SupplierIdCannotBeBlank : Exception
        { public SupplierIdCannotBeBlank() { } }

        public class InvoiceNumbercannotBeBlank : Exception
        { public InvoiceNumbercannotBeBlank() { } }

        public class PurchaseDateCannotBeBlank : Exception
        { public PurchaseDateCannotBeBlank() { } }

        public class BatchNOCannotBeBlank : Exception
        { public BatchNOCannotBeBlank() { } }

        public class ExpiryDateCannotBeBlank : Exception
        { public ExpiryDateCannotBeBlank() { } }

        public class PackOfCannotBeBlank : Exception
        { public PackOfCannotBeBlank() { } }


        public class MrpPerPackCannotBeBlank : Exception
        { public MrpPerPackCannotBeBlank() { } }

        public class RecieversNameCannotBeBlank : Exception
        { public RecieversNameCannotBeBlank() { } }

        public class PpaymentDateCannotBeBlank : Exception
        { public PpaymentDateCannotBeBlank() { } }

        public class DocumentUploadCannotBeBlank : Exception
        { public DocumentUploadCannotBeBlank() { } }


        public class FailedToUpdatePurchase : Exception
        { public FailedToUpdatePurchase() { } }


        public class FailedToDeletePurchase : Exception
        { public FailedToDeletePurchase() { } }


        public class FailedToSavePurchaseData : Exception
        { public FailedToSavePurchaseData() { } }




        public class FailedToFetchPurchaseReturnDAata : Exception
        { public FailedToFetchPurchaseReturnDAata() { } }

        public class PurchaseReturnIdDoesnotExists : Exception
        { public PurchaseReturnIdDoesnotExists() { } }


        public class ReturnRefIdCannotBeBlank : Exception
        { public ReturnRefIdCannotBeBlank() { } }

        public class BatchIdCannotBeBlank : Exception
        { public BatchIdCannotBeBlank() { } }

        public class ReturnQuantityCannoteBlank : Exception
        { public ReturnQuantityCannoteBlank() { } }

        public class FailedToSavePurchaseReturn : Exception
        { public FailedToSavePurchaseReturn() { } }


        public class FailedToUpdatePurchaseReturn : Exception
        { public FailedToUpdatePurchaseReturn() { } }


        public class FailedToDeletePurchaseReturn : Exception
        { public FailedToDeletePurchaseReturn() { } }


        public class FailedToFetchPurchaseOrderData : Exception
        { public FailedToFetchPurchaseOrderData() { } }

        public class PurchaseOrderIdDoesnotExists : Exception
        { public PurchaseOrderIdDoesnotExists() { } }

        public class FailedToSavePurchaseOrder: Exception
        { public FailedToSavePurchaseOrder() { } }


        public class FailedToUpdatePurchaseOrder : Exception
        { public FailedToUpdatePurchaseOrder() { } }


        public class FailedToDeletePurchaseOrder : Exception
        { public FailedToDeletePurchaseOrder() { } }




        public class FailedToFetchQuotationData : Exception
        { public FailedToFetchQuotationData() { } }

        public class QuotationIdDoesnotExists : Exception
        { public QuotationIdDoesnotExists() { } }

        public class QuotationDateCannotBeBlank : Exception
        { public QuotationDateCannotBeBlank() { } }


        public class DocumentPathCannotBeBlank : Exception
        { public DocumentPathCannotBeBlank() { } }


        public class FailedToSaveQuotation : Exception
        { public FailedToSaveQuotation() { } }

        public class FailedToUpdateQuotation : Exception
        { public FailedToUpdateQuotation() { } }
        public class FailedToDeleteQuotation : Exception
        { public FailedToDeleteQuotation() { } }

        public class QuotationAlreadyExists : Exception
        { public QuotationAlreadyExists() { } }
        public class QuotationNoCannotBeBlank : Exception
        { public QuotationNoCannotBeBlank() { } }

        public class QuotationDocumentPathDoesnotExists : Exception
        { public QuotationDocumentPathDoesnotExists() { } }



        public class FailedToFetchQuotationTemplateData : Exception
        { public FailedToFetchQuotationTemplateData() { } }

        public class QuotationTemplateIdDoesnotExists : Exception
        { public QuotationTemplateIdDoesnotExists() { } }

        public class QuotationTemplateNameCannotBeBlank : Exception
        { public QuotationTemplateNameCannotBeBlank() { } }


        public class FailedToSaveQuotationTemplate : Exception
        { public FailedToSaveQuotationTemplate() { } }



        public class FailedToUpdateQuotationTemplate : Exception
        { public FailedToUpdateQuotationTemplate() { } }
        public class FailedToDeleteQuotationTemplate : Exception
        { public FailedToDeleteQuotationTemplate() { } }












    }
}
