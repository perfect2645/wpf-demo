namespace UserComponent.Core.viewmodels.diagrams.abstractions
{
    public interface IActiveAware
    {
        bool IsActive { get; set; }
        void SetIsActive(bool isActive);
    }
}
