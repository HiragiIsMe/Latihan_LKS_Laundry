create table job (
	id_job int identity(1,1) primary key not null,
	name_job varchar(50) not null
);

create table employee (
	id_job int foreign key references job(id_job) on update cascade on delete cascade not null,
	id_employee int identity(1,1) primary key not null,
	password_employee varchar(255) not null,
	name_employee varchar(50) not null,
	email_employee  varchar(50) not null,
	addres_employee varchar(200) not null,
	phone_number_employee varchar(20) not null,
	date_of_birth_employee datetime not null,
	salary_employee money not null
);

create table customer (
	id_customer int identity(1,1) primary key not null,
	name_customer varchar(50) not null,
	phone_number_customer varchar(20) not null,
	address_customer varchar(200) not null
);

create table headertransaction (
	id_employee int foreign key references employee(id_employee) on update cascade on delete cascade not null,
	id_customer int foreign key references customer(id_customer) on update cascade on delete cascade not null,
	id_header_transaction int identity(1,1) primary key not null,
	transaction_date_time_header_transaction datetime not null,
	complete_estimation_date_time_header datetime
);

create table category (
	id_category int identity(1,1) primary key not null,
	name_category varchar(50) not null,
);

create table unit (
	id_unit int identity(1,1) primary key not null,
	name_unit varchar(50) not null,
);

create table service (
	id_category int foreign key references category(id_category) on update cascade on delete cascade not null,
	id_unit int foreign key references unit(id_unit) on update cascade on delete cascade not null,
	id_service int identity(1,1) primary key not null,
	name_service varchar(50) not null,
	price_unit_service int not null,
	estimation_duration_service int not null
);

create table package (
	id_package int identity(1,1) primary key not null,
	name_package varchar(100) not null,
	price_package int not null,
	description_package varchar(300) not null,
	duration_package int not null
);

create table detailpackage (
	id_service int foreign key references service(id_service) on update cascade on delete cascade not null,
	id_package int foreign key references package(id_package) on update cascade on delete cascade not null,
	id_detail_package int identity(1,1) primary key not null,
	total_unit_service_detail_package int not null
);

create table detailtransaction (
	id_service int foreign key references service(id_service) on update cascade on delete cascade,
	id_header_transaction int foreign key references headertransaction(id_header_transaction) on update cascade on delete cascade not null,
	id_detail_transaction int identity(1,1) primary key not null,
	id_package int foreign key references package(id_package) on update cascade on delete cascade,
	price_detail_transaction int not null,
	total_unit_detail_transaction float(10) not null,
	complete_datetime_detail_transaction datetime
);