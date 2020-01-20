use Product;

-- Member Table Creation Query
CREATE TABLE Member (  
	MemberID INTEGER NOT NULL AUTO_INCREMENT,
    MemberName NVARCHAR(100) NULL,
    IsAvailable TINYINT(1) NULL,
    Email NVARCHAR(100) NULL,
    PhoneNumber NVARCHAR(100) NULL,
    Address NVARCHAR(1024) NULL,
    InsertedDate DATETIME NULL,
    UpdatedDate DATETIME NULL,
    PRIMARY KEY (MemberID)
);

-- Drop table
-- DROP TABLE Member;

CREATE TABLE MemberHistory (
	Sequence INTEGER NOT NULL AUTO_INCREMENT,
    MemberID INTEGER NOT NULL,
    MemberName NVARCHAR(100) NULL,
    Successful TINYINT(1) NULL DEFAULT 0,
    Message NVARCHAR(1024) NULL,
    InsertedDate DATETIME NULL,
    PRIMARY KEY(Sequence)
);

-- Drop table
-- DROP TABLE MemberHistory;