namespace Features.MineResources.Code {
    public interface IResourceHolder {

        #region Accessors

        bool HasResource { get; }

        float TimeToMine { get; }

        #endregion
        
        #region Methods

        void AddResource();

        void RemoveResource();

        #endregion
    }
}