delimiter ;
delimiter //
CREATE PROCEDURE sp_Memberships_Delete_Member_By_MemberID(MemID INTEGER)
	BEGIN
		DELETE FROM Product.Member
		WHERE MemberID = MemID; 
    END;