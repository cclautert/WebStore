/*CREATE DATABASE WebStore*/

USE [WebStore]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

 Create table Customer(          
    Id uniqueidentifier NOT NULL,
    FirstName varchar(50) NOT NULL,
    LastName varchar(50) NOT NULL,
    Email varchar(30) NOT NULL,
    Address varchar(220) NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO
SET ANSI_PADDING OFF
GO

USE [WebStore]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

 Create table User_Authentication(          
    Id uniqueidentifier NOT NULL,
    Name varchar(50) NOT NULL,
    Email varchar(30) NOT NULL,
    PasswordHash varbinary(max) NOT NULL,
    PasswordSalt varbinary(max) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO
SET ANSI_PADDING OFF
GO

USE [WebStore]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
 Create table Product(          
    Id uniqueidentifier NOT NULL,    
    Name varchar(50) NOT NULL,
    Description varchar(50) NOT NULL,
    Value decimal (18,2)  NOT NULL,
    DateRegister DATETIME2 (7),
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO
SET ANSI_PADDING OFF
GO

/****** Object:  StoredProcedure [dbo].[spAddCustomer]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spAddCustomer
(
    @Id uniqueidentifier,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(30),
    @Address VARCHAR(220)          
)          
as           
Begin           
    Insert into Customer (Id,FirstName,LastName,Email,Address)           
    Values (@Id,@FirstName,@LastName,@Email,@Address)           
End 
GO

/****** Object:  StoredProcedure [dbo].[spAddUser]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spAddUser
(
    @Id uniqueidentifier,
    @Name VARCHAR(50),
    @Email VARCHAR(30),
    @PasswordHash varbinary(max),
    @PasswordSalt varbinary(max)
)          
as           
Begin           
    Insert into User_Authentication (Id,Name,Email,PasswordHash,PasswordSalt)           
    Values (@Id,@Name,@Email,@PasswordHash,@PasswordSalt)           
End 
GO

/****** Object:  StoredProcedure [dbo].[spAddProduct]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spAddProduct          
(
    @Id uniqueidentifier,
    @Name VARCHAR(50),
    @Description VARCHAR(50),
    @Value DECIMAL(18,2),
    @DateRegister DATETIME2(7)          
)          
as           
Begin           
    Insert into Product (Id,Name,Description,Value,DateRegister)           
    Values (@Id,@Name,@Description,@Value,@DateRegister)           
End
GO

/****** Object:  StoredProcedure [dbo].[spUpdateCustomer]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spUpdateCustomer
(   
    @Id uniqueidentifier ,       
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(30),
    @Address VARCHAR(220)          
)          
as           
Begin           
   Update Customer
   set FirstName=@FirstName,            
   LastName=@LastName,            
   Email=@Email,
   Address=@Address            
   where Id=@Id
End 
GO

/****** Object:  StoredProcedure [dbo].[spUpdateUser]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spUpdateUser
(   
    @Id uniqueidentifier,
    @Name VARCHAR(50),
    @Email VARCHAR(30),
    @PasswordHash varbinary(max),
    @PasswordSalt varbinary(max)
)          
as           
Begin           
   Update User_Authentication
   set Name=@Name,
   Email=@Email,
   PasswordHash=@PasswordHash,
   PasswordSalt=@PasswordSalt          
   where Id=@Id           
End 
GO

/****** Object:  StoredProcedure [dbo].[spUpdateProduct]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spUpdateProduct          
(
    @Id uniqueidentifier,
    @Name VARCHAR(50),
    @Description VARCHAR(50),
    @Value DECIMAL(18,2),
    @DateRegister DATETIME2(7)          
)          
as           
Begin           
   Update Product
   set
   Name=@Name,
   Description=@Description,
   Value=@Value
   where Id=@Id
End
GO

/****** Object:  StoredProcedure [dbo].[spDeleteCustomer]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spDeleteCustomer
(            
   @Id uniqueidentifier            
)            
as             
begin            
   Delete from Customer where Id=@Id            
End
GO

/****** Object:  StoredProcedure [dbo].[spDeleteUser]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spDeleteUser
(            
   @Id uniqueidentifier            
)            
as             
begin            
   Delete from User_Authentication where Id=@Id            
End
GO

/****** Object:  StoredProcedure [dbo].[spDeleteProduct]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spDeleteProduct           
(            
   @Id uniqueidentifier            
)            
as             
begin            
   Delete from Product where Id=@Id            
End
GO

/****** Object:  StoredProcedure [dbo].[spGetAllCustomer]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spGetAllCustomer
as        
Begin        
    select *        
    from Customer     
    order by Id   
End
GO

/****** Object:  StoredProcedure [dbo].[spGetAllUser]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spGetAllUser
as        
Begin        
    select *        
    from User_Authentication     
    order by Id   
End
GO

/****** Object:  StoredProcedure [dbo].[spGetAllProducts]    Script Date: 16/06/2024 2:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure spGetAllProducts        
as        
Begin        
    select *        
    from Product     
    order by Id   
End
GO