namespace Lib.Core.Models.Response
{
    public class ValidationResponseModel
    {
        public ValidationResponseModel()
        {
            Errors = new List<ValidationError>();
        }

        public bool HasErrors => Errors.Count > 0;
        public List<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {

        /// <summary>
        /// Gets or sets the code for this error.
        /// </summary>
        /// <value>
        /// The field for this error.
        /// </value>
        public string Field { get; set; } = default!;

        /// <summary>
        /// Gets or sets the description for this error.
        /// </summary>
        /// <value>
        /// The description for this error.
        /// </value>
        public string Message { get; set; } = default!;
    }
}
