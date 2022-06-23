create table purchase.purchase_order_item(
    id uuid not null,
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
    id uuid not null,
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
    id uuid not null,
    customer_name varchar(20),
    customer_contact varchar(10),
    constraint customer_pk primary key(id)
);

create table purchase.purchase_invoice(
    id uuid not null,
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
    id uuid not null,
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
    id uuid not null,
    category_name varchar(20),
    brand_name varchar(20),
    constraint category_fk primary key(id)
);

create table purchase.purchase_invoice_item(
    id uuid not null,
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

--ishkhan ahamed 
