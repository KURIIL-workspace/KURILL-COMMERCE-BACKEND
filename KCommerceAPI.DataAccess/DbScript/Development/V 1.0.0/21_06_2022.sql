
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

alter table person.employee_login drop constraint employee_login_fk_1;
alter table person.employee_login drop constraint employee_login_pkey;

alter table person.employee_login drop column id;
alter table person.employee_login add column id uuid NOT NULL DEFAULT uuid_generate_v4();
alter table person.employee_login add constraint employee_login_pkey PRIMARY KEY (id);

alter table person.employee drop constraint employee_pkey;
alter table person.employee drop column id;
alter table person.employee add column id uuid NOT NULL DEFAULT uuid_generate_v4();
alter table person.employee add constraint employee_pkey PRIMARY KEY (id);

alter table person.employee_login add CONSTRAINT employee_login_fk_1 FOREIGN KEY (employee_id)
        REFERENCES person.employee (id);

alter table person.employee drop column birth_date;
alter table person.employee add column updated_date_time timestamp without time zone;
------------------------------------------------------------------------------------------------
--pre sql  *************************************************************************************
------------------------------------------------------------------------------------------------

create schema purchase;
create schema settings;

create table settings.purchase_order_status(
    id smallint not null,
    name varchar(20),
    description varchar(50),
    constraint purchase_order_status_pk primary key (id)
);
create table purchase.purchase_order(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    status_id smallint,
    total_price numeric(28,2),
    created_date_time timestamp without time zone NOT NULL DEFAULT (now())::timestamp without time zone,
    updated_date_time timestamp without time zone,
    constraint purchase_order_pk primary key (id),
    constraint purchase_order_fk FOREIGN KEY (status_id)
    REFERENCES settings.purchase_order_status (id)
);