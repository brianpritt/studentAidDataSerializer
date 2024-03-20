namespace StudentAidData{
    public class StudentInfo{
        public StudentInfo()
        {}
        public string? StudentFirstName { get; set; }
        public string? StudentMiddleInitial { get; set; }
        public string? StudentLastName{ get; set; }
        public string? StudentStreetAddress1 { get; set; }
        public string? StudentStreetAddress2 { get; set; }
        public string? StudentCity{ get; set; }
        public string? StudentStateCode { get; set; }
        public string? StudentCountryCode { get; set; }
        public string? StudentZipCode { get; set; }
        public string? StudentEmailAddress { get; set; }
        public string? StudentHomePhoneCountryCode { get; set; } 
        public string? StudentHomePhoneNumber { get; set; }
        public string? StudentHomePhonePreferred { get; set; }
        public string? StudentCellPhoneCountryCode { get; set; }
        public string? StudentCellPhoneNumber { get; set; }
        public string? StudentCellPhonePreferred { get; set; }
        public string? StudentWorkPhoneCountryCode { get; set; }
        public string? StudentWorkPhoneNumber { get; set; }
        public string? StudentWorkPhonePreferred { get; set; }
        public string? StudentSULAMaximumEligibilityPeriod { get; set; }
        public string? StudentSULASubsidizedUsagePeriod { get; set; }
        public string? StudentSULARemainingEligibilityPeriod { get; set; }
        public string? StudentEnrollmentStatusCode { get; set; }
        public string? StudentEnrollmentStatusCodeDescription { get; set; }
        public string? UndergraduateSubsidizedLoanLimitFlag{ get; set; }
        public string? UndergraduateCombinedLoanLimitFlag{ get; set; }
        public string? UndergraduateAwardYear { get; set; }
        public string? UndergraduateDependencyIndicator { get; set; }
        public string? UndergraduateAggregateSubsidizedTotal { get; set; }
        public string? UndergraduateAggregateUnsubsidizedTotal{ get; set; }
        public string? UndergraduateAggregateCombinedTotal { get; set; }
        public string? GraduateSubsidizedLoanLimitFlag{get; set;}
        public string? GraduateCombinedLoanLimitFlag{get; set;}
        public string? GraduateAwardYear {get; set;}
        public string? GraduateDependencyIndicator {get; set;}
        public string? GraduateAggregateSubsidizedTotal {get; set;}
        public string? GraduateAggregateUnsubsidizedTotal {get; set;}
        public string? GraduateAggregateCombinedTotal {get; set;}
        public string? AggregateSubsidizedTotal { get; set;}
        public string? AggregateUnsubsidizedTotal { get; set;}
        public string? AggregateCombinedTotal { get; set;}
        public string? StudentTotalAllLoansOutstandingPrincipal { get; set;}
        public string? StudentTotalAllLoansOutstandingInterest { get; set;}
        public string? StudentPellLifetimeEligibilityUsed { get; set;}
        public string? StudentIraqandAfghanistanServiceLifetimeEligibilityUsed { get; set;}
        public string? StudentTotalAllGrants { get; set;}
        public List<Loan>? StudentLoans {get;set;}
    }
}