namespace MediSureClinic
{

    /// <summary>
    /// Represents a patient's billing information, including charges, discounts, and payment details for medical
    /// services.           
    /// </summary>
    /// <remarks>The PatientBill class encapsulates all relevant billing data for a single patient visit or
    /// transaction. It includes details such as consultation fees, laboratory and medicine charges, insurance status,
    /// and calculated totals. This class can be used to generate invoices, track payments, or integrate with healthcare
    /// billing systems.</remarks>
    public class PatientBill
    {
        public string? BillId { get; set; }
        public string? PatientName { get; set; }
        public bool HasInsurance { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }
    }
}

