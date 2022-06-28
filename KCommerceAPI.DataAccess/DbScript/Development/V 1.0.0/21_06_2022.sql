create schema purchase;
create schema settings;
create table settings.purchase_order_status(
    id smallint not null,
    name varchar(20),
    description varchar(50),
    constraint purchase_order_status_pk primary key (id)
);
create table purchase.purchase_order(
    id uuid not null,
    status_id smallint,
    total_price numeric(28,2),
    created_date_time timestamp without time zone NOT NULL DEFAULT (now())::timestamp without time zone,
    updated_date_time timestamp without time zone,
    constraint purchase_order_pk primary key (id),
    constraint purchase_order_fk FOREIGN KEY (status_id)
    REFERENCES settings.purchase_order_status (id)
);