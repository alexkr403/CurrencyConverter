using System.Runtime.Serialization;
using System.ServiceModel;

namespace Server.CurrencyConverter
{
    [ServiceContract()]
    public interface ICurrencyConverterService
    {
        [OperationContract]
        NumberPresentationResult GetNumberPresentation(string value);
    }

    [DataContract]
    public class NumberPresentationResult
    {
        [DataMember]
        public bool Success
        {
            get;
            set;
        }

        [DataMember]
        public string Number
        {
            get;
            set;
        }

        [DataMember]
        public string ErrorMessage
        {
            get;
            set;
        }

        public static readonly NumberPresentationResult Error = new NumberPresentationResult();

        private NumberPresentationResult()
        {
            Success = false;
            ErrorMessage = "An error has occurred while converting";
        }

        public NumberPresentationResult(
            bool success,
            string number,
            string error
            )
        {
            Success = success;
            Number = number;
            ErrorMessage = error;
        }
    }
}
