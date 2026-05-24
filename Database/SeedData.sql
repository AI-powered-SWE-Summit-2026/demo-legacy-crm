USE LegacyCRMDb;
GO

SET IDENTITY_INSERT dbo.Users ON;
INSERT INTO dbo.Users (Id, Username, PasswordHash, FullName, Email, Role, IsActive, LastLoginAt)
VALUES
(1, N'admin', N'admin123', N'System Administrator', N'admin@legacycrm.local', N'Administrator', 1, GETDATE()),
(2, N'salesrep', N'sales123', N'Sales Representative', N'salesrep@legacycrm.local', N'Sales', 1, DATEADD(DAY, -1, GETDATE()));
SET IDENTITY_INSERT dbo.Users OFF;
GO

SET IDENTITY_INSERT dbo.Customers ON;
INSERT INTO dbo.Customers (Id, Name, CompanyName, Email, Phone, Address, City, Country, Industry, Status, AssignedToUserId, CreatedAt, ModifiedAt, Notes)
VALUES
(1, N'Ava Thompson', N'Northwind Traders', N'ava@northwind.example', N'555-0101', N'100 Market Street', N'Seattle', N'USA', N'Retail', N'Active', 2, DATEADD(DAY, -90, GETDATE()), DATEADD(DAY, -2, GETDATE()), N'Existing strategic account.'),
(2, N'Liam Carter', N'Contoso Logistics', N'liam@contoso.example', N'555-0102', N'22 Harbor Way', N'Hamburg', N'Germany', N'Logistics', N'Prospect', 2, DATEADD(DAY, -75, GETDATE()), DATEADD(DAY, -4, GETDATE()), N'Interested in fleet visibility module.'),
(3, N'Olivia Chen', N'Fabrikam Health', N'olivia@fabrikam.example', N'555-0103', N'58 King Street', N'Toronto', N'Canada', N'Healthcare', N'Lead', 2, DATEADD(DAY, -60, GETDATE()), DATEADD(DAY, -10, GETDATE()), N'Inbound lead from webinar.'),
(4, N'Noah Patel', N'Blue Yonder Energy', N'noah@blueyonder.example', N'555-0104', N'17 Lake Drive', N'Austin', N'USA', N'Energy', N'Active', 1, DATEADD(DAY, -120, GETDATE()), DATEADD(DAY, -1, GETDATE()), N'Upsell discussions underway.'),
(5, N'Emma Garcia', N'Alpine Media', N'emma@alpine.example', N'555-0105', N'9 Sunset Blvd', N'Los Angeles', N'USA', N'Media', N'Inactive', 2, DATEADD(DAY, -200, GETDATE()), DATEADD(DAY, -90, GETDATE()), N'Paused due to budget freeze.'),
(6, N'William Kim', N'Cityline Banking', N'william@cityline.example', N'555-0106', N'41 Queen Square', N'London', N'UK', N'Finance', N'Prospect', 1, DATEADD(DAY, -40, GETDATE()), DATEADD(DAY, -7, GETDATE()), N'Security review scheduled.'),
(7, N'Sophia Rossi', N'Verde Manufacturing', N'sophia@verde.example', N'555-0107', N'16 Via Roma', N'Milan', N'Italy', N'Manufacturing', N'Lead', 2, DATEADD(DAY, -25, GETDATE()), DATEADD(DAY, -3, GETDATE()), N'Needs multilingual support.'),
(8, N'James Wilson', N'Pacific Education', N'james@pacificedu.example', N'555-0108', N'200 College Ave', N'San Diego', N'USA', N'Education', N'Active', 1, DATEADD(DAY, -150, GETDATE()), DATEADD(DAY, -5, GETDATE()), N'High ticket volume account.'),
(9, N'Isabella Novak', N'Nordic Retail Group', N'isabella@nordic.example', N'555-0109', N'89 Harbour Road', N'Copenhagen', N'Denmark', N'Retail', N'Prospect', 2, DATEADD(DAY, -15, GETDATE()), DATEADD(DAY, -2, GETDATE()), N'Pilot scope under review.'),
(10, N'Benjamin Lee', N'Orbit Telecom', N'benjamin@orbit.example', N'555-0110', N'5 Innovation Park', N'Singapore', N'Singapore', N'Telecom', N'Active', 1, DATEADD(DAY, -180, GETDATE()), DATEADD(DAY, -1, GETDATE()), N'Enterprise renewal due next quarter.');
SET IDENTITY_INSERT dbo.Customers OFF;
GO

