namespace c1tr00z.TrainsAppointment.Map.Nodes {
    public interface IPassableNode {
        #region Methods

        public void Pass(INodePasser passer);

        #endregion
    }
}