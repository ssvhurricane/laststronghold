using View;

namespace Services.Building
{
    public interface IBuildingHolder
    {
        public void SetPlacedObject(IView viewObject);

        public IView GetPlacedObject();

        public bool IsPlacedObjectEmpty();
    }
}