SET IDENTITY_INSERT dbo.Contacts ON;
INSERT INTO dbo.Contacts (Id, CustomerId, FirstName, LastName, Email, Phone, Title, IsPrimary, CreatedAt)
VALUES
(1, 1, N'Ava', N'Thompson', N'ava@northwind.example', N'555-0101', N'Operations Director', 1, DATEADD(DAY, -90, GETDATE())),
(2, 1, N'Mark', N'Benson', N'mark@northwind.example', N'555-0111', N'IT Manager', 0, DATEADD(DAY, -85, GETDATE())),
(3, 2, N'Liam', N'Carter', N'liam@contoso.example', N'555-0102', N'VP Sales', 1, DATEADD(DAY, -75, GETDATE())),
(4, 2, N'Ella', N'Nguyen', N'ella@contoso.example', N'555-0112', N'Operations Lead', 0, DATEADD(DAY, -72, GETDATE())),
(5, 3, N'Olivia', N'Chen', N'olivia@fabrikam.example', N'555-0103', N'Practice Manager', 1, DATEADD(DAY, -60, GETDATE())),
(6, 4, N'Noah', N'Patel', N'noah@blueyonder.example', N'555-0104', N'Program Sponsor', 1, DATEADD(DAY, -120, GETDATE())),
(7, 4, N'Sara', N'Cole', N'sara@blueyonder.example', N'555-0113', N'Project Manager', 0, DATEADD(DAY, -118, GETDATE())),
(8, 5, N'Emma', N'Garcia', N'emma@alpine.example', N'555-0105', N'Publisher', 1, DATEADD(DAY, -200, GETDATE())),
(9, 6, N'William', N'Kim', N'william@cityline.example', N'555-0106', N'Compliance Lead', 1, DATEADD(DAY, -40, GETDATE())),
(10, 7, N'Sophia', N'Rossi', N'sophia@verde.example', N'555-0107', N'Plant Director', 1, DATEADD(DAY, -25, GETDATE())),
(11, 7, N'Luca', N'Moretti', N'luca@verde.example', N'555-0114', N'Procurement Manager', 0, DATEADD(DAY, -24, GETDATE())),
(12, 8, N'James', N'Wilson', N'james@pacificedu.example', N'555-0108', N'Dean of Operations', 1, DATEADD(DAY, -150, GETDATE())),
(13, 9, N'Isabella', N'Novak', N'isabella@nordic.example', N'555-0109', N'Retail Director', 1, DATEADD(DAY, -15, GETDATE())),
(14, 10, N'Benjamin', N'Lee', N'benjamin@orbit.example', N'555-0110', N'Account Executive', 1, DATEADD(DAY, -180, GETDATE())),
(15, 10, N'Rina', N'Park', N'rina@orbit.example', N'555-0115', N'Support Manager', 0, DATEADD(DAY, -175, GETDATE()));
SET IDENTITY_INSERT dbo.Contacts OFF;
GO

SET IDENTITY_INSERT dbo.Opportunities ON;
INSERT INTO dbo.Opportunities (Id, CustomerId, Title, Description, Value, Stage, ExpectedCloseDate, AssignedToUserId, Probability, CreatedAt, ModifiedAt)
VALUES
(1, 1, N'Northwind Renewal', N'Annual renewal and seat expansion.', 125000.00, N'Negotiation', DATEADD(DAY, 30, GETDATE()), 2, 80, DATEADD(DAY, -20, GETDATE()), DATEADD(DAY, -1, GETDATE())),
(2, 2, N'Contoso Fleet Visibility', N'Initial CRM rollout for logistics team.', 68000.00, N'Proposal', DATEADD(DAY, 45, GETDATE()), 2, 60, DATEADD(DAY, -18, GETDATE()), DATEADD(DAY, -2, GETDATE())),
(3, 3, N'Fabrikam Intake Automation', N'Healthcare intake workflow modernization.', 42000.00, N'Qualification', DATEADD(DAY, 60, GETDATE()), 1, 35, DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, -3, GETDATE())),
(4, 4, N'Blue Yonder Expansion', N'Add field service users.', 98000.00, N'ClosedWon', DATEADD(DAY, -7, GETDATE()), 1, 100, DATEADD(DAY, -45, GETDATE()), DATEADD(DAY, -7, GETDATE())),
(5, 6, N'Cityline Security Review', N'Pilot contingent on security sign-off.', 150000.00, N'Prospecting', DATEADD(DAY, 90, GETDATE()), 1, 20, DATEADD(DAY, -6, GETDATE()), DATEADD(DAY, -1, GETDATE())),
(6, 7, N'Verde Localization', N'German and Italian localization package.', 36000.00, N'Proposal', DATEADD(DAY, 50, GETDATE()), 2, 55, DATEADD(DAY, -8, GETDATE()), DATEADD(DAY, -2, GETDATE())),
(7, 9, N'Nordic Pilot', N'Three-store CRM pilot.', 28000.00, N'Prospecting', DATEADD(DAY, 70, GETDATE()), 2, 25, DATEADD(DAY, -4, GETDATE()), DATEADD(DAY, -1, GETDATE())),
(8, 10, N'Orbit Renewal', N'Multi-year enterprise renewal.', 210000.00, N'ClosedLost', DATEADD(DAY, -30, GETDATE()), 1, 0, DATEADD(DAY, -90, GETDATE()), DATEADD(DAY, -30, GETDATE()));
SET IDENTITY_INSERT dbo.Opportunities OFF;
GO

