use HospitalDb;

INSERT INTO [user] 
(RoleIdFk, user_code, name,password, address, email, user_status, contact_no_1, contact_no_2, contact_no_3, created_by, created_at) 
VALUES
( 1,'U-1', 'John Doe', 'password123', '123 Main St', 'pranav.mahadik@gmail.com', 0, '9404406441', '0987654321', '5555555555', 1,  GETDATE());








INSERT INTO supplier 
    ( supplier_code, name, address, contact_no_1, contact_no_2, contact_no_3, created_by, created_at)
VALUES 
    ( 'S-1', 'Amit shah', '1234 Elm St, City, State, ZIP', '1234567890', '0987654321', '1112223333', 1,  GETDATE()),
    ( 'S-2', 'Willam Jake', '5678 Oak St, City, State, ZIP', '2345678901', '8901234567', '2223334444', 1, GETDATE());
	
	
	
INSERT INTO customer_category 
    (category_name, description, created_by, created_at)
VALUES 
    ( 'General', 'General category for customers', 1,  GETDATE()),
    ( 'VIP', 'VIP category for premium customers', 1,  GETDATE());
	
	
INSERT INTO customer 
    (category_id_fk, customer_code, name,ethinity , address, custom_field_1, custom_field_2, contact_no_1, contact_no_2, contact_no_3, created_by, created_at)
VALUES 
    ( 1, 'C-1', 'John Doe', 'Caucasian', '1234 Maple St, City, State, ZIP', 'Custom Value 1', 'Custom Value 2', '1234567890', '0987654321', '1112223333', 1, GETDATE()),
    ( 2, 'C-2', 'Jane Smith', 'Asian', '5678 Oak St, City, State, ZIP', 'Custom Value 3', 'Custom Value 4', '2345678901', '8901234567', '2223334444', 1,  GETDATE());

INSERT INTO brand
    ( brand_name, description, created_by, created_at)
VALUES 
    ( 'BrandA', 'Description of BrandA', 1, GETDATE()),
    ( 'BrandB', 'Description of BrandB', 1, GETDATE());

	
INSERT INTO product_category
       (category_name,description,created_by,created_at)
VALUES
       ('Medical Equipment','Devices and instruments used in hospitals',1,GETDATE());
	   
	   
	   INSERT INTO product
       (product_name,brand_id_fk,product_category_id_fk,alert_quantity,quantity,sequence_sorting,discount_percent
       ,custom_field_1,custom_field_2,custom_field_3,created_by,created_at)
VALUES
       ( 'citrizine',1,1,10,50,1,5,'custom value 1','custom value 2','custom value 3',1,GETDATE());





	  
	  
      