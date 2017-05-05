CREATE TABLE public.logs
(
   id bigserial, 
   "timestamp" timestamp with time zone, 
   level character varying(15), 
   message text, 
   exception text, 
   properties json, 
   CONSTRAINT logs_pkey PRIMARY KEY (id)
);