SET IDENTITY_INSERT dbo.Tickets ON;
INSERT INTO dbo.Tickets (Id, CustomerId, Title, Description, Priority, Status, AssignedToUserId, CreatedAt, ResolvedAt, ResolutionNotes)
VALUES
(1, 1, N'Import job failure', N'Nightly import fails on duplicate rows.', N'High', N'InProgress', 1, DATEADD(DAY, -3, GETDATE()), NULL, NULL),
(2, 4, N'Mobile app timeout', N'Field users report timeouts over VPN.', N'Critical', N'Open', 1, DATEADD(DAY, -1, GETDATE()), NULL, NULL),
(3, 5, N'Password reset request', N'Former admin requires account reset.', N'Low', N'Resolved', 2, DATEADD(DAY, -14, GETDATE()), DATEADD(DAY, -13, GETDATE()), N'Password updated and shared securely.'),
(4, 8, N'Case routing issue', N'Tickets are not routing to the student support queue.', N'High', N'Open', 1, DATEADD(DAY, -2, GETDATE()), NULL, NULL),
(5, 10, N'Invoice mismatch', N'Contract price differs from invoice line item.', N'Medium', N'Closed', 2, DATEADD(DAY, -20, GETDATE()), DATEADD(DAY, -18, GETDATE()), N'Finance corrected invoice.'),
(6, 2, N'API credential rotation', N'Need help rotating integration credentials.', N'Medium', N'InProgress', 2, DATEADD(DAY, -5, GETDATE()), NULL, NULL);
SET IDENTITY_INSERT dbo.Tickets OFF;
GO

SET IDENTITY_INSERT dbo.Activities ON;
INSERT INTO dbo.Activities (Id, CustomerId, ContactId, OpportunityId, Type, Description, ActivityDate, CreatedByUserId)
VALUES
(1, 1, 1, 1, N'Call', N'Renewal kickoff call completed.', DATEADD(DAY, -12, GETDATE()), 2),
(2, 1, 2, 1, N'Email', N'Sent pricing breakdown.', DATEADD(DAY, -10, GETDATE()), 2),
(3, 2, 3, 2, N'Meeting', N'Presented proposal to logistics team.', DATEADD(DAY, -8, GETDATE()), 2),
(4, 2, 4, 2, N'Note', N'Customer requested revised timeline.', DATEADD(DAY, -6, GETDATE()), 2),
(5, 3, 5, 3, N'Call', N'Qualification call with clinic manager.', DATEADD(DAY, -5, GETDATE()), 1),
(6, 4, 6, 4, N'Meeting', N'Expansion workshop complete.', DATEADD(DAY, -15, GETDATE()), 1),
(7, 4, 7, 4, N'Email', N'Shared implementation schedule.', DATEADD(DAY, -13, GETDATE()), 1),
(8, 5, 8, NULL, N'Note', N'Account marked inactive due to budget freeze.', DATEADD(DAY, -90, GETDATE()), 2),
(9, 6, 9, 5, N'Email', N'Sent security questionnaire.', DATEADD(DAY, -4, GETDATE()), 1),
(10, 6, 9, 5, N'Call', N'Compliance review scheduled.', DATEADD(DAY, -2, GETDATE()), 1),
(11, 7, 10, 6, N'Meeting', N'Localization requirements gathered.', DATEADD(DAY, -3, GETDATE()), 2),
(12, 7, 11, 6, N'Email', N'Shared translation scope.', DATEADD(DAY, -2, GETDATE()), 2),
(13, 8, 12, NULL, N'Call', N'Reviewed support backlog.', DATEADD(DAY, -1, GETDATE()), 1),
(14, 8, 12, NULL, N'Note', N'Escalated routing issue to engineering.', DATEADD(HOUR, -18, GETDATE()), 1),
(15, 9, 13, 7, N'Email', N'Sent pilot statement of work.', DATEADD(DAY, -1, GETDATE()), 2),
(16, 9, 13, 7, N'Call', N'Follow-up call planned for next week.', DATEADD(HOUR, -12, GETDATE()), 2),
(17, 10, 14, 8, N'Note', N'Renewal lost to competitor.', DATEADD(DAY, -30, GETDATE()), 1),
(18, 10, 15, NULL, N'Email', N'Closed billing issue with support manager.', DATEADD(DAY, -19, GETDATE()), 2),
(19, 1, 1, NULL, N'Note', N'Support ticket triaged with customer.', DATEADD(HOUR, -10, GETDATE()), 1),
(20, 2, 3, NULL, N'Email', N'Credential rotation guide sent.', DATEADD(HOUR, -8, GETDATE()), 2);
SET IDENTITY_INSERT dbo.Activities OFF;
GO
