delimiter ;
delimiter //
CREATE PROCEDURE sp_Memberships_Select_Num_Of_Members_By_MemberName(MemName NVARCHAR(100))
	BEGIN		
		SELECT COUNT(*) 
        FROM Product.Member 
        WHERE MemberName like MemName; 
    END;