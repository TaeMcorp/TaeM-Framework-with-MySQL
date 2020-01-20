delimiter ;
delimiter //
CREATE PROCEDURE sp_Memberships_Select_MemberHistory_By_Sequence(Seq INTEGER)
    BEGIN 
		SELECT Sequence, MemberID, MemberName, Successful, Message, InsertedDate
        FROM Product.MemberHistory 
		WHERE Sequence = Seq; 
    END;