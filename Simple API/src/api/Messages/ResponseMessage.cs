using FluentValidation.Results;

namespace Application.API.Messages
{
    public class ResponseMessage : Message
    {
        public ValidationResult ValidationResult { get; set; }
        public string Message { get;  set; }

        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

    }

}
