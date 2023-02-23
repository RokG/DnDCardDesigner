using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.Interfaces
{
    public interface IViewModelBase
    {
        string Name { get; set; }
        string Description { get; set; }
        public ViewModelType Type { get; set; }
    }
}