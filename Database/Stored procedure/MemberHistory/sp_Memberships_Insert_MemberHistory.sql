delimiter ;
delimiter //
CREATE PROCEDURE sp_Memberships_Insert_MemberHistory(
	MemID INTEGER,
	MemName NVARCHAR(100), 
    MemSuccessful TINYINT(1), 
    MemMessage NVARCHAR(100))
    BEGIN 
		INSERT INTO Product.MemberHistory 
		( MemberID, MemberName, Successful, Message, InsertedDate ) 
		VALUES 
		( MemID, MemName, MemSuccessful, MemMessage, SYSDATE() ); 
		
        SELECT @InsertedSequence := LAST_INSERT_ID();
    END;