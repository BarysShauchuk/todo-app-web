using System.ComponentModel.DataAnnotations;

namespace Todo.Application.Models.ViewModels
{
    public class ReturnUrlViewModel<T>
    {
        public ReturnUrlViewModel()
        {
        }

        public ReturnUrlViewModel(T model, string returnUrl)
        {
            Model = model;
            ReturnUrl = returnUrl;
        }

        public T Model { get; set; }
        public string ReturnUrl { get; set; }
    }
}
