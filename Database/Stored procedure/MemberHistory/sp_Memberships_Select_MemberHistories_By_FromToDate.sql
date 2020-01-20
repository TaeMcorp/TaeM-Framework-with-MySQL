delimiter ;
delimiter //
CREATE PROCEDURE sp_Memberships_Select_MemberHistories_By_FromToDate(
	FromDate DATETIME,
	ToDate DATETIME)
    BEGIN 
		SELECT Sequence, MemberID, MemberName, Successful, Message, InsertedDate
        FROM Product.MemberHistory 
		WHERE InsertedDate >= FromDate AND InsertedDate <= ToDate 
        ORDER BY InsertedDate DESC; 
    END;