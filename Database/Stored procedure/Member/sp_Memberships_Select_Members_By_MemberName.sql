delimiter ;
delimiter //
CREATE PROCEDURE sp_Memberships_Select_Members_By_MemberName(MemName NVARCHAR(100))
	BEGIN		
		SELECT MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate 
        FROM Product.Member
        WHERE MemberName like MemName; 
    END;