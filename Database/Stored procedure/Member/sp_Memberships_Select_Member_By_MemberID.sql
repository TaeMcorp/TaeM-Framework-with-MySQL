delimiter ;
delimiter //
CREATE PROCEDURE sp_Memberships_Select_Member_By_MemberID(MemID INTEGER)
	BEGIN		
		SELECT MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate 
        FROM Product.Member
        WHERE MemberID = MemID;
    END;