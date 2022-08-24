SET SERVEROUTPUT ON
CREATE OR REPLACE PROCEDURE TableField (user in VARCHAR2) is
begin 
DECLARE

   TYPE TABL IS TABLE OF sys.all_tables.table_name%TYPE  INDEX BY BINARY_INTEGER;
     tab TABL;
  
   TYPE TABL1 IS TABLE OF user_tab_columns.column_name%TYPE  INDEX BY BINARY_INTEGER;
     tab1 TABL1;
     emtyTab1 TABL1;
   
   id INTEGER;
   idTable INTEGER;
   NAME VARCHAR2(20);
   CURSOR cr IS SELECT table_name FROM sys.all_tables where sys.all_tables.owner in (user)  ;
   
BEGIN 

  open cr ;
  fetch cr BULK COLLECT into tab ;  
  close cr;
  
  for i in 1..tab.count LOOP
  
 DBMS_OUTPUT.PUT_LINE('les columns de  table '||tab(i)||' sont :'  );

   INSERT INTO "C##INVOLYS"."data_source_tables" ( "Name") VALUES (tab(i));
   select "data_source_tables"."Id" into idTable from "data_source_tables" where "data_source_tables"."Name"=tab(i);
   SELECT  column_name  BULK COLLECT into tab1 FROM all_TAB_COLUMNS WHERE all_TAB_COLUMNS.owner in (user) and table_name = tab(i);
     
 for j in 1..tab1.count LOOP
  
 DBMS_OUTPUT.PUT_LINE(tab1(j));
  
 SELECT COUNT(DISTINCT  ac.constraint_type)  into id  
    FROM all_cons_columns acc, all_constraints ac
    WHERE acc.constraint_name = ac.constraint_name
         and acc.table_name =  ac.table_name 
         and CONSTRAINT_TYPE IN ('P', 'R') and acc.owner  in (user) and ac.table_name=tab(i)  
         and acc.column_name=tab1(j) ;
 
if(id=1) then 
    SELECT DISTINCT  ac.constraint_type  into name  
    FROM all_cons_columns acc, all_constraints ac
    WHERE acc.constraint_name = ac.constraint_name
    and acc.table_name =  ac.table_name 
    and CONSTRAINT_TYPE IN ('P', 'R') and acc.owner  in (user) and ac.table_name=tab(i)  
    and acc.column_name=tab1(j) ;
else 
name:='simpleFiled';
end if;

if(name='P') then 
  INSERT INTO "C##INVOLYS"."data_source_table_fields" ( "Designation","Name","TableId","IsList") VALUES ( 'P',tab1(j),idTable,0);
  DBMS_OUTPUT.PUT_LINE(name);
elsif(name='R') then 
  INSERT INTO "C##INVOLYS"."data_source_table_fields" ( "Designation","Name","TableId","IsList") VALUES ( 'R',tab1(j),idTable,0);
  DBMS_OUTPUT.PUT_LINE(name);
else  
 INSERT INTO "C##INVOLYS"."data_source_table_fields" ( "Name","TableId","IsList") VALUES ( tab1(j),idTable,0);
  DBMS_OUTPUT.PUT_LINE(name);
end if;
 
END LOOP;
   tab1 := emtytab1;   
END LOOP;
     EXCEPTION
      WHEN NO_DATA_FOUND THEN
        name := NULL;
   END;
END;

--EXECUTE TableField('C##INVOLYS')