Alter table purchase.purchase_order add column order_date timestamp without time zone;
Alter table purchase.purchase_order add column supplier_id uuid;
alter table purchase.purchase_order add CONSTRAINT purchase_order_fk_1 FOREIGN KEY (supplier_id)
        REFERENCES person.supplier (id);
Alter table purchase.purchase_order add column total_qty int;
Alter table purchase.purchase_order add column terms_condition varchar(500);
Alter table purchase.purchase_order add column ship_to_address varchar(100);
Alter table purchase.purchase_order add column description varchar(50);
Alter table purchase.purchase_order add column due_date timestamp without time zone;
Alter table purchase.purchase_order add column prepared_employee uuid;
alter table purchase.purchase_order add CONSTRAINT purchase_order_fk_2 FOREIGN KEY (prepared_employee)
        REFERENCES person.employee (id);

-------------------Edited
insert into settings.purchase_order_status(id,name) values(1,'Pending'),(2,'Rejected');