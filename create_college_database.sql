-- Creating Database
CREATE DATABASE College;
GO

-- Use the newly created database
USE College;
GO

-- Creating StudentRegistration Table
CREATE TABLE [dbo].[StudentRegistration](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[StudentFirstName] [varchar](100) NOT NULL,
	[StudentLastName] [varchar](50) NULL,
	[StudentDob] [date] NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[StudentFee] [int] NOT NULL,
	[is_active] [bit] NOT NULL,
	[created_by] [int] NULL,
	[created_date] [date] NULL,
	[modified_by] [int] NULL,
	[modified_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY];
GO

-- Inserting Data into StudentRegistration Table
INSERT INTO [dbo].[StudentRegistration] 
([StudentFirstName], [StudentLastName], [StudentDob], [Gender], [StudentFee], [is_active], [created_by], [created_date], [modified_by], [modified_date])
VALUES
('John', 'Doe', '2000-01-15', 'Male', 5000, 1, 1, '2025-04-14', NULL, NULL),
('Jane', 'Smith', '2001-02-20', 'Female', 4500, 1, 2, '2025-04-14', NULL, NULL),
('Tom', 'Brown', '1999-05-10', 'Male', 6000, 1, 3, '2025-04-14', NULL, NULL),
('Emily', 'Davis', '2002-08-25', 'Female', 5500, 1, 4, '2025-04-14', NULL, NULL),
('Michael', 'Wilson', '1998-12-30', 'Male', 7000, 1, 5, '2025-04-14', NULL, NULL),
('Sarah', 'Taylor', '2000-07-05', 'Female', 5200, 1, 6, '2025-04-14', NULL, NULL),
('David', 'Anderson', '2001-11-12', 'Male', 4800, 1, 7, '2025-04-14', NULL, NULL),
('Sophia', 'Thomas', '1999-03-17', 'Female', 6300, 1, 8, '2025-04-14', NULL, NULL),
('Daniel', 'Jackson', '2000-06-28', 'Male', 5600, 1, 9, '2025-04-14', NULL, NULL),
('Olivia', 'White', '2002-01-22', 'Female', 4900, 1, 10, '2025-04-14', NULL, NULL);
GO