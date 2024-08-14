----------------- Database Schematic -----------------
CREATE DATABASE projectplmdb;
GO

USE projectplmdb;
GO

----------------- Create Tables -----------------
CREATE TABLE person (
	person_id INT NOT NULL,
	[name] VARCHAR(30) NOT NULL,
	last_name VARCHAR(30) NOT NULL,
	second_lastname VARCHAR(30) NOT NULL,
	[address] VARCHAR(300) NOT NULL,
	birthday DATE NOT NULL,
	phone_number INT NOT NULL,

	CONSTRAINT[PK_person_id] PRIMARY KEY(person_id),
	CONSTRAINT[UK_phone_number] UNIQUE(phone_number)
);
GO

CREATE TABLE [role] (
	role_id TINYINT NOT NULL IDENTITY(1,1),
	[description] VARCHAR(50) NOT NULL,

	CONSTRAINT[PK_role_id] PRIMARY KEY(role_id)
);
GO

CREATE TABLE user_employee (
	user_employee_id INT NOT NULL IDENTITY(1,1),
	[password] VARCHAR(300) NOT NULL,
	email VARCHAR(150) NOT NULL,
	active BIT NOT NULL,
	role_id TINYINT NOT NULL,
	person_id INT NOT NULL,

	CONSTRAINT[PK_user_employee_id] PRIMARY KEY(user_employee_id),
	CONSTRAINT[UK__user_employee_email] UNIQUE(email),
	CONSTRAINT[FK_user_employee_role_id] FOREIGN KEY(role_id) REFERENCES [role], 
	CONSTRAINT[FK_user_employee_person_id] FOREIGN KEY(person_id) REFERENCES person
);
GO

CREATE TABLE category_idea (
	category_idea_id TINYINT NOT NULL IDENTITY(1,1),
	[description] VARCHAR(50) NOT NULL,

	CONSTRAINT[PK_category_idea_id] PRIMARY KEY(category_idea_id)
);
GO

CREATE TABLE idea (
	idea_id INT NOT NULL IDENTITY(1,1),
	[name] VARCHAR(100) NOT NULL,
	[description] VARCHAR(300) NOT NULL,
	date_creation DATE NOT NULL,
	[status] CHAR(1) NOT NULL,
	user_employee_id INT NOT NULL,
	category_idea_id TINYINT NOT NULL,

	CONSTRAINT[idea_status] CHECK ([status] IN ('A','R','P')), -- A = Aceptado.  R = Rechazado.  P = Pendiente.
	CONSTRAINT[PK_idea_id] PRIMARY KEY(idea_id),
	CONSTRAINT[UK_idea_name] UNIQUE([name]),
	CONSTRAINT[FK_idea_user_employee_id] FOREIGN KEY(user_employee_id) REFERENCES user_employee, 
	CONSTRAINT[FK_idea_category_idea_id] FOREIGN KEY(category_idea_id) REFERENCES category_idea
);
GO

CREATE TABLE product_proposal (
	product_proposal_id INT NOT NULL IDENTITY(1,1),
	date_creation DATE NOT NULL,
	[status] CHAR(1) NOT NULL,
	file_path VARCHAR(300) NOT NULL,
	idea_id INT NOT NULL,
	user_employee_id INT NOT NULL,

	CONSTRAINT[product_proposal_status] CHECK ([status] IN ('A','R','P')), -- A = Aceptado.  R = Rechazado.  P = Pendiente.
	CONSTRAINT[PK_product_proposal_id] PRIMARY KEY(product_proposal_id),
	CONSTRAINT[FK_product_proposal_idea_id] FOREIGN KEY(idea_id) REFERENCES idea, 
	CONSTRAINT[FK_product_proposal_user_employee_id] FOREIGN KEY(user_employee_id) REFERENCES user_employee
);
GO

CREATE TABLE review_product_proposal (
	review_product_proposal_id INT NOT NULL IDENTITY(1,1),
	evaluation_date DATE NOT NULL,
	justification VARCHAR(300) NOT NULL,
	product_proposal_id INT NOT NULL,
	user_employee_id INT NOT NULL,

	CONSTRAINT[PK_review_product_proposal_id] PRIMARY KEY(review_product_proposal_id),
	CONSTRAINT[FK_review_product_proposal_product_proposal_id] FOREIGN KEY(product_proposal_id) REFERENCES product_proposal, 
	CONSTRAINT[FK_review_product_proposal_user_employee_id] FOREIGN KEY(user_employee_id) REFERENCES user_employee
);
GO

CREATE TABLE design (
	design_id INT NOT NULL IDENTITY(1,1),
	[name] VARCHAR(100) NOT NULL,
	[status] CHAR(1) NOT NULL,
	current_desing_path VARCHAR(300) NOT NULL,
	last_modification DATE NOT NULL,
	review_product_proposal_id INT NOT NULL,
	user_employee_id INT NOT NULL,

	CONSTRAINT[design_status] CHECK ([status] IN ('A','R','P')), -- A = Aceptado.  R = Rechazado.  P = Pendiente.
	CONSTRAINT[PK_design_id] PRIMARY KEY(design_id),
	CONSTRAINT[UK_design_name] UNIQUE([name]),
	CONSTRAINT[FK_design_review_product_proposal_id] FOREIGN KEY(review_product_proposal_id) REFERENCES review_product_proposal, 
	CONSTRAINT[FK_design_user_employee_id] FOREIGN KEY(user_employee_id) REFERENCES user_employee
);
GO

CREATE TABLE design_comment(
	design_comment_id INT NOT NULL IDENTITY(1,1),
	[description] VARCHAR(300) NOT NULL,
	user_employee_id INT NOT NULL,
	design_id INT NOT NULL,

	CONSTRAINT[PK_design_comment_id] PRIMARY KEY(design_comment_id),
	CONSTRAINT[FK_design_comment_user_employee_id] FOREIGN KEY(user_employee_id) REFERENCES user_employee, 
	CONSTRAINT[FK_design_comment_design_id] FOREIGN KEY(design_id) REFERENCES design
);
GO

CREATE TABLE design_history(
	design_history_id INT NOT NULL IDENTITY(1,1),
	design_path VARCHAR(300) NOT NULL,
	upload_date DATE NOT NULL,
	design_id INT NOT NULL,
	user_employee_id INT NOT NULL,

	CONSTRAINT[PK_design_history_id] PRIMARY KEY(design_history_id),
	CONSTRAINT[FK_design_history_design_id] FOREIGN KEY(design_id) REFERENCES design,
	CONSTRAINT[FK_design_history_user_employee_id] FOREIGN KEY(user_employee_id) REFERENCES user_employee
);
GO

CREATE TABLE review_design(
	review_design_id INT NOT NULL IDENTITY(1,1),
	evaluation_date DATE NOT NULL,
	justification VARCHAR(300) NOT NULL,
	design_id INT NOT NULL,
	user_employee_id INT NOT NULL,

	CONSTRAINT[PK_review_design_review_desing_id] PRIMARY KEY(review_design_id),
	CONSTRAINT[FK_review_design_design_id] FOREIGN KEY(design_id) REFERENCES design,
	CONSTRAINT[FK_review_design_user_employee_id] FOREIGN KEY(user_employee_id) REFERENCES user_employee
);
GO

----------------- Configure Security -----------------
CREATE LOGIN anonymous_login
WITH PASSWORD = '11723';
GO

CREATE USER anonymous_db
FOR LOGIN anonymous_login;
GO

EXEC sp_addrolemember 'db_owner', 'anonymous_db';