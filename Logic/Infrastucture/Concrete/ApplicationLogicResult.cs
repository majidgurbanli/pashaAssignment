using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Infrastucture.Concrete
{
    public class ApplicationLogicResult : JsonResult
    {
        public bool Success { set; get; }
        public object Data { set; get; }
        public string Message { set; get; }
        public string ErrorMessage { set; get; }
        public FileContentResult FileContent { set; get; }

        public ApplicationLogicResult(bool Success, object Data, string Message = null) : base(new { Success, Data, Message })
        {
            this.Success = Success;
            this.Message = Message;
            this.Data = Data;
        }


        public ApplicationLogicResult(FileContentResult ContentResult) : base(new { Success = true })
        {
            this.Success = Success;
            this.FileContent = ContentResult;
        }

    }

    public class ApplicationLogicResult<T> : JsonResult
    {
        public bool Success { set; get; }
        public T Data { set; get; }
        public string Message { set; get; }
        public string ErrorMessage { set; get; }
        public FileContentResult FileContent { set; get; }

        public ApplicationLogicResult(bool Success, T Data, string Message) : base(new { Success, Data, Message })
        {
            this.Success = Success;
            this.Message = Message;
            this.Data = Data;
        }
        public ApplicationLogicResult(bool Success) : base(new { Success })
        {
            this.Success = Success;
        }
        public ApplicationLogicResult(FileContentResult ContentResult) : base(new { Success = true })
        {
            this.Success = Success;
            this.FileContent = ContentResult;
        }
    }
}
