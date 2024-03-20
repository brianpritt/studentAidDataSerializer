using System;
using System.Reflection;
using System.Globalization;
namespace StudentAidData
{
    public class Status 
    {
        public string? LoanStatus {get;set;}
        public string? LoanStatusDescription {get;set;}
        public DateTime? LoanStatusEffectiveDate {get;set;}

    }

    public class Loan 
    {
        public string? LoanTypeCode {get;set;}
        public string? LoanTypeDescription {get;set;}
        public string? LoanAwardID {get;set;}
        public string? LoanAttendingSchoolName {get;set;}
        public string? LoanAttendingSchoolOPEID {get;set;}
        public DateTime? LoanDate {get;set;} 
        public DateTime? LoanRepaymentBeginDate {get;set;}
        public DateTime LoanPeriodBeginDate {get;set;}
        public DateTime LoanPeriodEndDate {get;set;}
        public decimal LoanAmount {get;set;}
        public decimal LoanDisbursedAmount {get;set;}
        public decimal LoanCanceledAmount {get;set;}
        public DateTime LoanCanceledDate {get;set;}
        public decimal LoanOutstandingPrincipalBalance {get;set;}
        public DateTime LoanOutstandingPrincipalBalanceasofDate {get;set;}
        public decimal LoanOutstandingInterestBalance {get;set;}
        public DateTime LoanOutstandingInterestBalanceasofDate {get;set;}
        public string? LoanInterestRateTypeCode {get;set;}
        public string? LoanInterestRateTypeDescription {get;set;}
        public decimal LoanInterestRate {get;set;}
        public decimal LoanActualInterestRate {get;set;}
        public decimal LoanStatutoryInterestRate {get;set;}
        public string? LoanRepaymentPlanTypeCode {get;set;}
        public string? LoanRepaymentPlanTypeCodeDescription {get;set;}
        public string? LoanRepaymentPlanBeginDate {get;set;}
        public decimal LoanRepaymentPlanScheduledAmount {get;set;}
        public DateTime LoanRepaymentPlanIDRPlanAnniversaryDate {get;set;}
        public string? LoanConfirmedSubsidyStatus {get;set;}
        public string? LoanSubsidizedUsageinYears {get;set;}
        public DateTime LoanReaffirmationDate {get;set;}
        public DateTime LoanMostRecentPaymentEffectiveDate {get;set;}
        public DateTime LoanNextPaymentDueDate {get;set;}
        public decimal LoanCumulativePaymentAmount {get;set;}
        public string? LoanPSLFCumulativeMatchedMonths {get;set;}
        public string? AcademicLevel {get;set;}
        public string? AdditionalUnsubsidizedLoanFlag {get;set;}
        public int AwardYear {get;set;}
        public decimal CapitalizedInterest {get;set;}
        public decimal NetLoanAmount {get;set;}
        public string? Reaffirmationflag {get;set;}
        public decimal CalculatedSubsidizedAggregateOPB {get;set;}
        public decimal CalculatedUnsubsidizedAggregateOPB {get;set;}
        public decimal CalculatedCombinedAggregateOPB {get;set;}
        public DateTime UpdtDt {get;set;}
        public DateTime DelinqDate {get;set;}
        public string? CurrentLoanStatus {get;set;}
        public string? CurrentLoanStatusDescription {get;set;}
        public decimal HighestHistoricalOutstandingPrincipalBalance {get;set;}
        public decimal CurrentStandardStandardSchedulePaymentAmount {get;set;}
        public decimal PermanentStandardStandardSchedulePaymentAmount {get;set;}
        public string? ParentPlusConsolidationIndicator {get;set;}
        public DateTime LoanDisbursementDate {get;set;}
        public decimal LoanDisbursementAmount {get;set;}
        public string? LoanContactType {get;set;}
        public string? LoanContactCode {get;set;}
        public string? LoanContactName {get;set;}
        public string? LoanContactStreetAddress_1 {get;set;}
        public string? LoanContactStreetAddress_2 {get;set;}
        public string? LoanContactCity {get;set;}
        public string? LoanContactStateCode {get;set;}
        public string? LoanContactZipCode {get;set;}
        public string? LoanContactPhoneNumber {get;set;}
        public string? LoanContactPhoneExtension {get;set;}
        public string? LoanContactEmailAddress {get;set;}
        public string? LoanContactWebSiteAddress {get;set;}
        public string? MostRelevant {get;set;}
        public string? LoanSpecialContactReason {get;set;}
        public string? LoanSpecialContact {get;set;}
        public List<Status>? StatusChanges {get;set;} 
        
