-- Table: public.contcardpic

DROP TABLE public.contcardpic;

CREATE TABLE public.contcardpic
(
    "contcardpicid" bigint NOT NULL,
    "contcardid" bigint,
    "picdtm" date,
    "picname" character(250) COLLATE pg_catalog."default",
    "picdata" bytea
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.contcardpic
    OWNER to postgres;