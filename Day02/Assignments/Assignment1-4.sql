---- ####  Day2 Assignment-1 #####

SELECT
	p.[FullName] AS ProviderName,
	d.Name AS Department,
	COUNT(*) AS Encounters,
	RANK() OVER (ORDER BY COUNT(*) DESC) AS ProviderRank
FROM Encounter e
JOIN Provider p
	ON p.ProviderId = e.ProviderId
JOIN Department d
    ON d.DepartmentId = e.DepartmentId
GROUP BY 
	p.FullName,
	d.Name
ORDER BY Encounters DESC;


---- ####  Day2 Assignment-2 #####

ALTER TABLE Insurance
ADD
    ValidFrom DATETIME2
        GENERATED ALWAYS AS ROW START HIDDEN
        CONSTRAINT DF_Insurance_From
        DEFAULT SYSUTCDATETIME(),

    ValidTo DATETIME2
        GENERATED ALWAYS AS ROW END HIDDEN
        CONSTRAINT DF_Insurance_To
        DEFAULT '9999-12-31 23:59:59.9999999',

    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo);

ALTER TABLE Insurance
SET (
    SYSTEM_VERSIONING = ON
    (
        HISTORY_TABLE = dbo.Insurance_History
    )
);

UPDATE Insurance
SET payer = 'SBI'
WHERE PatientId = 10;

SELECT
    i.InsuranceId,
    i.Payer,
    i.PolicyNumber,
    i.ValidFrom,
    i.ValidTo
FROM Insurance FOR SYSTEM_TIME ALL AS i
WHERE i.PatientId = 10
ORDER BY i.ValidFrom DESC;


---- ####  Day2 Assignment-3 #####

CREATE VIEW vw_BillingClaims
AS
SELECT 
	c.ClaimId,
	e.PatientId,
	c.Status AS ClaimStatus,
	c.BilledAmount,
	c.ReimbursedAmt AS ReimbursedAmount
FROM Claim AS c
JOIN Encounter AS e
ON e.EncounterId = c.EncounterId;


CREATE PROCEDURE GetMonthlyClaimSummary
AS
BEGIN
    SELECT
        ClaimStatus,
        COUNT(*) AS TotalClaims,
        SUM(BilledAmount) AS TotalBilledAmount,
        SUM(ReimbursedAmount) AS TotalReimbursedAmount,
        SUM(BilledAmount - ReimbursedAmount) AS OutstandingAmount,

        RANK() OVER (
            ORDER BY SUM(BilledAmount - ReimbursedAmount) DESC
        ) AS LossRank

    FROM vw_BillingClaims

    GROUP BY ClaimStatus

    ORDER BY OutstandingAmount DESC;
END;

EXEC GetMonthlyClaimSummary;


---- ####  Day2 Assignment-4 #####

SELECT
    -- 1. Total Active Patients
    (SELECT COUNT(*) 
     FROM Patient 
     WHERE IsActive = 1) AS TotalActivePatients,

    -- 2. Denied Claims
    (SELECT COUNT(*) 
     FROM Claim 
     WHERE Status = 'Denied') AS DeniedClaims,

    -- 3. Top 5 Departments by Encounters
    d.Name AS DepartmentName,
    COUNT(e.EncounterId) AS TotalEncounters

FROM Encounter e
JOIN Department d
    ON d.DepartmentId = e.DepartmentId

GROUP BY d.Name

ORDER BY TotalEncounters DESC
OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY;

