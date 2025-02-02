USE [HospitalDB]
GO
--ALTER TABLE [dbo].[users_tbl] DROP CONSTRAINT [FK_users_tbl_roles_tbl]
--GO
--ALTER TABLE [dbo].[sales_tbl] DROP CONSTRAINT [FK_sales_tbl_customer_tbl]
--GO
--ALTER TABLE [dbo].[sales_returns_tbl] DROP CONSTRAINT [FK_sales_returns_tbl_customer_tbl]
--GO
--ALTER TABLE [dbo].[sales_return_records_tbl] DROP CONSTRAINT [FK_sales_return_records_tbl_sales_returns_tbl]
--GO
--ALTER TABLE [dbo].[sales_return_records_tbl] DROP CONSTRAINT [FK_sales_return_records_tbl_product_tbl]
--GO
--ALTER TABLE [dbo].[sales_return_records_tbl] DROP CONSTRAINT [FK_sales_return_records_tbl_batch_tbl]
--GO
--ALTER TABLE [dbo].[sales_records_tbl] DROP CONSTRAINT [FK_sales_records_tbl_sales_tbl]
--GO
--ALTER TABLE [dbo].[sales_records_tbl] DROP CONSTRAINT [FK_sales_records_tbl_product_tbl]
--GO
--ALTER TABLE [dbo].[sales_payments_tbl] DROP CONSTRAINT [FK_sales_payments_tbl_sales_tbl]
--GO
--ALTER TABLE [dbo].[role_features_tbl] DROP CONSTRAINT [FK_role_features_tbl_roles_tbl]
--GO
--ALTER TABLE [dbo].[role_features_tbl] DROP CONSTRAINT [FK_role_features_tbl_features_tbl]
--GO
--ALTER TABLE [dbo].[quotation_temp_records_tbl] DROP CONSTRAINT [FK_quotation_temp_records_tbl_quotation_temp_tbl]
--GO
--ALTER TABLE [dbo].[quotation_temp_records_tbl] DROP CONSTRAINT [FK_quotation_temp_records_tbl_product_tbl]
--GO
--ALTER TABLE [dbo].[quotation_tbl] DROP CONSTRAINT [FK_quotation_tbl_customer_tbl]
--GO
--ALTER TABLE [dbo].[quotation_records_tbl] DROP CONSTRAINT [FK_quotation_records_tbl_quotation_tbl]
--GO
--ALTER TABLE [dbo].[quotation_records_tbl] DROP CONSTRAINT [FK_quotation_records_tbl_product_tbl]
--GO
--ALTER TABLE [dbo].[purchase_tbl] DROP CONSTRAINT [FK_purchase_tbl_supplier_tbl]
--GO
--ALTER TABLE [dbo].[purchase_returns_tbl] DROP CONSTRAINT [FK_purchase_returns_tbl_purchase_returns_tbl]
--GO
--ALTER TABLE [dbo].[purchase_return_records_tbl] DROP CONSTRAINT [FK_purchase_return_records_tbl_purchase_returns_tbl]
--GO
--ALTER TABLE [dbo].[purchase_return_records_tbl] DROP CONSTRAINT [FK_purchase_return_records_tbl_product_tbl]
--GO
--ALTER TABLE [dbo].[purchase_return_records_tbl] DROP CONSTRAINT [FK_purchase_return_records_tbl_batch_tbl1]
--GO
--ALTER TABLE [dbo].[purchase_records_tbl] DROP CONSTRAINT [FK_purchase_records_tbl_purchase_tbl]
--GO
--ALTER TABLE [dbo].[purchase_records_tbl] DROP CONSTRAINT [FK_purchase_records_tbl_product_tbl]
--GO
--ALTER TABLE [dbo].[purchase_records_tbl] DROP CONSTRAINT [FK_purchase_records_tbl_batch_tbl]
--GO
--ALTER TABLE [dbo].[purchase_payments_tbl] DROP CONSTRAINT [FK_purchase_payments_tbl_purchase_tbl]
--GO
--ALTER TABLE [dbo].[product_tbl] DROP CONSTRAINT [FK_product_tbl_product_category_tbl]
--GO
--ALTER TABLE [dbo].[product_tbl] DROP CONSTRAINT [FK_product_tbl_brand_tbl_ID]
--GO
--ALTER TABLE [dbo].[po_tbl] DROP CONSTRAINT [FK_po_tbl_supplier_tbl]
--GO
--ALTER TABLE [dbo].[po_records_tbl] DROP CONSTRAINT [FK_po_records_tbl_product_tbl]
--GO
--ALTER TABLE [dbo].[po_records_tbl] DROP CONSTRAINT [FK_po_records_tbl_po_tbl]
--GO
--ALTER TABLE [dbo].[expense_tbl] DROP CONSTRAINT [FK_expense_tbl_expense_category_tbl]
--GO
--ALTER TABLE [dbo].[customer_tbl] DROP CONSTRAINT [FK_customer_tbl_customer_category_tbl]
--GO
--ALTER TABLE [dbo].[batch_tbl] DROP CONSTRAINT [FK_batch_tbl_product_tbl]
--GO
--/****** Object:  Table [dbo].[users_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[users_tbl]
--GO
--/****** Object:  Table [dbo].[supplier_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[supplier_tbl]
--GO
--/****** Object:  Table [dbo].[sales_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[sales_tbl]
--GO
--/****** Object:  Table [dbo].[sales_returns_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[sales_returns_tbl]
--GO
--/****** Object:  Table [dbo].[sales_return_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[sales_return_records_tbl]
--GO
--/****** Object:  Table [dbo].[sales_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[sales_records_tbl]
--GO
--/****** Object:  Table [dbo].[sales_payments_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[sales_payments_tbl]
--GO
--/****** Object:  Table [dbo].[roles_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[roles_tbl]
--GO
--/****** Object:  Table [dbo].[role_features_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[role_features_tbl]
--GO
--/****** Object:  Table [dbo].[quotation_temp_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[quotation_temp_tbl]
--GO
--/****** Object:  Table [dbo].[quotation_temp_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[quotation_temp_records_tbl]
--GO
--/****** Object:  Table [dbo].[quotation_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[quotation_tbl]
--GO
--/****** Object:  Table [dbo].[quotation_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[quotation_records_tbl]
--GO
--/****** Object:  Table [dbo].[purchase_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[purchase_tbl]
--GO
--/****** Object:  Table [dbo].[purchase_returns_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[purchase_returns_tbl]
--GO
--/****** Object:  Table [dbo].[purchase_return_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[purchase_return_records_tbl]
--GO
--/****** Object:  Table [dbo].[purchase_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[purchase_records_tbl]
--GO
--/****** Object:  Table [dbo].[purchase_payments_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[purchase_payments_tbl]
--GO
--/****** Object:  Table [dbo].[product_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[product_tbl]
--GO
--/****** Object:  Table [dbo].[product_category_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[product_category_tbl]
--GO
--/****** Object:  Table [dbo].[po_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[po_tbl]
--GO
--/****** Object:  Table [dbo].[po_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[po_records_tbl]
--GO
--/****** Object:  Table [dbo].[features_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[features_tbl]
--GO
--/****** Object:  Table [dbo].[expense_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[expense_tbl]
--GO
--/****** Object:  Table [dbo].[expense_category_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[expense_category_tbl]
--GO
--/****** Object:  Table [dbo].[customer_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[customer_tbl]
--GO
--/****** Object:  Table [dbo].[customer_category_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[customer_category_tbl]
--GO
--/****** Object:  Table [dbo].[brand_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[brand_tbl]
--GO
--/****** Object:  Table [dbo].[batch_tbl]    Script Date: 31-10-2022 10:58:20 ******/
--DROP TABLE [dbo].[batch_tbl]
--GO
/****** Object:  Table [dbo].[batch_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[batch_tbl](
	[batch_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[product_id_fk] [bigint] NULL,
	[batch_no] [varchar](50) NULL,
	[expiry_date] [datetime] NULL,
	[pack_of] [int] NULL,
	[mrp_per_pack] [decimal](18, 0) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_batch_tbl] PRIMARY KEY CLUSTERED 
(
	[batch_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[brand_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[brand_tbl](
	[brand_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[brand_name] [varchar](50) NULL,
	[description] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_brand_tbl] PRIMARY KEY CLUSTERED 
(
	[brand_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[customer_category_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[customer_category_tbl](
	[category_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[category_name] [varchar](50) NULL,
	[description] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_customer_category_tbl] PRIMARY KEY CLUSTERED 
(
	[category_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[customer_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[customer_tbl](
	[customer_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[category_id_fk] [bigint] NOT NULL,
	[first_name] [varchar](50) NULL,
	[middle_name] [varchar](50) NULL,
	[last_name] [varchar](50) NULL,
	[ethiniity] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[profile_pic] [varchar](50) NULL,
	[custom_field_1] [varchar](50) NULL,
	[custom_field_2] [varchar](50) NULL,
	[contact_no_1] [varchar](50) NULL,
	[contact_no_2] [varchar](50) NULL,
	[contact_no_3] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_customer_tbl] PRIMARY KEY CLUSTERED 
(
	[customer_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[expense_category_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[expense_category_tbl](
	[category_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[category_name] [varchar](50) NULL,
	[description] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_expense_category_tbl] PRIMARY KEY CLUSTERED 
(
	[category_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[expense_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[expense_tbl](
	[expense_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[expense_cat_id_fk] [bigint] NULL,
	[amount] [decimal](18, 0) NULL,
	[date] [datetime] NULL,
	[note] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_expense_tbl] PRIMARY KEY CLUSTERED 
(
	[expense_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[features_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[features_tbl](
	[feature_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[feature_name] [varchar](50) NULL,
	[description] [varchar](100) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_role_features_tbl] PRIMARY KEY CLUSTERED 
(
	[feature_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[po_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[po_records_tbl](
	[po_id_fk] [bigint] NULL,
	[product_id_fk] [bigint] NULL,
	[order_quantity] [bigint] NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[po_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[po_tbl](
	[po_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[supplier_id_fk] [bigint] NULL,
	[po_number] [varchar](50) NULL,
	[po_date] [datetime] NULL,
	[status] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_po_tbl] PRIMARY KEY CLUSTERED 
(
	[po_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[product_category_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[product_category_tbl](
	[category_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[category_name] [varchar](50) NULL,
	[description] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_product_category_tbl] PRIMARY KEY CLUSTERED 
(
	[category_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[product_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[product_tbl](
	[product_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[product_name] [varchar](50) NULL,
	[brand_id_fk] [bigint] NULL,
	[product_category_id_fk] [bigint] NULL,
	[alert_quantity] [bigint] NULL,
	[quantity] [bigint] NULL,
	[sequence_sorting] [varchar](50) NULL,
	[discount_percent] [decimal](18, 0) NULL,
	[custom_field_1] [varchar](50) NULL,
	[custom_field_2] [varchar](50) NULL,
	[custom_field_3] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_product_tbl] PRIMARY KEY CLUSTERED 
(
	[product_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[purchase_payments_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[purchase_payments_tbl](
	[payment_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[purchase_id_fk] [bigint] NULL,
	[receiver_name] [varchar](50) NULL,
	[receiver_contact] [varchar](50) NULL,
	[payment_method] [varchar](50) NULL,
	[amount] [decimal](18, 0) NULL,
	[payment_date] [datetime] NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_purchase_payments_tbl] PRIMARY KEY CLUSTERED 
(
	[payment_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[purchase_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[purchase_records_tbl](
	[purchase_id_fk] [bigint] NULL,
	[product_id_fk] [bigint] NULL,
	[batch_id_fk] [bigint] NULL,
	[order_quantity] [bigint] NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[purchase_return_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[purchase_return_records_tbl](
	[return_id_fk] [bigint] NULL,
	[product_id_fk] [bigint] NULL,
	[batch_id_fk] [bigint] NULL,
	[return_quantity] [bigint] NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[purchase_returns_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[purchase_returns_tbl](
	[return_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[supplier_id_fk] [bigint] NULL,
	[return_ref_no] [varchar](50) NULL,
	[return_date] [datetime] NULL,
	[total_bill] [decimal](18, 0) NULL,
	[todal_paid] [decimal](18, 0) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_purchase_returns_tbl] PRIMARY KEY CLUSTERED 
(
	[return_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[purchase_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[purchase_tbl](
	[purchase_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[supplier_id_fk] [bigint] NULL,
	[invoice_number] [varchar](50) NULL,
	[purchase_date] [datetime] NULL,
	[total_bill] [decimal](18, 0) NULL,
	[total_paid] [decimal](18, 0) NULL,
	[document_location] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_purchase_tbl] PRIMARY KEY CLUSTERED 
(
	[purchase_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[quotation_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[quotation_records_tbl](
	[quotation_id_fk] [bigint] NULL,
	[product_id_fk] [bigint] NULL,
	[new_field_1] [varchar](50) NULL,
	[new_field_2] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[quotation_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[quotation_tbl](
	[quotation_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[customer_id_fk] [bigint] NULL,
	[quotation_no] [varchar](50) NULL,
	[document_location] [varchar](50) NULL,
	[quotation_date] [datetime] NULL,
	[sell_note_1] [varchar](50) NULL,
	[sell_note_2] [varchar](50) NULL,
	[sell_note_3] [varchar](50) NULL,
	[sell_note_4] [varchar](50) NULL,
	[new_field_1] [varchar](50) NULL,
	[new_field_2] [varchar](50) NULL,
	[new_field_3] [varchar](50) NULL,
	[new_field_4] [varchar](50) NULL,
	[new_field_5] [varchar](50) NULL,
	[new_field_6] [varchar](50) NULL,
	[new_field_7] [varchar](50) NULL,
	[new_field_8] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_quotation_tbl] PRIMARY KEY CLUSTERED 
(
	[quotation_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[quotation_temp_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[quotation_temp_records_tbl](
	[template_id_fk] [bigint] NULL,
	[product_id_fk] [bigint] NULL,
	[custom_field_1] [varchar](50) NULL,
	[custom_field_2] [varchar](50) NULL,
	[custom_field_3] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[quotation_temp_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[quotation_temp_tbl](
	[template_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_quotation_temp_tbl] PRIMARY KEY CLUSTERED 
(
	[template_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[role_features_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[role_features_tbl](
	[role_id_fk] [bigint] NULL,
	[feature_id_fk] [bigint] NULL,
	[view_perm] [bit] NULL,
	[add_perm] [bit] NULL,
	[edit_perm] [bit] NULL,
	[delete_perm] [bit] NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[roles_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[roles_tbl](
	[role_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[role_name] [varchar](50) NULL,
	[description] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_roles_tbl] PRIMARY KEY CLUSTERED 
(
	[role_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sales_payments_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sales_payments_tbl](
	[payment_id_pk] [bigint] NOT NULL,
	[sales_id_fk] [bigint] NULL,
	[payment_method] [varchar](50) NULL,
	[amount] [decimal](18, 0) NULL,
	[payment_date] [datetime] NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_sales_payments_tbl] PRIMARY KEY CLUSTERED 
(
	[payment_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sales_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sales_records_tbl](
	[sales_id_fk] [bigint] NULL,
	[product_id_fk] [bigint] NULL,
	[quantity] [bigint] NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sales_return_records_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sales_return_records_tbl](
	[return_id_fk] [bigint] NULL,
	[product_id_fk] [bigint] NULL,
	[batch_id_fk] [bigint] NULL,
	[return_quantity] [bigint] NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sales_returns_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sales_returns_tbl](
	[return_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[customer_id_fk] [bigint] NULL,
	[return_ref_no] [varchar](50) NULL,
	[return_date] [datetime] NULL,
	[total_bill] [decimal](18, 0) NULL,
	[todal_paid] [decimal](18, 0) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_sales_returns_tbl] PRIMARY KEY CLUSTERED 
(
	[return_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sales_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sales_tbl](
	[sales_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[customer_id_fk] [bigint] NULL,
	[total_bill] [decimal](18, 0) NULL,
	[total_paid] [decimal](18, 0) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_sales_tbl] PRIMARY KEY CLUSTERED 
(
	[sales_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[supplier_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[supplier_tbl](
	[supplier_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[address] [varchar](100) NULL,
	[contact_no_1] [varchar](50) NULL,
	[contact_no_2] [varchar](50) NULL,
	[contact_no_3] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_supplier_tbl] PRIMARY KEY CLUSTERED 
(
	[supplier_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users_tbl]    Script Date: 31-10-2022 10:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users_tbl](
	[user_id_pk] [bigint] IDENTITY(1,1) NOT NULL,
	[role_id_fk] [bigint] NULL,
	[first_name] [varchar](50) NULL,
	[middle_name] [varchar](50) NULL,
	[last_name] [varchar](50) NULL,
	[address] [varchar](100) NULL,
	[email] [varchar](50) NULL,
	[profile_pic_path] [varchar](100) NULL,
	[contact_no_1] [varchar](50) NULL,
	[contact_no_2] [varchar](50) NULL,
	[contact_no_3] [varchar](50) NULL,
	[is_deleted] [char](1) NULL,
	[created_by] [int] NULL,
	[updated_by] [int] NULL,
	[deleted_by] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_users_tbl] PRIMARY KEY CLUSTERED 
(
	[user_id_pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[batch_tbl]  WITH CHECK ADD  CONSTRAINT [FK_batch_tbl_product_tbl] FOREIGN KEY([product_id_fk])
REFERENCES [dbo].[product_tbl] ([product_id_pk])
GO
ALTER TABLE [dbo].[batch_tbl] CHECK CONSTRAINT [FK_batch_tbl_product_tbl]
GO
ALTER TABLE [dbo].[customer_tbl]  WITH CHECK ADD  CONSTRAINT [FK_customer_tbl_customer_category_tbl] FOREIGN KEY([category_id_fk])
REFERENCES [dbo].[customer_category_tbl] ([category_id_pk])
GO
ALTER TABLE [dbo].[customer_tbl] CHECK CONSTRAINT [FK_customer_tbl_customer_category_tbl]
GO
ALTER TABLE [dbo].[expense_tbl]  WITH CHECK ADD  CONSTRAINT [FK_expense_tbl_expense_category_tbl] FOREIGN KEY([expense_cat_id_fk])
REFERENCES [dbo].[expense_category_tbl] ([category_id_pk])
GO
ALTER TABLE [dbo].[expense_tbl] CHECK CONSTRAINT [FK_expense_tbl_expense_category_tbl]
GO
ALTER TABLE [dbo].[po_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_po_records_tbl_po_tbl] FOREIGN KEY([po_id_fk])
REFERENCES [dbo].[po_tbl] ([po_id_pk])
GO
ALTER TABLE [dbo].[po_records_tbl] CHECK CONSTRAINT [FK_po_records_tbl_po_tbl]
GO
ALTER TABLE [dbo].[po_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_po_records_tbl_product_tbl] FOREIGN KEY([product_id_fk])
REFERENCES [dbo].[product_tbl] ([product_id_pk])
GO
ALTER TABLE [dbo].[po_records_tbl] CHECK CONSTRAINT [FK_po_records_tbl_product_tbl]
GO
ALTER TABLE [dbo].[po_tbl]  WITH CHECK ADD  CONSTRAINT [FK_po_tbl_supplier_tbl] FOREIGN KEY([supplier_id_fk])
REFERENCES [dbo].[supplier_tbl] ([supplier_id_pk])
GO
ALTER TABLE [dbo].[po_tbl] CHECK CONSTRAINT [FK_po_tbl_supplier_tbl]
GO
ALTER TABLE [dbo].[product_tbl]  WITH CHECK ADD  CONSTRAINT [FK_product_tbl_brand_tbl_ID] FOREIGN KEY([brand_id_fk])
REFERENCES [dbo].[brand_tbl] ([brand_id_pk])
GO
ALTER TABLE [dbo].[product_tbl] CHECK CONSTRAINT [FK_product_tbl_brand_tbl_ID]
GO
ALTER TABLE [dbo].[product_tbl]  WITH CHECK ADD  CONSTRAINT [FK_product_tbl_product_category_tbl] FOREIGN KEY([product_category_id_fk])
REFERENCES [dbo].[product_category_tbl] ([category_id_pk])
GO
ALTER TABLE [dbo].[product_tbl] CHECK CONSTRAINT [FK_product_tbl_product_category_tbl]
GO
ALTER TABLE [dbo].[purchase_payments_tbl]  WITH CHECK ADD  CONSTRAINT [FK_purchase_payments_tbl_purchase_tbl] FOREIGN KEY([purchase_id_fk])
REFERENCES [dbo].[purchase_tbl] ([purchase_id_pk])
GO
ALTER TABLE [dbo].[purchase_payments_tbl] CHECK CONSTRAINT [FK_purchase_payments_tbl_purchase_tbl]
GO
ALTER TABLE [dbo].[purchase_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_purchase_records_tbl_batch_tbl] FOREIGN KEY([batch_id_fk])
REFERENCES [dbo].[batch_tbl] ([batch_id_pk])
GO
ALTER TABLE [dbo].[purchase_records_tbl] CHECK CONSTRAINT [FK_purchase_records_tbl_batch_tbl]
GO
ALTER TABLE [dbo].[purchase_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_purchase_records_tbl_product_tbl] FOREIGN KEY([product_id_fk])
REFERENCES [dbo].[product_tbl] ([product_id_pk])
GO
ALTER TABLE [dbo].[purchase_records_tbl] CHECK CONSTRAINT [FK_purchase_records_tbl_product_tbl]
GO
ALTER TABLE [dbo].[purchase_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_purchase_records_tbl_purchase_tbl] FOREIGN KEY([purchase_id_fk])
REFERENCES [dbo].[purchase_tbl] ([purchase_id_pk])
GO
ALTER TABLE [dbo].[purchase_records_tbl] CHECK CONSTRAINT [FK_purchase_records_tbl_purchase_tbl]
GO
ALTER TABLE [dbo].[purchase_return_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_purchase_return_records_tbl_batch_tbl1] FOREIGN KEY([batch_id_fk])
REFERENCES [dbo].[batch_tbl] ([batch_id_pk])
GO
ALTER TABLE [dbo].[purchase_return_records_tbl] CHECK CONSTRAINT [FK_purchase_return_records_tbl_batch_tbl1]
GO
ALTER TABLE [dbo].[purchase_return_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_purchase_return_records_tbl_product_tbl] FOREIGN KEY([product_id_fk])
REFERENCES [dbo].[product_tbl] ([product_id_pk])
GO
ALTER TABLE [dbo].[purchase_return_records_tbl] CHECK CONSTRAINT [FK_purchase_return_records_tbl_product_tbl]
GO
ALTER TABLE [dbo].[purchase_return_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_purchase_return_records_tbl_purchase_returns_tbl] FOREIGN KEY([return_id_fk])
REFERENCES [dbo].[purchase_returns_tbl] ([return_id_pk])
GO
ALTER TABLE [dbo].[purchase_return_records_tbl] CHECK CONSTRAINT [FK_purchase_return_records_tbl_purchase_returns_tbl]
GO
ALTER TABLE [dbo].[purchase_returns_tbl]  WITH CHECK ADD  CONSTRAINT [FK_purchase_returns_tbl_purchase_returns_tbl] FOREIGN KEY([supplier_id_fk])
REFERENCES [dbo].[supplier_tbl] ([supplier_id_pk])
GO
ALTER TABLE [dbo].[purchase_returns_tbl] CHECK CONSTRAINT [FK_purchase_returns_tbl_purchase_returns_tbl]
GO
ALTER TABLE [dbo].[purchase_tbl]  WITH CHECK ADD  CONSTRAINT [FK_purchase_tbl_supplier_tbl] FOREIGN KEY([supplier_id_fk])
REFERENCES [dbo].[supplier_tbl] ([supplier_id_pk])
GO
ALTER TABLE [dbo].[purchase_tbl] CHECK CONSTRAINT [FK_purchase_tbl_supplier_tbl]
GO
ALTER TABLE [dbo].[quotation_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_quotation_records_tbl_product_tbl] FOREIGN KEY([product_id_fk])
REFERENCES [dbo].[product_tbl] ([product_id_pk])
GO
ALTER TABLE [dbo].[quotation_records_tbl] CHECK CONSTRAINT [FK_quotation_records_tbl_product_tbl]
GO
ALTER TABLE [dbo].[quotation_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_quotation_records_tbl_quotation_tbl] FOREIGN KEY([quotation_id_fk])
REFERENCES [dbo].[quotation_tbl] ([quotation_id_pk])
GO
ALTER TABLE [dbo].[quotation_records_tbl] CHECK CONSTRAINT [FK_quotation_records_tbl_quotation_tbl]
GO
ALTER TABLE [dbo].[quotation_tbl]  WITH CHECK ADD  CONSTRAINT [FK_quotation_tbl_customer_tbl] FOREIGN KEY([customer_id_fk])
REFERENCES [dbo].[customer_tbl] ([customer_id_pk])
GO
ALTER TABLE [dbo].[quotation_tbl] CHECK CONSTRAINT [FK_quotation_tbl_customer_tbl]
GO
ALTER TABLE [dbo].[quotation_temp_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_quotation_temp_records_tbl_product_tbl] FOREIGN KEY([product_id_fk])
REFERENCES [dbo].[product_tbl] ([product_id_pk])
GO
ALTER TABLE [dbo].[quotation_temp_records_tbl] CHECK CONSTRAINT [FK_quotation_temp_records_tbl_product_tbl]
GO
ALTER TABLE [dbo].[quotation_temp_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_quotation_temp_records_tbl_quotation_temp_tbl] FOREIGN KEY([template_id_fk])
REFERENCES [dbo].[quotation_temp_tbl] ([template_id_pk])
GO
ALTER TABLE [dbo].[quotation_temp_records_tbl] CHECK CONSTRAINT [FK_quotation_temp_records_tbl_quotation_temp_tbl]
GO
ALTER TABLE [dbo].[role_features_tbl]  WITH CHECK ADD  CONSTRAINT [FK_role_features_tbl_features_tbl] FOREIGN KEY([feature_id_fk])
REFERENCES [dbo].[features_tbl] ([feature_id_pk])
GO
ALTER TABLE [dbo].[role_features_tbl] CHECK CONSTRAINT [FK_role_features_tbl_features_tbl]
GO
ALTER TABLE [dbo].[role_features_tbl]  WITH CHECK ADD  CONSTRAINT [FK_role_features_tbl_roles_tbl] FOREIGN KEY([role_id_fk])
REFERENCES [dbo].[roles_tbl] ([role_id_pk])
GO
ALTER TABLE [dbo].[role_features_tbl] CHECK CONSTRAINT [FK_role_features_tbl_roles_tbl]
GO
ALTER TABLE [dbo].[sales_payments_tbl]  WITH CHECK ADD  CONSTRAINT [FK_sales_payments_tbl_sales_tbl] FOREIGN KEY([sales_id_fk])
REFERENCES [dbo].[sales_tbl] ([sales_id_pk])
GO
ALTER TABLE [dbo].[sales_payments_tbl] CHECK CONSTRAINT [FK_sales_payments_tbl_sales_tbl]
GO
ALTER TABLE [dbo].[sales_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_sales_records_tbl_product_tbl] FOREIGN KEY([product_id_fk])
REFERENCES [dbo].[product_tbl] ([product_id_pk])
GO
ALTER TABLE [dbo].[sales_records_tbl] CHECK CONSTRAINT [FK_sales_records_tbl_product_tbl]
GO
ALTER TABLE [dbo].[sales_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_sales_records_tbl_sales_tbl] FOREIGN KEY([sales_id_fk])
REFERENCES [dbo].[sales_tbl] ([sales_id_pk])
GO
ALTER TABLE [dbo].[sales_records_tbl] CHECK CONSTRAINT [FK_sales_records_tbl_sales_tbl]
GO
ALTER TABLE [dbo].[sales_return_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_sales_return_records_tbl_batch_tbl] FOREIGN KEY([batch_id_fk])
REFERENCES [dbo].[batch_tbl] ([batch_id_pk])
GO
ALTER TABLE [dbo].[sales_return_records_tbl] CHECK CONSTRAINT [FK_sales_return_records_tbl_batch_tbl]
GO
ALTER TABLE [dbo].[sales_return_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_sales_return_records_tbl_product_tbl] FOREIGN KEY([product_id_fk])
REFERENCES [dbo].[product_tbl] ([product_id_pk])
GO
ALTER TABLE [dbo].[sales_return_records_tbl] CHECK CONSTRAINT [FK_sales_return_records_tbl_product_tbl]
GO
ALTER TABLE [dbo].[sales_return_records_tbl]  WITH CHECK ADD  CONSTRAINT [FK_sales_return_records_tbl_sales_returns_tbl] FOREIGN KEY([return_id_fk])
REFERENCES [dbo].[sales_returns_tbl] ([return_id_pk])
GO
ALTER TABLE [dbo].[sales_return_records_tbl] CHECK CONSTRAINT [FK_sales_return_records_tbl_sales_returns_tbl]
GO
ALTER TABLE [dbo].[sales_returns_tbl]  WITH CHECK ADD  CONSTRAINT [FK_sales_returns_tbl_customer_tbl] FOREIGN KEY([customer_id_fk])
REFERENCES [dbo].[customer_tbl] ([customer_id_pk])
GO
ALTER TABLE [dbo].[sales_returns_tbl] CHECK CONSTRAINT [FK_sales_returns_tbl_customer_tbl]
GO
ALTER TABLE [dbo].[sales_tbl]  WITH CHECK ADD  CONSTRAINT [FK_sales_tbl_customer_tbl] FOREIGN KEY([customer_id_fk])
REFERENCES [dbo].[customer_tbl] ([customer_id_pk])
GO
ALTER TABLE [dbo].[sales_tbl] CHECK CONSTRAINT [FK_sales_tbl_customer_tbl]
GO
ALTER TABLE [dbo].[users_tbl]  WITH CHECK ADD  CONSTRAINT [FK_users_tbl_roles_tbl] FOREIGN KEY([role_id_fk])
REFERENCES [dbo].[roles_tbl] ([role_id_pk])
GO
ALTER TABLE [dbo].[users_tbl] CHECK CONSTRAINT [FK_users_tbl_roles_tbl]
GO
