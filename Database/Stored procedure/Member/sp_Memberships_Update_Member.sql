delimiter ;
delimiter //
CREATE PROCEDURE sp_Memberships_Update_Member(
	MemName NVARCHAR(100),
    MemIsAvailable TINYINT(1), 
    MemEmail NVARCHAR(100),	
    MemPhoneNumber NVARCHAR(100),
    MemAddress NVARCHAR(1024),
    MemID INTEGER)
	BEGIN		
		UPDATE Product.Member 
        SET MemberName = MemName, IsAvailable = MemIsAvailable, Email = MemEmail, 
			PhoneNumber = MemPhoneNumber, Address = MemAddress, UpdatedDate = SYSDATE()
        WHERE MemberID = MemID; 
    END;