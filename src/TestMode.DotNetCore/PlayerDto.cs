namespace TestMode
{
    internal class PlayerDto
    {
        public int Id { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }

        #region Overrides of Object

        public override string ToString()
        {
            return $"{Id}, ({PositionX}, {PositionY}, {PositionZ})";
        }

        #endregion
    }
}