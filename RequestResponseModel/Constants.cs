using HospitalMgmtService.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HospitalMgmtService.RequestResponseModel
{
    public static class Constants
    {
        public static class SuccessMessages
        {


            public const string USER_SAVED_MESSAGE = "User saved successfully.";
            public const string USER_UPDATED_MESSAGE = "User updated successfully.";
            public const string USER_DELETED_MESSAGE = "User deleted successfully.";


            public const string CUSTOMER_SAVED_MESSAGE = "customer saved successfully.";
            public const string CUSTOMER_UPDATED_MESSAGE = "customer updated successfully.";
            public const string CUSTOMER_DELETED_MESSAGE = "customer deleted successfully";

            public const string BRAND_SAVED_MESSAGE = "Product Brand saved successfully";
            public const string BRAND_UPDATED_MESSAGE = "Product brand updated successfully";
            public const string BRAND_DELETED_MESSAGE = "Product brand deleted successfully";

            public const string CUSTOMER_CATEGORY_SAVED_MESSAGE = "Customer category saved successfully.";
            public const string CUSTOMER_CATEGORY_UPDATED_MESSAGE = " Customer category updated successfully.";
            public const string CUSTOMER_CATEGORY_DELETED_MESSAGE = " Custommer category deleted successfully";

            public const string PRODUCT_SAVED_MESSAGE = "Product saved successfully.";
            public const string PRODUCT_UPDATED_MESSAGE = "Product updated successfully.";
            public const string PRODUCT_DELETED_MESSAGE = "Product deleted successfully";

            public const string PRODUCT_CATEGORY_SAVED_MESSAGE = "Product Category saved successfully.";
            public const string PRODUCT_CATEGORY_UPDATED_MESSAGE = "Product Category updated successfully.";
            public const string PRODUCT_CATEGORY_DELETED_MESSAGE = "Product Category deleted successfully";

            public const string SUPPLIER_SAVED_MESSAGE = "Supplier saved successfully.";
            public const string SUPPLIER_UPDATED_MESSAGE = "Supplier updated successfully.";
            public const string SUPPLIER_DELETED_MESSAGE = "Supplier deleted successfully";

            public const string ROLE_SAVED_MESSAGE = "Role saved successfully.";
            public const string ROLE_UPDATED_MESSAGE = "Role updated successfully.";
            public const string ROLE_DELETED_MESSAGE = "Role deleted successfully.";


            public const string QUOTATION_SAVED_MESSAGE = "Quotation saved successfully.";
            public const string QUOTATION_UPDATED_MESSAGE = "Quotation updated successfully.";
            public const string QUOTATION_DELETED_MESSAGE = "Quotation deleted successfully.";



            public const string QUOTATION_TEMPLATE_SAVED_MESSAGE = "Quotation Template saved successfully.";
            public const string QUOTATION_TEMPLATE_UPDATED_MESSAGE = "Quotation Template updated successfully.";
            public const string QUOTATION_TEMPLATE_DELETED_MESSAGE = "Quotation Template deleted successfully.";

            public const string POS_SAVED_MESSAGE = "POS saved successfully.";
            public const string POS_UPDATED_MESSAGE = "POS updated successfully.";
            public const string POS_DELETED_MESSAGE = "POS deleted successfully.";


            public const string SALESRETURN_SAVED_MESSAGE = "Sales return saved successfully.";
            public const string SALESRETURN_UPDATED_MESSAGE = "Sales return updated successfully.";
            public const string SALESRETURN_DELETED_MESSAGE = "Sales return deleted successfully.";


            public const string EXPENSE_SAVED_MESSAGE = "Expenses saved successfully.";
            public const string EXPENSES_UPDATED_MESSAGE = "Expenses updated successfully.";
            public const string EXPENSES_DELETED_MESSAGE = "Expenses deleted successfully.";

            public const string EXPENSE_CATEGORY_SAVED_MESSAGE = "Expense category saved successfully.";
            public const string EXPENSE_CATEGORY_UPDATED_MESSAGE = "Expense category updated successfully.";
            public const string EXPENSE_CATEGORY_DELETED_MESSAGE = "Expense category deleted successfully.";

            public const string PURCHASE_SAVED_MESSAGE = "Purchase saved successfully.";
            public const string PURCHASE_UPDATED_MESSAGE = "Purchase updated successfully.";
            public const string PURCHASE_DELETED_MESSAGE = "Purchase deleted successfully.";

            public const string PURCHASE_RETURN_SAVED_MESSAGE = "Purchase Return saved successfully";
            public const string PURCHASE_RETURN_DELETED_MESSAGE = "Purchase Return deleted successfully";
            public const string PURCHASE_RETURN_UPDATED_MESSAGE = "Purchase Return updated successfully";


            public const string PURCHASE_ORDER_SAVED_MESSAGE = "Purchase Order saved successfully";
            public const string PURCHASE_ORDER_UPDATED_MESSAGE = "Purchase Order Updated successfully";
            public const string PURCHASE_ORDER_DELETED_MESSAGE = "Purchase Order Deleted successfully";


            public const string PURCHASE_DOCUMENT_SAVED_MESSAGE = "Purchase Document Saved successfully";
            public const string PURCHASE_DOCUMENT_DELETED_MESSAGE = "Purchase Document Deleted successfully";


            public const string QUOTATION_DOCUMENT_SAVED_MESSAGE = "Quotation Document Saved successfully";
            public const string QUOTATION_DOCUMENT_DELETED_MESSAGE = "Quotation Document Deleted successfully";


            public const string PROFILE_PHOTO_SAVED_MESSAGE = "Profile Photo Saved successfully";
            public const string PROFILE_PHOTO_DELETED_MESSAGE = "Profile Photo Deleted successfully";


            public const string HEADER_PHOTO_SAVED_MESSAGE = "Header Photo Saved successfully";
            public const string HEDAER_PHOTO_DELETED_MESSAGE = "Header Photo Deleted successfully";


            public const string FOOTER_PHOTO_SAVED_MESSAGE = "Footer Photo Saved successfully";
            public const string FOOTER_PHOTO_DELETED_MESSAGE = "Footer Photo Deleted successfully";

            public const string IMPORT_STOCK_FILE_SAVED_MESSAGE = "Import Stock File Uploaded Successfully";




        }
        public static class Errors
        {
            public static class Codes
            {
               
                public const int INVALID_SEARCHTYPE_ERROR_CODE = 1031;


                public const int INVALID_LOGIN_EMAIL_ERROR_CODE = 1039;


                //public const int INVALID_USER_ERROR_CODE = 1001;
                public const int INVALID_PASSWORD_ERROR_CODE = 1002;

                // Start:User APIs Error Code

                public const int FAILED_TO_FETCH_USER_DATA_CODE = 1031;
                public const int USER_ID_DOES_NOT_EXISTS = 1034;
                public const int NO_USER_FOUND_CODE = 1035;
                public const int USER_NAME_CAN_NOT_BE_BLANK_CODE = 1041;
                public const int INVALID_USER_NAME_CODE = 1038;
                public const int USER_EMAIL_CAN_NOT_BE_BLANK_CODE = 1039;
                public const int INVALID_USER_EMAIL_ERROR_CODE = 1040;
                public const int INVALID_USER_ERROR_CODE = 1041;
                public const int FAILED_TO_ADD_USER_CODE = 1042;
                public const int FAILED_TO_UPDATE_USER_CODE = 1043;
                public const int FAILED_DELETE_USER_CODE = 1044;

                //End: User APIs Error codes


                //Start: Role APIs Error code 

                public const int FAILED_TO_FETCH_ROLE_DATA_ERROR_CODE = 1061;
                public const int UNAUTHORIZED_ROLE_DATA_ACCESS_ERROR_CODE = 1062;
                public const int ROLE_ID_DOES_NOT_EXISTS_ERROR_CODE = 1063;
                public const int NO_ROLE_FOUND_ERROR_CODE = 1064;
                public const int ROLE_NAME_CAN_NOT_BE_BLANK_ERROR_CODE = 1064;
                public const int FAILED_TO_SAVE_ROLE_ERROR_CODE = 1065;
                public const int FAILED_TO_UPDATE_ROLE_ERROR_CODE = 1066;
                public const int FAILED_TO_DELETE_ROLE_ERROR_CODE = 1067;
                public const int FEATURE_ID_DOES_NOT_EXISTS_ERROR_CODE = 1068;
                public const int ROLE_EXIST_ERROR_ERROR_CODE = 1069;

                // End: Role APIs Error code


                //Start: Customer APIs Error code

                public const int FAILED_TO_FETCH_CUSTOMER_DATA_ERROR_CODE = 1151;
                public const int CUSTOMER_ID_DOES_NOT_EXISTS_ERROR_CODE = 1152;
                public const int FAILED_TO_UPDATE_CUSTOMER_ERROR_CODE = 1156;
                public const int CUSTOMER_NAME_CAN_NOT_BE_BLANK_CODE = 1153;
                public const int CUSTOMER_NOT_DELETED_CODE = 1127;
                public const int FAILED_TO_SAVE_CUSTOMER_ERROR_CODE = 1155;
                public const int CATEGORY_ID_CAN_NOT_BE_BLANK_CODE = 1158;
                public const int CUSTOMER_CODE_CAN_NOT_BE_BLANK_CODE = 1124;
                public const int CUSTOMER_CONATCT_NO_1_CAN_NOT_BE_BLANK_CODE = 1154;
                public const int CUSTOMER_CONTACT_NUMBER_ALREADY_EXISTS_CODE = 1159;


                //End: Customer APIs Error code


                // Start: Customer category APIs Error Code

                public const int FAILED_TO_FETCH_CUSTOMER_CATEGORY_CODE = 1121;
                public const int CUSTOMER_CATEGORY_ID_DOES_NOT_EXISTS_ERROR_CODE = 1122;
                public const int NO_CUSTOMER_CATEGORY_FOUND_ERROR_CODE = 1127;
                public const int CUSTOMER_CATEGORY_NAME_CAN_NOT_BE_BLANK_CODE = 1123;
                public const int FAILED_TO_SAVE_CUSTOMER_CATEGORY_ERROR_CODE = 1124;
                public const int CUSTOMER_CATEGORY_EXIST_ERROR_CODE = 1122;
                public const int FAILED_TO_UPDATE_CUSTOMER_CATEGORY_ERROR_CODE = 1125;
                public const int FAILED_TO_DELETE_CUSTOMER_CATEGORY_ERROR_CODE = 1126;

                //End: Customer category APIs Error Code


                // Start: Brand API Error Code

                public const int FAILED_TO_FETCH_BRAND_DATA_ERROR_CODE = 1241;
                public const int FAILED_TO_UPDATE_BRAND_ERROR_CODE = 1245;
                public const int FAILED_TO_DELETE_BRAND_ERROR_CODE = 1246;
                public const int BRAND_NAME_ALREADY_EXIST_ERROR_CODE = 1247;
                public const int BRAND_NAME_CAN_NOT_BE_BLANK_ERROR_CODE = 1243;
                public const int FAILED_TO_SAVE_BRAND_ERROR_CODE = 1244;
                public const int BRAND_ID_DOES_NOT_EXISTS_ERROR_CODE = 1242;


                // End: Brand API Error code


                //Start: Product Error Code

                public const int FAILED_TO_FETCH_PRODUCT_DATA_ERROR_CODE = 1181;
                public const int PRODUCT_ID_DOES_NOT_EXISTS_ERROR_CODE = 1182;
                public const int FAILED_TO_SAVE_PRODUCT_DATA_ERROR_CODE = 1186;
                public const int PRODUCT_NAME_CAN_NOT_BE_BLANK_ERROR_CODE = 1183;
                public const int BRAND_ID_CAN_NOT_BE_BLANK = 1184;
                public const int PRODUCT_CATEGORY_CAN_NOT_BE_BLANK_ERROR_CODE = 1185;
                public const int ALERT_QUANTITY_CAN_NOT_BE_BLANK_ERROR_CODE = 1189;
                public const int FAILED_TO_UPDATE_PRODUCT_DATA_ERROR_CODE = 1187;
                public const int FAILED_TO_DELETE_PRODUCT_DATA_ERROR_CODE = 1188;

                //End: Product Error Code


                // Start: Product Category Error Code

                public const int FAILED_TO_FETCH_PRODUCT_CATEGORY_DATA_ERROR_CODE = 1211;
                public const int PRODUCT_CATEGORY_ID_DOES_NOT_EXISTS_ERROR_CODE = 1212;
                public const int PRODUCT_CATEGORY_NAME_CAN_NOT_BE_BLANK_ERROR_CODE = 1213;
                public const int PRODUCT_CATEGORY_EXIST_ERROR_CODE = 1214;
                public const int FAILED_TO_SAVE_PRODUCT_CATEGORY_CODE = 1215;
                public const int FAILED_TO_UPDATE_PRODUCT_CATEGORY_CODE = 1216;
                public const int PRODUCT_CATEGORY_NOT_DELETED_CODE = 1217;

                // End: Product Category Error Code


                //Start: Supplier API Error Code

                public const int FAILED_TO_FETCH_SUPPLIER_DATA_ERROR_CODE = 1091;
                public const int SUPPLIER_ID_DOES_NOT_EXISTS_ERROR_CODE = 1092;
                public const int SUPPLIER_FULLNAME_CAN_NOT_BE_BLANKERROR_CODE = 1093;
                public const int SUPPLIER_CONTACT_NO_CAN_NOT_BE_BLANKERROR_CODE = 1094;
                public const int FAILED_TO_UPDATE_SUPPLIER_DATA_ERROR_CODE = 1096;
                public const int FAILED_TO_SAVE_SUPPLIER_DATA_ERROR_CODE = 1095;
                public const int FAILED_TO_FETCH_PURCHASE_DOCUMENTS_ERROR_CODE = 1098;
                public const int SUPPLIER_ADDRESS_CAN_NOT_BE_BLANK_ERROR_CODE = 1099;
                public const int FAILED_TO_DELETE_SUPPLIER_ERROR_CODE = 1097;
                public const int SUPPLIER_ALREADY_EXIST_ERROR_CODE = 1100;



                //End: Supplier API Error Code


                // Start: Quotation API error Code

                public const int FAILED_TO_FETCH_QUOTATION_DATA_ERROR_CODE = 1361;
                public const int QUOTATION_ID_DOES_NOT_EXISTS_ERROR_CODE = 1362;
                public const int QUOTATION_ALREADY_EXISTS_ERROR_CODE = 1369;
                public const int FAILED_TO_SAVE_QUOTATION_DATA_ERROR_CODE = 1366;
                public const int FAILED_TO_UPDATE_QUOTATION_DATA_ERROR_CODE = 1367;
                public const int FAILED_TO_DELETE_QUOTATION_DATA_ERROR_CODE = 1368;
                public const int QUOTATION_DOCUMENT_PATH_DOES_NOT_EXISTS_ERROR_CODE = 1365;
                public const int CUSTOMER_ID_CAN_NOT_BE_BLANK_ERROR_CODE = 1363;
                public const int QUOTATION_NO_CAN_NOT_BE_BLANK_ERROR_CODE = 1333;

                //End: Quotation API error Code


                // Start: Quotation Template API error code.

                public const int FAILED_TO_FETCH_QUOTATION_TEMPLATE_DATA_ERROR_CODE = 1391;
                public const int QUOTATION_TEMPLATE_ID_DOES_NOT_EXISTS_ERROR_CODE = 1392;
                public const int QUOTATION_TEMPLATE_ALREADY_EXISTS_ERROR_CODE = 1395;
                public const int QUOTATION_TEMPLATE_NAME_CAN_NOT_BE_BLANK_ERROR_CODE = 1393;
                public const int PRODUCT_ID_CAN_NOT_BE_BLANK_ERROR_CODE = 1394;
                public const int FAILED_TO_SAVE_QUOTATION_TEMPLATE_DATA_ERROR_CODE = 1396;
                public const int FAILED_TO_UPDATE_QUOTATION_TEMPLATE_DATA_ERROR_CODE = 1397;
                public const int FAILED_TO_DELETE_QUOTATION_TEMPLATE_DATA_ERROR_CODE = 1398;

                // End:Quotation Template API error code

               
                // Start: POS Error Code 

                public const int FAILED_TO_FETCH_POS_DATA_ERROR_CODE = 1421;
                public const int POS_ID_DOES_NOT_EXISTS_ERROR_CODE = 1422;
                public const int POS_ALREADY_EXISTS_ERROR_CODE = 1433;
                public const int FAILED_TO_SAVE_POS_DATA_ERROR_CODE = 1434;
                public const int FAILED_TO_UPDATE_POS_DATA_ERROR_CODE = 1435;
                public const int FAILED_TO_DELETE_POS_DATA_ERROR_CODE = 1436;
                public const int CUSTOMER_ID_CAN_NOT_BE_BlANK_ERROR_CODE = 1423;
                public const int POS_DATE_CAN_NOT_BE_BlANK_ERROR_CODE = 1424;
                public const int TOTAL_BILL_CAN_NOT_BE_BlANK_ERROR_CODE = 1425;
                public const int PAYMENT_METHOD_CAN_NOT_BE_BLANK_ERROR_CODE = 1428;
                public const int AMOUNT_CAN_NOT_BE_BLANK_ERROR_CODE = 1429;
                public const int EXPIRED_PRODUCTS_DATA_NOT_FOUND_ERROR_CODE = 1430;
                public const int NEAR_EXPIRE_PRODUCT_DATA_NOT_FOUND_ERROR_CODE = 1431;
                public const int LOW_STOCK_PRODUCTS_DATA_NOT_FOUND_ERROR_CODE = 1432;
                public const int CAN_NOT_DELETE_SALES_WITH_PAYMENTS_ERROR_CODE = 1437;


                //End:  POS Error Code 

                // Start: Sale return Error code

                public const int FAILED_TO_FETCH_SALESRETRUN_DATA_ERROR_CODE = 1451;
                public const int SALESRETRUN_ID_DOES_NOT_EXISTS_ERROR_CODE = 1452;
                public const int SALESRETRUN_ALREADY_EXISTS_ERROR_CODE = 1463;
                public const int FAILED_TO_SAVE_SALESRETRUN_DATA_ERROR_CODE = 1464;
                public const int FAILED_TO_UPDATE_SALESRETRUN_DATA_ERROR_CODE = 1465;
                public const int FAILED_TO_DELETE_SALESRETRUN_DATA_ERROR_CODE = 1466;
                public const int BATCH_ID_DOES_NOT_EXISTS_ERROR_CODE = 1453;
                public const int CUTOMER_ID_CAN_NOT_BE_BLANK_ERROR_CODE = 1454;
                public const int RETURN_DATE_CAN_NOT_BE_BLANK_ERROR_CODE = 1455;
                public const int QUANTITY_CAN_NOT_BE_BLANK_ERROR_CODE = 1458;
                public const int SALE_RETURN_PAYMENT_METHOD_CAN_NOT_BE_BLANK_ERROR_CODE = 1459;
                public const int SALE_RETURN_AMOUNT_CAN_NOT_BE_BLANK_ERROR_CODE = 1460;
                public const int EXPIRED_PRODUCTS_NOT_FOUND_ERROR_CODE = 1461;
                public const int NEW_EXPIRE_PRODUCTS_DATA_NOT_FOUND_ERROR_CODE = 1462;
                public const int LOW_STOCK_PRODUCTS_NOT_FOUND_ERROR_CODE = 1463;
                public const int TOTAL_BILL_CAN_NOT_BE_BLANK_ERROR_CODE = 1456;

                // End: Sale return Error code

                // Start:Expenses Category Error Codes

                public const int FAILED_TO_FETCH_EXPENSE_CATEGORY_CODE = 1511;
                public const int EXPENSE_CATEGORY_ID_DOES_NOT_EXISTS_ERROR_CODE = 1512;
                public const int EXPENSE_CATEGORY_NAME_CAN_NOT_BE_BLANK_CODE = 1513;
                public const int FAILED_TO_SAVE_EXPENSE_CATEGORY_ERROR_CODE = 1515;
                public const int EXPENSE_CATEGORY_EXIST_ERROR_CODE = 1514;
                public const int FAILED_TO_UPDATE_EXPENSE_CATEGORY_ERROR_CODE = 1516;
                public const int FAILED_TO_DELETE_EXPENSE_CATEGORY_ERROR_CODE = 1517;

                //End: Expenses Category Error Codes


                // Start: Expenses Error Codes

                public const int FAILED_TO_FETCH_EXPENSE_CODE = 1481;
                public const int EXPENSEC_ID_DOES_NOT_EXISTS_ERROR_CODE = 1482;
                public const int EXPENSE_DATE_CAN_NOT_BE_NULL_ERROR_CODE = 1483;
                public const int EXPENSE_AMOUNT_CAN_NOT_BE_NULL_ERROR_CODE = 1484;
                public const int EXPENSE_CATEGORY_ID_CAN_NOT_BE_NULL_ERROR_CODE = 1485;
                public const int EXPENSE_NOTE_CAN_NOT_BE_NULL_ERROR_CODE = 1486;
                public const int EXPENSE_NAME_CAN_NOT_BE_BLANK_CODE = 1490;
                public const int FAILED_TO_SAVE_EXPENSE_ERROR_CODE = 1487;
                public const int EXPENSE_EXIST_ERROR_CODE = 1491;
                public const int FAILED_TO_UPDATE_EXPENSE_ERROR_CODE = 1488;
                public const int FAILED_TO_DELETE_EXPENSE_ERROR_CODE = 1489;

                //End: Expenses Error Codes


                // Start:Purchase API error codes

                public const int FAILED_TO_FETCH_PURCHASE_DATA_ERROR_CODE = 1301;
                public const int PURCHASE_ID_DOES_NOT_EXISTS_ERROR_CODE = 1302;
                public const int SUPPLIER_ID_CAN_NOT_BLANK_ERROR_CODE = 1303;
                public const int PURCHASE_ALREADY_EXISTS_ERROR_CODE = 1325;
                public const int FAILED_TO_SAVE_PURCHASE_DATA_ERROR_CODE = 1320;
                public const int FAILED_TO_UPDATE_PURCHASE_DATA_ERROR_CODE = 1321;
                public const int FAILED_TO_DELETE_PURCHASE_DATA_ERROR_CODE = 1322;
                public const int PURCHASE_DOCUMENT_PATH_DOES_NOT_EXISTS_ERROR_CODE = 1319;
                public const int INVOICE_NUMBER_CAN_NOT_BE_BLANK_ERROR_CODE = 1304;
                public const int PURCHASE_DATE_CAN_NOT_BE_BLANK_ERROR_CODE = 1305;
                public const int BATCH_NO_DOES_NOT_EXISTS_ERROR_CODE = 1323;
                public const int BATCH_NO_CAN_NOT_BE_BLANK_ERROR_CODE = 1308;
                public const int EXPIRY_DATE_CAN_NOT_BE_BLANK_ERROR_CODE = 1309;
                public const int PACK_OF_CAN_NOT_BE_BLANK_ERROR_CODE = 1310;
                public const int MRP_PER_PACK_CAN_NOT_BE_BLANK_ERROR_CODE = 1311;
                public const int PURCHASE_QUANTITY_CAN_NOT_BE_BLANK_ERROR_CODE = 1312;
                public const int PURCHASE_TOTAL_BILL_CAN_NOT_BE_BLANK_ERROR_CODE = 1313;
                public const int PAYMENT_ID_DOES_NOT_EXISTS_ERROR_CODE = 1324;
                public const int RECEIVER_NAME_CAN_NOT_BE_BLANK_ERROR_CODE = 1314;
                public const int RECEIVER_CONTACT_CAN_NOT_BE_BLANK_ERROR_CODE = 1315;
                public const int PAYMENT_DATE_CAN_NOT_BE_BLANK_ERROR_CODE = 1316;
                public const int CAN_NOT_DELETE_PURCHASE_WITH_PAYMENTS_ERROR_CODE = 1324;


               // End: Purchase API error codes


                // Start: Purchase Return API error codes


                public const int FAILED_TO_FETCH_PURCHASE_RETURN_DATA_ERROR_CODE = 1331;
                public const int RETURN_ID_DOES_NOT_EXISTS_ERROR_CODE = 1332;
                public const int SUPPLIER_ID_DOES_NOT_EXIST_ERROR_CODE = 1333;
                public const int PURCHASE_RETURN_ALREADY_EXIST_ERROR_CODE = 1342;
                public const int PRODUCT_ID_FK_DOES_NOT_EXISTS_ERROR_CODE = 1334;
                public const int SUPPLIER_ID_CAN_NOT_BE_BLANK_ERROR_CODE = 1333;
                public const int PRODUCT_ID_FK_CAN_NOT_BE_BLANK_ERROR_CODE = 1343;
                public const int RETURN_REF_NO_CAN_NOT_BE_BLANK_ERROR_CODE = 1335;
                public const int PURCHASE_RETURN_DATE_CAN_NOT_BE_BLANK_ERROR_CODE = 1336;
                public const int BATCH_ID_FK_CAN_NOT_BE_BLANK_ERROR_CODE = 1337;
                public const int RETURN_QUANTITY_CAN_NOT_BE_BLANK_ERROR_CODE = 1338;
                public const int FAILED_TO_SAVE_PURCHASE_RETURN_DATA_ERROR_CODE = 1339;
                public const int FAILED_TO_UPDATE_PURCHASE_RETURN_DATA_ERROR_CODE = 1340;
                public const int FAILED_TO_DELETE_PURCHASE_RETURN_DATA_ERROR_CODE = 1341;
                public const int BATCH_ID_FK_DOES_NOT_EXISTS_ERROR_CODE = 1342;

                // End: Purchase Return API error codes


                //Start: Purchase Order API error codes.

                public const int FAILED_TO_FETCH_PURCHASE_ORDER_DATA_ERROR_CODE = 1271;
                public const int PURCHASE_ORDER_NUMBER_CAN_NOT_BE_BLANK_ERROR_CODE = 1273;
                public const int PURCHASE_ORDER_DATE_CAN_NOT_BE_BLANK_ERROR_CODE = 1275;
                public const int SUPPLIER_ID_OF_PO_CAN_NOT_BE_BLANK_ERROR_CODE = 1276;
                public const int PRODUCT_ID_FK_OF_PO_CAN_NOT_BE_BLANK_ERROR_CODE = 1277;
                public const int ORDER_QUANTITY_OF_PO_CAN_NOT_BE_BLANK_ERROR_CODE = 1278;

                public const int PURCHASE_ORDER_STATUS_CAN_NOT_BE_BLANK_ERROR_CODE = 1279;
                public const int PURCHASE_ORDER_ALREADY_EXIST_ERROR_CODE = 1283;
                public const int FAILED_TO_SAVE_PURCHASE_ORDER_DATA_ERROR_CODE = 1280;
                public const int FAILED_TO_UPDATE_PURCHASE_ORDER_DATA_ERROR_CODE = 1281;
                public const int FAILED_TO_DELETE_PURCHASE_ORDER_DATA_ERROR_CODE = 1282;
                public const int PURCHASE_ORDER_ALREADY_EXISTS_ERROR_CODE = 1283;

                // End: Purchase Order API error codes.


              


                public const int FAILED_TO_FETCH_FEATURE_DATA_ERROR_CODE = 1006;

                public const int FAILED_TO_FETCH_QUOTATION_DOCUMENT_DATA_ERROR_CODE = 1545;

                public const int INVALID_FILE_FORMAT_ERROR_CODE = 1549;

                
            }
            public static class Messages
            {
                public const string INVALID_SEARCH_TYPE_MESSAGE = "Invalid search type passed.";
                public const string INVALID_USER_MESSAGE = "Invalid userid and password.";
                public const string INVALID_EMAIL_MESSAGE = "Invalid email Id or mobile number";

                // Start: Customer API error message

                //public const string INVALID_USER_MESSAGE = "Invalid user";
                public const string INVALID_PASSWORD_MESSAGE = "Invalid password";
                public const string FAILED_TO_FETCH_CUSTOMER_DATA_MESSAGE = "Failed to get customer data";
                public const string NO_CUSTOMER_FOUND_MESSAGE = "No customer found.";
                public const string CUSTOMER_EXIST_MESSAGE = "Customer name already exists";
                public const string CUSTOMER_NAME_CAN_NOT_BE_BLANK_MESSAGE = "Customer name should not be blank";
                public const string FAILED_TO_SAVE_CUSTOMER_MESSAGE = "Failed to update customer";
                public const string FAILED_TO_UPDATE_CUSTOMER_MESSAGE = "Failed to update customer";
                public const string CUSTOMER_NOT_DELETED_MESSAGE = "Failed to delete customer";
                public const string CUSTOMER_ID_DOES_NOT_EXISTS_MESSAGE = "Failed to delete customer";
                public const string CATEGORY_ID_CAN_NOT_BE_BLANK_MESSAGE = "Category Id can not be blank";
                public const string CUSTOMER_CODE_CAN_NOT_BE_BLANK_MESSAGE = "Customer Code can not be blank";
                public const string CUSTOMER_CONTACT_NO_1_CAN_NOT_BE_BLANK_MESSAGE = "Customer Contact no 1 can not be blank";
                public const string CUSTOMER_CONTACT_NUMBER_ALREADY_EXISTS_MESSAGE = "Customer contact number already exists.";

                // End: Customer API error message


                // Start: Product Brand API error message.

                public const string NO_BRAND_FOUND_MESSAGE = "No Brands Found";
                public const string FAILED_TO_FETCH_BRAND_DATA_MESSAGE = "Failed to fetch brand data.";
                public const string BRAND_ALREADY_EXISTS_MESSAGE = "Brand name already exists";
                public const string FAILED_TOUPDATE_BRAND_MESSAGE = "Failed to update Brand.";
                public const string BRANDNAME_CAN_NOT_BE_BLANK_MESSAGE = "Brand name can not be blank";
                public const string FAILED_TO_DELETE_BRAND_MESSAGE = "Failed to delete Brand";
                public const string FAILED_TO_SAVE_BRAND_MESSAGE = "Failed to save Brand";
                public const string BRAND_ID_DOES_NOT_EXISTS_MESSAGE = "Brand Id doesn’t not exist.";

                // End: Product Brand API error message.

                // Start: Customer Category API error message.

                public const string FAILED_TO_FETCH_CUSTOMER_CATEGORY_DATA_MESSAGE = "Failed to Fetch Customer category data.";
                public const string CUSTOMER_CATEGORY_ID_DOES_NOT_EXISTS = "Customer Category Id doesn't exists.";
                public const string NO_CUSTOMER_CATEGORY_FOUND_MESSAGE = "No customer category found.";
                public const string CUSTOMER_CATEGORY_EXIST_MESSAGE = "Category name already exists";
                public const string FAILED_TO_UPDATED_CUSTOMER_CATEGORY_MESSAGE = "Failed to update Customer category";
                public const string FAILED_TO_DELETED_CUSTOMER_CATEGORY_MESSAGE = "Failed to delete Customer category";
                public const string FAILED_TO_SAVE_CUSTOMER_CATEGORY_MESSAGE = "Failed to add Customer category";
                public const string CUSTOMER_CATEGORY_NAME_CAN_NOT_BE_BLANK_MESSAGE = "Customer Category Name can not be blank";

                // End: Customer Category API error messages.


                // Start: User Role API error messages

                public const string FAILED_TO_FETCH_ROLES_DATA_MESSAGE = "Failed to get Roles data";
                public const string ROLE_ID_DOES_NOT_EXISTS_MESSAGE = "Role Id does not exists";
                public const string NO_ROLE_FOUND_MESSAGE = "No Role Found";
                public const string ROLE_NAME_CAN_NOT_BE_BLANK = "Role name can not be black";
                public const string ROLE_EXIST_MESSAGE = "Role already exists";
                public const string FAILED_TO_SAVE_ROLE_MESSAGE = "Failed to save role";
                public const string FAILED_TO_UPDATE_ROLE_MESSAGE = "Failed to update role";
                public const string FAILED_TO_DELETE_ROLE_MESSAGE = "Failed to delete role";
                public const string FEATURE_ID_DOES_NOT_EXISTS = "Feature does not exists";
                public const string UNAUTHORIZED_ROLE_DATA_ACCESS_ERROR_CODE = "Unauthorized Role data access.";

                // End:User Role API error messages.


                // Start: Product API error messages.

                public const string FAILED_TO_FETCH_PRODUCT_DATA_MESSAGE = "Failed to get product data";
                public const string PRODUCT_ID_DOES_NOT_EXISTS_MESSAGE = "No product found.";
                public const string PRODUCT_EXIST_MESSAGE = "Product name already exists";
                public const string FAILED_TO_UPDATE_PRODUCT_MESSAGE = "Failed to update Product";
                public const string FAILED_TO_DELETE_PRODUCT_MESSAGE = "Failed to delete Product";
                public const string FAILED_TO_SAVE_PRODUCT_MESSAGE = "Failed to delete Product";
                public const string PRODUCT_NAME_CAN_NOT_BE_BLANK = "Product Name can not be blank";
                public const string BRAND_ID_CAN_NOT_BE_BLANK_MESSAGE = "Brand Id can not be blank";
                public const string PRODUCT_CATEGORY_ID_CAN_NOT_BE_BLANK_MESSAGE = "Product category Id can not be blank";
                public const string ALERT_QUANTITY_CAN_NOT_BE_BLANK_MESSAGE = "Alert Quantity can not be blank";

                // End: Product API error messages.


                // Start: Product category API error messages

                public const string FAILED_TO_FETCH_PRODUCT_CATEGORY_DATA_MESSAGE = "Failed to fetch Product Category data";
                public const string PRODUCT_CATEGORY_ID_DOES_NOT_EXISTS_MESSAGE = "Product Category Id does not exits";
                public const string NO_PRODUCT_CATEGORY_FOUND_MESSAGE = "No Product Category found ";
                public const string PRODUCT_CATEGORY_NAME_CAN_NOT_BE_BLANK_ERROR_CODE = "Product Category name can not be blank";
                public const string FAILED_TO_SAVE_PRODUCT_CATEGORY_MESSAGE = "Failed to save Product Category";
                public const string FAILED_TO_UPDATE_PRODUCT_CATEGORY_MESSAGE = "Failed to update Product Category";
                public const string FAILED_TO_DELETE_PRODUCT_CATEGORY_MESSAGE = "Failed to delete Product Category";
                public const string PRODUCT_CATEGORY_EXIST_MESSAGE = "Product Category Already exists";

                // End: Product category API error messages.


                // Start: Supplier API error messages

                public const string FAILED_TO_FETCH_SUPPLIER_DATA_MESSAGE = "Failed to get Supplier data";
                public const string SUPPLIER_ID_DOES_NOT_EXISTS_MESSAGE = "Supplier Id does not exists.";
                public const string SUPPLIER_ALREADY_EXIST_MESSAGE = "Supplier name already exists";
                public const string FAILED_TO_SAVE_SUPPLIER_MESSAGE = "Failed to save Supplier";
                public const string FAILED_TO_UPDATE_SUPPLIER_MESSAGE = "Failed to Update Supplier";
                public const string FAILED_TO_DELETE_SUPPLIER_MESSAGE = "Failed to delete Supplier";
                public const string SUPPLIER_CONTACT_NO_CAN_NOT_BE_BLANK_MESSAGE = "Supplier contact no can not be blank";
                public const string SUPPLIER_NAME_CAN_NOT_BE_BLANK_MESSAGE = "Supplier name can not be blank";
                public const string SUPPLIER_ADDRESS_CAN_NOT_BE_BLANK_MESSAGE = "Supplier address can not be blank";
                public const string FAILED_TO_FETCH_PURCHASE_DOCUMENTS_ERROR_CODE = "Failed to fetch Purchase documents.";

                // End: Supplier API error messages.


                // Start: Users API error messages.

                public const string FAILED_TO_FETCH_USER_DATA = "Failed to fetch User data.";
                public const string USER_ID_DOES_NOT_EXIST = "User Id does not exists.";
                public const string NO_USER_FOUND_MESSAGE = "No matching User found";
                public const string INVALID_USER_EMAIL_MESSAGE = "Invalid user email Id. ";
                public const string USER_EMAIL_CAN_NOT_BE_BLANK_MESSAGE = "User email id can not be blank.";
                public const string USER_NAME_CAN_NOT_BE_BLANK_MESSAGE = "User name can not be blank.";
                public const string FAILED_TO_ADD_USER_MESSAGE = "Failed to add user data. ";
                public const string FAILED_TO_UPDATE_USER_MESSAGE = "Failed to update user data. ";
                public const string FAILED_TO_DELETE_USER_MESSAGE = "Failed to Delete user data. ";

                // End: Users API error messages.


                // Start: Quotation API error messages.

                public const string FAILED_TO_FETCH_QUOTATION_DATA_MESSAGE = "Failed to fetch Quotation data.";
                public const string QUOTATION_ID_DOES_NOT_EXISTS_MESSAGE = "Quotation Id does not exists.";
                public const string QUOTATION_ALREADY_EXISTS_MESSAGE = "Quotation already exists.";
                public const string FAILED_TO_SAVE_QUOTATION_MESSAGE = "Failed to save quotation user data.";
                public const string FAILED_TO_UPDATE_QUOTATION_MESSAGE = "Failed to update quotation user data.";
                public const string FAILED_TO_DELETE_QUOTATION_MESSAGE = "Failed to delete quotation user data.";
                public const string QUOTATION_DOCUMENT_PATH_DOES_NOT_EXISTS_MESSAGE = "Quotation document path does not exists";
                public const string CUSTOMER_ID_CAN_NOT_BE_BLANK = "Customer Id can not be blank";
                public const string QUOTATION_NO_CAN_NOT_BE_BLANK = "Quotation no can not be blank";

                // End: Quotation API error messages.


                // Start: Quotation Template API error messages.

                public const string FAILED_TO_FETCH_QUOTATION_TEMPLATE_DATA_MESSAGE = "Failed to fetch Quotation Template data.";
                public const string QUOTATION_TEMPLATE_ID_DOES_NOT_EXISTS_MESSAGE = "Quotation Template Id does not exists.";
                public const string QUOTATION_TEMPLATE_NAME_CAN_NOT_BE_BLANK_MESSAGE = "Quotation Templatename can not be blank.";
                public const string QUOTATION_TEMPLATE_ALREADY_EXISTS_MESSAGE = "Quotation Template already exists.";
                public const string FAILED_TO_SAVE_QUOTATION_TEMPLATE_MESSAGE = "Failed to save quotation Template  data.";
                public const string FAILED_TO_UPDATE_QUOTATION_TEMPLATE_MESSAGE = "Failed to update Quotation Template  data.";
                public const string FAILED_TO_DELETE_QUOTATION_TEMPLATE_MESSAGE = "Failed to delete quotation Template  data.";

                // End: Quotation Template API error messages.


                // Start: POS API error messages.

                public const string FAILED_TO_FETCH_POS_DATA_MESSAGE = "Failed to fetch  POS data.";
                public const string POS_ID_DOES_NOT_EXISTS_MESSAGE = "POS Id does not exists.";
                public const string POS_ALREADY_EXISTS_MESSAGE = "POS already exists.";
                public const string FAILED_TO_SAVE_POS_MESSAGE = "Failed to save POS  data.";
                public const string FAILED_TO_UPDATE_POS_MESSAGE = "Failed to update POS  data.";
                public const string FAILED_TO_DELETE_POS_MESSAGE = "Failed to delete POS  data.";
                public const string CUTOMER_ID_CAN_NOT_BE_BLANK_MESSAGE = "Customer Id can not be blank";
                public const string POS_DATE_CAN_NOT_BE_BLANK_MESSAGE = "POS date can not be blank";
                public const string TOTAL_BILL_CAN_NOT_BE_BLANK_MESSAGE = "Total Bill can not be blank";
                public const string PAYMENT_METHOD_CAN_NOT_BE_BLANK_MESSAGE = "Payment Method can not be blank";
                public const string AMOUNT_CAN_NOT_BE_BLANK_MESSAGE = "Amount can not be blank";
                public const string CAN_NOT_DELETE_SALES_WITH_PAYMENTS_MESSAGE = "Can not delete Sales with Sales Payment";
                public const string EXPIRED_PRODUCTS_DATA_NOT_FOUND_ERROR_CODE = "Expired products data not found.";
                public const string LOW_STOCK_PRODUCTS_DATA_NOT_FOUND_ERROR_CODE = "Low stock products data not found.";
                public const string NEAR_EXPIRE_PRODUCT_DATA_NOT_FOUND_ERROR_CODE = "Near Expire products data not found.";

                // End: POS API error messages.


                // Start: SaleReturn API error messages.

                public const string FAILED_TO_FETCH_SALESRETURN_DATA_MESSAGE = "Failed to fetch  sale retrun data.";
                public const string SALESRETURN_ID_DOES_NOT_EXISTS_MESSAGE = "Sales retrun Id does not exists.";
                public const string SALESRETURN_ALREADY_EXISTS_MESSAGE = "Sales return already exists.";
                public const string FAILED_TO_SAVE_SALESRETURN_MESSAGE = "Failed to save sales return data.";
                public const string FAILED_TO_UPDATE_SALESRETURN_MESSAGE = "Failed to update sales return data.";
                public const string FAILED_TO_DELETE_SALESRETURN_MESSAGE = "Failed to delete sales return data.";
                public const string BATCH_ID_DOES_NOT_EXISTS_MESSAGE = "Batch Id does not exists.";
                public const string RETURN_DATE_CAN_NOT_BE_BLANK_MESSAGE = "Return date can not be blank";
                public const string PRODUCT_ID_CAN_NOT_BE_BLANK_ERROR_CODE = "Product Id can not be blank.";
                public const string QUANTITY_CAN_NOT_BE_BLANK_ERROR_CODE = "Quantity can not be blank.";
                public const string SALE_RETURN_PAYMENT_METHOD_CAN_NOT_BE_BLANK_ERROR_CODE = "PaymentMethod can not be blank.";
                public const string SALE_RETURN_AMOUNT_CAN_NOT_BE_BLANK_ERROR_CODE = "Amount can not be blank.";
                public const string EXPIRED_PRODUCTS_NOT_FOUND_ERROR_CODE = "Expired products data not found.";
                public const string LOW_STOCK_PRODUCTS_NOT_FOUND_ERROR_CODE = "Low stock products data not found.";
                public const string TOTAL_BILL_CAN_NOT_BE_BLANK_ERROR_CODE = "Total Bill can not be blank.";

                //End: SaleReturn API error messages.


                //Start: Expense Category API error messages.

                public const string FAILED_TO_FETCH_EXPENSE_CATEGORY_DATA_MESSAGE = "Failed to get expenses category data.";
                public const string EXPENSE_CATEGORY_ID_DOES_NOT_EXISTS = "Expense Category Id doesn't exists.";
                public const string EXPENSE_CATEGORY_NAME_CAN_NOT_BE_BLANK_MESSAGE = "Expense Category namecan not be blank.";
                public const string EXPENSE_CATEGORY_EXIST_MESSAGE = "Expense Category name already exists";
                public const string FAILED_TO_UPDATED_EXPENSE_CATEGORY_MESSAGE = "Failed to update expense category"; 
                public const string FAILED_TO_DELETED_EXPENSE_CATEGORY_MESSAGE = "Failed to delete expense category";
                public const string FAILED_TO_SAVE_EXPENSE_CATEGORY_MESSAGE = "Failed to add expense category";

                //end: Expense Category API error messages.


                // Start: Expense API error messages.

                public const string FAILED_TO_FETCH_EXPENSE_DATA_MESSAGE = "Failed to get expense data.";
                public const string EXPENSE_ID_DOES_NOT_EXISTS = "Expense  Id doesn't exists.";
                public const string EXPENSE_NAME_CAN_NOT_BE_BLANK_MESSAGE = "Expense namecan not be blank.";
                public const string EXPENSE_EXIST_MESSAGE = "Expense name already exists";
                public const string FAILED_TO_UPDATED_EXPENSE_MESSAGE = "Failed to update expense";
                public const string FAILED_TO_DELETED_EXPENSE_MESSAGE = "Failed to delete expense";
                public const string FAILED_TO_SAVE_EXPENSE_MESSAGE = "Failed to add expense";
                public const string EXPENSE_NOTE_CAN_NOT_BE_NULL_MESSAGE = "Expense note can not be blank";
                public const string EXPENSE_DATE_CAN_NOT_BE_NULL_MESSAGE = "Expense date can not be blank";
                public const string EXPENSE_AMOUNT_CAN_NOT_BE_NULL_MESSAGE = "Expense amount can not be blank";
                public const string EXPENSE_CATEGORY_ID_CAN_NOT_BE_NULL_MESSAGE = "Expense category Id can not be blank";

                // End: Expense API error messages.


                //Start: Purchase API error messages.

                public const string FAILED_TO_FETCH_PURCHASE_DATA_MESSAGE = "Failed to fetch Purchase data.";
                public const string PURCHASE_ID_DOES_NOT_EXISTS_MESSAGE = "Purchase Id does not exists.";
                public const string PURCHASE_ALREADY_EXISTS_MESSAGE = "Purchase already exists.";
                public const string FAILED_TO_SAVE_PURCHASE_MESSAGE = "Failed to save Purchase user data.";
                public const string FAILED_TO_UPDATE_PURCHASE_MESSAGE = "Failed to update Purchase user data.";
                public const string FAILED_TO_DELETE_PURCHASE_MESSAGE = "Failed to delete Purchase user data.";
                public const string PURCHASE_DOCUMENT_PATH_DOES_NOT_EXISTS_MESSAGE = "Purchase document path does not exists";
                public const string INVOICE_NUMBER_CAN_NOT_BE_BLANK = "Invoice Number Can Not Be Blank";
                public const string PURCHASE_DATE_CAN_NOT_BE_BLANK = " Purchase Date Can Not Be Blank";
                public const string BATCH_NO_DOES_NOT_EXISTS_MESSAGE = "Batch no does not Exist";
                public const string BATCH_NO_CAN_NOT_BE_BLANK = "Batch no can not be Blank";
                public const string EXPIRY_DATE_CAN_NOT_BE_BLANK = "Expiry Date can not be Blank";
                public const string PACK_OF_CAN_NOT_BE_BLANK = "Pack Of can not be Blank";
                public const string MRP_PER_PACK_CAN_NOT_BE_BLANK = "MRP per Pack can not be Blank";
                public const string QUANTITY_CAN_NOT_BE_BLANK = "Quantity can not be Blank";
                public const string TOTAL_BILL_CAN_NOT_BE_BLANK = "Total Bill can not be Blank";
                public const string PAYMENT_ID_DOES_NOT_EXISTS_MESSAGE = "Payment Id Does Not Exist";
                public const string RECEIVER_NAME_CAN_NOT_BE_BLANK = "Receiver Name can not be Blank";
                public const string RECEIVER_CONTACT_CAN_NOT_BE_BLANK = "Receiver Contact can not be Blank";
                public const string PAYMENT_DATE_CAN_NOT_BE_BLANK = "Payment date can not be Blank";
                public const string AMOUNT_CAN_NOT_BE_BLANK = "Amount can not be Blank";
                public const string PAYMENT_METHOD_CAN_NOT_BE_BLANK = "Payment Method can not be Blank";
                public const string CAN_NOT_DELETE_PURCHASE_WITH_PAYMENTS_MESSAGE = "Can not delete Purchase with Purchase Payment";

                //End: Purchase API error messages.



                //Start: Purchase return API error messages.

                public const string FAILED_TO_FETCH_PURCHASE_RETURN_DATA_MESSAGE = "Failed to fetch purchase return data";
                public const string RETRUN_ID_DOES_NOT_EXISTS_MESSAGE = "Return id does not exist";
                public const string PURCHASE_RETURN_ALREADY_EXISTS_MESSAGE = "Purchase Return already exist";
                public const string PRODUCT_ID_FK_DOES_NOT_EXISTS_MESSAGE = "Product Id does not exist";
                public const string FAILED_TO_SAVE_PURCHASE_RETURN_MESSAGE = "failed to save Purchase Return data ";
                public const string FAILED_TO_DELETE_PURCHASE_RETURN_MESSAGE = "failed to delete Purchase return data";
                public const string BATCH_ID_FK_DOES_NOT_EXISTS_MESSAGE = "Batch Id does not exist";
                public const string FAILED_TO_UPDATE_PURCHASE_RETURN_MESSAGE = "failed to update Purchase return data";
                public const string SUPPLIER_ID_CAN_NOT_BE_BLANK = "Supplier id can not be blank";
                public const string RETURN_REF_NO_CAN_NOT_BE_BLANK = "Return Ref No can not be blank";
                public const string PURCHASE_RETURN_DATE_CAN_NOT_BE_BLANK = "Purchase Return Date can not be blank";
                public const string PRODUCT_ID_FK_CAN_NOT_BE_BLANK = "Product id can not be blank";
                public const string BATCH_ID_FK_CAN_NOT_BE_BLANK = "Batch id can not be blank";
                public const string RETURN_QUANTITY_CAN_NOT_BE_BLANK = "Return Quantity can not be blank";
                public const string NO_DATA_AVAILABLE_MESSAGE = "No data Available";

                //End: Purchase return API error messages.


                //Start: Purchase Order API error messages.

                public const string FAILED_TO_FETCH_PURCHASE_ORDER_DATA_MESSAGE = "Failed to Fetch purchase order data";
                public const string PURCHASE_ORDER_ALREADY_EXISTS_MESSAGE = "purchase order data already exists";
                public const string PURCHASE_ORDER_NUMBER_CAN_NOT_BE_BLANK = "Purchase Order Number can not be blank";
                public const string PURCHASE_ORDER_DATE_CAN_NOT_BE_BLANK = "Purchase Order Date can not be blank";
                public const string SUPPLIER_ID_OF_PO_CAN_NOT_BE_BLANK = "Supplier id of Purchase Order can not be blank";
                public const string PURCHASE_ORDER_STATUS_CAN_NOT_BE_BLANK = "Purchase Order Status can not be blank";
                public const string PRODUCT_ID_FK_OF_PO_CAN_NOT_BE_BLANK = "Product Id of Purchase Order Record can not be blank";
                public const string ORDER_QUANTITY_OF_PO_CAN_NOT_BE_BLANK = "Purchase Order Quantity can not be blank";
                public const string FAILED_TO_SAVE_PURCHASE_ORDER_MESSAGE = "Failed to save Purchase order data";
                public const string FAILED_TO_UPDATE_PURCHASE_ORDER_MESSAGE = "Failed to update Purchase order data";
                public const string FAILED_TO_DELETE_PURCHASE_ORDER_MESSAGE = "Failed to delete Purchase order data";
                public const string PURCHASE_ORDER_NUMBER_CAN_NOT_BE_BLANK_ERROR_CODE = "Purchase Note can not be blank.";
                public const string PURCHASE_ORDER_ALREADY_EXISTS_ERROR_CODE = "Purchase order already exists.";

                // End: Purchase Order API error messages.


                public const string FAILED_TO_FETCH_FEATURE_DATA_MESSAGE = "failed to fetch Features data";

                public const string FAILED_TO_FETCH_QUOTATION_DOCUMENT_DATA_MESSAGE = "failed to fetch Quotation data";


            }
        }

    }

}
