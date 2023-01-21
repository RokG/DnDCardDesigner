using System.ComponentModel;

namespace CardDesigner.Domain.Interfaces
{
    public interface IViewModelBase
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}