-- Table: public.rotas

-- DROP TABLE IF EXISTS public.rotas;

CREATE TABLE IF NOT EXISTS public.rotas
(
    id integer NOT NULL DEFAULT nextval('rotas_id_seq'::regclass),
    origem character varying COLLATE pg_catalog."default",
    destino character varying COLLATE pg_catalog."default",
    valor numeric
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.rotas
    OWNER to postgres;