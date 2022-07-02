Alter table purchase.purchase_invoice add column invoice_date timestamp without time zone;
Alter table purchase.purchase_invoice add column purchase_order_id uuid;
Alter table purchase.purchase_invoice add CONSTRAINT purchase_invoice_fk_4 FOREIGN KEY (purchase_order_id)
        REFERENCES purchase.purchase_order (id);
Alter table purchase.purchase_invoice add column total_item_qty int;
Alter table purchase.purchase_invoice add column total_amount int;

---------Insert
insert into settings.purchase_invoice_status(id,name) values(1,'Pending'),(2,'Rejected');