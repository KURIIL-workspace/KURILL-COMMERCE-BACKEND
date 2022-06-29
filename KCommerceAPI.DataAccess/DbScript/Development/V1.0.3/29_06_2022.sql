CREATE TABLE IF NOT EXISTS settings.document_ref
(
    id smallint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 32767 CACHE 1 ),
    type character varying(50) COLLATE pg_catalog."default" NOT NULL,
    format character varying(100) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT document_ref_pk PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS settings.document_ref
    OWNER to postgres;


    insert into settings.document_ref(type,format) values('EMPLOYEE','EMP-@@NO@@/4');

alter table person.employee_login drop column password;
alter table person.employee_login add column password varchar(200);