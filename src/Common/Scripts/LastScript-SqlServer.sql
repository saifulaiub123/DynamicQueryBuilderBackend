
DECLARE @table nvarchar(500)
DECLARE @colu nvarchar(500)
DECLARE @sql nvarchar(520)
DECLARE @currentId TABLE (
                            
                            [data] nvarchar(500),
                            [id] int,
                            ARRAYINDEX int identity(1,1)
                         );  

DECLARE CursorSelect CURSOR FOR
    select table_name from INFORMATION_SCHEMA.tables where table_name not like 'sys%'

OPEN CursorSelect
FETCH NEXT FROM CursorSelect
INTO @table
DECLARE @TOTALCOUNTtab int

WHILE @@FETCH_STATUS = 0
BEGIN
    
    INSERT INTO data_source_tables(name) VALUES (@Table)
    SELECT @TOTALCOUNTtab = id from data_source_tables where name = @Table
    INSERT @currentId([data],id) SELECT COLUMN_NAME ,@TOTALCOUNTtab
    FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @Table

    FETCH NEXT FROM CursorSelect INTO @table   
END

CLOSE CursorSelect
DEALLOCATE CursorSelect

DECLARE @INDEXVAR int  
DECLARE @TOTALCOUNT int 
DECLARE @TABLEid int
DECLARE @typeColu nvarchar(500)

SET @INDEXVAR = 0  
SELECT @TOTALCOUNT= COUNT(*) FROM @currentId
WHILE @INDEXVAR < @TOTALCOUNT 

BEGIN  

    SELECT @INDEXVAR = @INDEXVAR + 1 
    SELECT @colu = data,@TABLEid = id from @currentId where ARRAYINDEX = @INDEXVAR
    PRINT @colu 
    SELECT @typeColu=tc.CONSTRAINT_TYPE  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
    JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS ccu ON ccu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME
    where ccu.COLUMN_NAME=@colu
    
    if @typeColu='PRIMARY KEY'
        insert into data_source_table_fields(Designation,name,IsList,TableId) values ('P',@colu,0,@TABLEid)
    if @typeColu='FOREIGN KEY'
        insert into data_source_table_fields(Designation, name,IsList,TableId) values ('F',@colu,0,@TABLEid)
    if @typeColu='normal column'
        insert into data_source_table_fields(name,IsList,TableId) values (@colu,0,@TABLEid)
    set @typeColu='normal column'
    
END
