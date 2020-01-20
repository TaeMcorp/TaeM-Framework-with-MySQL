delimiter ;
delimiter //
CREATE PROCEDURE sp_Memberships_Insert_Member(
	MemName NVARCHAR(100), 
    MemIsAvailable TINYINT(1), 
    MemEmail NVARCHAR(100),	
    MemPhoneNumber NVARCHAR(100),
    MemAddress NVARCHAR(1024))
    
    BEGIN 
		INSERT INTO Product.Member 
		( MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate ) 
		VALUES 
		( MemName, MemIsAvailable, MemEmail, MemPhoneNumber, MemAddress, SYSDATE(), NULL ); 
			
		SELECT @InsertedMemberID := LAST_INSERT_ID();
    END;