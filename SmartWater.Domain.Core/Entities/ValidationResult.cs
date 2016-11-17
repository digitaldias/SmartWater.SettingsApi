namespace SmartWater.Domain.Core.Entities
{
    public class ValidationResult
    {
        public bool Passed { get; set; }

        public string Message { get; set; }


        public ValidationResult()
        {
            Passed = false;
            Message = string.Empty;
        }


        public static ValidationResult Pass
        {
            get
            {
                return new ValidationResult { Passed = true };
            }
        }


        public static ValidationResult Fail(string message)
        {
            return new ValidationResult
            {
                Passed = false,
                Message = message
            };
        }
    }
}
