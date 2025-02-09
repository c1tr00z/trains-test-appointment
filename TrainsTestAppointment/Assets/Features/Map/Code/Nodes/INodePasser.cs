namespace c1tr00z.TrainsAppointment.Map.Nodes {
    public interface INodePasser {

        #region Accessors

        float TimeToMine { get; }

        #endregion
        
        #region Methods

        public void Occupy();

        public void Release();

        #endregion
    }
}