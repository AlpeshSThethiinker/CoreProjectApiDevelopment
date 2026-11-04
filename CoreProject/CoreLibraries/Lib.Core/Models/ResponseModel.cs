using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Models;
    public class ResponseModel
    {
        public ResponseModel()
        {
            IsSuccess = false;
            Message = null;
            Response = null;
            Errors = null;
        }

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Response { get; set; }
        public IList<string>? Errors { get; set; }

        public int Status => IsSuccess ? 200 : 400;
    }