        public static List<List<string>> CreateList(string[] lines)
        {
            List<List<string>> loans = new List<List<string>>();
            List<string> currentLoan = new List<string>();
            bool loanStart = false;

            foreach (string line in lines){
                if (line.StartsWith("Loan Type Code"))
                {
                    loanStart = true;
                }
                
                if (loanStart)
                {
                    currentLoan.Add(line);
                    if (line.StartsWith("Loan Special Contact") && !line.StartsWith("Loan Special Contact Reason"))
                    {
                        currentLoan.Add(line);
                        loans.Add(new List<string>(currentLoan));  
                        currentLoan.Clear();  
                        loanStart = false;
                    }
                }   
            }
            return loans;
        }
        public static List<Loan> CovertLoans(List<List<string>> loans)
        {
            string format = "MM/dd/yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            List<Loan> loanList = new List<Loan>();
            foreach(var loan in loans)
            {
                Loan currentLoan = new Loan();
                List<string> statusChanges = new List<string>();
                List<Status> status = new List<Status>();
                Type loanType = typeof(Loan);
                foreach(string line in loan)
                {
                    Dictionary<string,string> loanDict = new Dictionary<string, string>();
                    var parts = line.Split(new[] { ':' }, 2);
                    string key = parts[0];
                    string value = parts[1] ?? "";

                    if (key.Trim() =="Loan Status" || key.Trim() =="Loan Status Description" || key.Trim() =="Loan Status Effective Date")
                    {
                        statusChanges.Add(new string(line));
                    }

                    PropertyInfo? propertyInfo = loanType.GetProperty(String.Concat(key.Where(c => !Char.IsWhiteSpace(c))), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    
                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                        {
                            if(value != ""){
                                propertyInfo.SetValue(currentLoan, DateTime.ParseExact(value, format, provider));
                            } 
                            else
                            {
                                propertyInfo.SetValue(currentLoan, null);
                            }
                        }
                        else if(propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(decimal?))
                            
                            {
                                bool containsPercent = value.Contains('%');
                                bool containsDollar = value.Contains('$');
                                if (containsDollar || containsPercent)
                                {
                                    propertyInfo.SetValue(currentLoan, Decimal.Parse(value.Trim(containsDollar ? '$' :'%')));
                                }
                            }
                        else if(propertyInfo.PropertyType == typeof(int))
                        {
                            propertyInfo.SetValue(currentLoan, int.Parse(value));
                        }
                        else 
                        {
                            propertyInfo.SetValue(currentLoan, value);
                        }
                    }
                }
                Status loanStatus = new();
                foreach(var lin in statusChanges)
                {
                    var parts = lin.Split(new[] { ':' }, 2);
                    string key = parts[0];
                    string value = parts[1] != null ? parts[1] : "";
                    
                    switch(key)
                    {
                        case "Loan Status":
                            loanStatus.LoanStatus = value;
                            break;
                        case "Loan Status Description":
                            loanStatus.LoanStatusDescription = value;
                            break;
                        case "Loan Status Effective Date":
                            try {
                                loanStatus.LoanStatusEffectiveDate = DateTime.ParseExact(value, format, provider);                            
                            }
                            catch(Exception ex) {
                                Console.WriteLine(ex.Message);
                                loanStatus.LoanStatusEffectiveDate = null;
                            }
                            status.Add(new Status{
                                LoanStatus=loanStatus.LoanStatus,
                                LoanStatusDescription=loanStatus.LoanStatusDescription,
                                LoanStatusEffectiveDate=loanStatus.LoanStatusEffectiveDate
                            });
                            loanStatus = new Status();
                            break;
                    }
                currentLoan.StatusChanges = status;
                }
                loanList.Add(currentLoan);
            }
            return loanList;
        }
    }
}