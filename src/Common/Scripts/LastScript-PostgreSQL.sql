
create or replace procedure script() language plpgsql
   as
   $$
declare 
-- variable declaration
tabname character varying(50);
type_constraint character varying(50);
tabcolumn varchar[];
emptytab varchar[];
id INTEGER;
idTable INTEGER;
f record;
g record;
  
 cr  CURSOR  for SELECT table_name FROM information_schema.tables
 WHERE table_schema='public'
 AND table_name!='__EFMigrationsHistory' AND table_type='BASE TABLE' ;


begin

id=1;

for f in SELECT table_name FROM information_schema.tables WHERE table_schema='public'
   AND table_name!='__EFMigrationsHistory' AND table_type='BASE TABLE'    loop 

    raise notice '%',f.table_name;
INSERT INTO "data_source_tables" ( "Name") VALUES ( f.table_name);
SELECT "data_source_tables"."Id" into idTable from "data_source_tables" where "data_source_tables"."Name"=f.table_name;
  
for g in  SELECT  column_name  FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = f.table_name loop
   
  SELECT  constraint_type into type_constraint FROM information_schema.table_constraints AS tc 
JOIN information_schema.key_column_usage AS kcu 
  ON tc.constraint_name = kcu.constraint_name 
JOIN information_schema.constraint_column_usage AS ccu 
  ON ccu.constraint_name = tc.constraint_name 
WHERE (constraint_type = 'FOREIGN KEY'  OR  constraint_type = 'PRIMARY KEY') and
    kcu.COLUMN_NAME=g.column_name;
  
   raise notice '%', g.column_name;
    
if type_constraint='PRIMARY KEY' then 
   
       raise notice '%', type_constraint ;
       INSERT INTO "data_source_table_fields" ( "Designation","Name","TableId","IsList") VALUES ('P', g.column_name,idTable,false);

elsif type_constraint='FOREIGN KEY' then 
   
       raise notice '%', type_constraint ;
       INSERT INTO "data_source_table_fields" ("Designation", "Name","TableId","IsList") VALUES ( 'F',g.column_name,idTable,false);

else 
  
       INSERT INTO "data_source_table_fields" ( "Name","TableId","IsList") VALUES ( g.column_name,idTable,false);
end if ;
end loop;
    id=id+1;
end loop;
end;
$$
