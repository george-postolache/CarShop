using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop
{
    public class Messages
    {
        public const string UpdateMessage = "Do you want to save changes?";
        public const string UpdateTitle = "Save";

        public const string ErrorTitle = "Error";
        public const string WarningTitle = "Warning";
        public const string InformationTitle = "Information";
        public const string CompleteAllFieldsError = "Complete all fields!";
        public const string VINTooShortError = "The VIN must be 17 characters!";
        public const string VINAlreadyExistError = "The VIN already exists!";

        public const string PINTooShortError = "The PIN must be 13 characters!";
        public const string PINAlreadyExistError = "The PIN already exists!";

        public const string ClientIsNotSelected = "Select the client to proceed.";
        public const string CarIsNotSelected = "Select the car to proceed.";

        public const string SaleCompleted = "The sale was completed successfully.";
    }
}
