

create table purchase.purchase_order_item(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    product_name varchar(20),
    qty int,
    unit_price numeric(28,2),
    purchase_order_id uuid,
    constraint purchase_order_item_pk primary key(id),
    constraint purchase_order_item_fk foreign key(purchase_order_id)
    references purchase.purchase_order(id)
);

create table settings.purchase_invoice_status(
    id smallint not null,
    name varchar(20),
    description varchar(50),
    constraint purchase_invoice_status_pk primary key(id)
);

create table person.supplier(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    supplier_name varchar(20),
    supplier_contact varchar(10),
    constraint supplier_pk primary key(id)
);
create table settings.employee_status(
    id smallint not null,
    name varchar(20),
    description varchar(50),
    constraint employee_status_pk primary key (id)
);

alter table person.employee add column contact varchar(10);
alter table person.employee add column employee_status smallint;
alter table person.employee add constraint employee_fk foreign key (employee_status)
references settings.employee_status(id);
-- create table person.employee(
--     id uuid not null,
--     employee_name varchar(20),
--     employee_contact varchar(10),
--     employee_status smallint,
--     constraint employee_pk primary key (id),
--     constraint employee_fk foreign key (employee_status)
--     references settings.employee_status(id)
-- );

create table person.customer(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    customer_name varchar(20),
    customer_contact varchar(10),
    constraint customer_pk primary key(id)
);

create table purchase.purchase_invoice(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    created_date_time timestamp without time zone NOT NULL DEFAULT (now())::timestamp without time zone,
    updated_date_time timestamp without time zone,
    status_id smallint,
    supplier_id uuid,
    prepared_employee uuid,
    constraint purchase_invoice_pk primary key(id),
    constraint purchase_invoice_fk_1 foreign key(status_id)
    references settings.purchase_invoice_status(id),
    constraint purchase_invoice_fk_2 foreign key (supplier_id)
    references person.supplier(id),
    constraint purchase_invoice_fk_3 foreign key (prepared_employee)
    references person.employee(id)
);

create schema contact;

create table contact.address(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    add_line_1 varchar (20),
    add_line_2 varchar (20),
    postal_code int,
    country varchar(10),
    supplier uuid,
    employee uuid,
    customer uuid,
    constraint address_pk primary key (id),
    constraint address_fk_1 foreign key (supplier)
    references person.supplier(id),
    constraint address_fk_2 foreign key (employee)
    references person.employee(id),
    constraint address_fk_3 foreign key (customer)
    references person.customer(id)
);

create schema category;

create table category.category(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    category_name varchar(20),
    brand_name varchar(20),
    constraint category_fk primary key(id)
);

create table purchase.purchase_invoice_item(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    category_id uuid,
    item_name varchar(20),
    item_quantity smallint,
    unit_price numeric(28,2),
    purchase_invoice_id uuid,
    constraint purchase_invoice_item_pk primary key(id),
    constraint purchase_invoice_item_fk_1 foreign key(category_id)
    references category.category(id),
    constraint purchase_invoice_item_fk_2 foreign key (purchase_invoice_id)
    references purchase.purchase_order_item(id)
);


-- create table settings.sales_order_status(
-- id smallint not null,
-- name varchar(30),
-- description varchar(50),
-- constraint sales_order_status_pk primary key(id)
-- );

create table public.stock_status(
id uuid NOT NULL DEFAULT uuid_generate_v4(),
name varchar(30),
description varchar(30),
constraint stock_status_pk primary key(id)
);

create table public.stock(
id uuid NOT NULL DEFAULT uuid_generate_v4(),
category_name varchar(20),
item_name varchar(20),
item_qty int,
unit_price numeric(20,2),
selling_price numeric(20,2),
emp_id uuid,
status_id uuid,
constraint stock_pk primary key(id),
constraint stock_fk_1 foreign key(emp_id) references person.employee(id),
constraint stock_fk_2 foreign key(status_id) references public.stock_status(id)

);

create schema sales;
create table sales.sales_order(
id uuid NOT NULL DEFAULT uuid_generate_v4(),
cus_id uuid,
cus_name varchar(20),
category_name varchar(20),
item_name varchar(20),
odr_qty int,
selling_price numeric(20,2),
total_price numeric(20,2),
status_id smallint,
status varchar(30),
stock_id uuid,
constraint sales_order_pk primary key(id),
constraint sales_order_fk_1 foreign key(cus_id) references person.customer(id),
constraint sales_order_fk_2 foreign key(stock_id) references public.stock(id)

);

create table sales.sales_order_items(
id smallint not null,
product_name varchar(30),
qty int,
unit_price numeric(20,2),
sales_order_id uuid,
constraint sales_order_items_pk primary key(id),
constraint sales_order_fk_1 foreign key(sales_order_id) references sales.sales_order(id)
);

create table purchase.goods_recieve_note(
id uuid NOT NULL DEFAULT uuid_generate_v4(),
purchase_order_id uuid,
checked_employee_id uuid,
checked_date date,
purchase_invoice_id uuid,
description varchar(30),
constraint goods_recieve_note_pk primary key(id),
constraint goods_recieve_note_fk_1 foreign key(purchase_order_id) references purchase.purchase_order(id),
constraint goods_recieve_note_fk_2 foreign key(checked_employee_id) references person.employee(id),
constraint goods_recieve_note_fk_3 foreign key(purchase_invoice_id) references purchase.purchase_invoice(id)

);

create table  purchase.goods_recieve_note_items(
id uuid NOT NULL DEFAULT uuid_generate_v4(),
goods_recieve_note_id uuid,
ordered_qty int,
recieved_qty int,
damaged_qty int,
other_deducted_qty int,
remaining_qty int,
description varchar(30),
created_date_time date,
updated_date_time date,
unit_price numeric(20,2),
constraint goods_recieve_note_items_pk primary key(id),
constraint goods_recieve_note_items_fk_1 foreign key(goods_recieve_note_id) references purchase.goods_recieve_note(id)

);

--QUERIES ADDED 


--ishkhan ahamed 

