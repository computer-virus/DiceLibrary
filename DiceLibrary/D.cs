namespace DiceLibrary {
	/// <summary>
	///		<para>Represents a rollable die with the standard arrangement of faces.</para>
	/// </summary>
	public class D : Die {
		#region Constructors
		/// <summary>
		///		<para>Initializes a new instance of the <see cref="D"/> class, using the specified <paramref name="size"/>.</para>
		/// </summary>
		/// <param name="size"></param>
		public D(int size) : base(SetUpFaces(size)) { }

		/// <summary>
		///		<para>Initializes a new instance of the <see cref="D"/> class, using the specified <paramref name="size"/> and <paramref name="seed"/>.</para>
		/// </summary>
		/// <param name="size"></param>
		/// <param name="seed"></param>
		public D(int size, int seed) : base(SetUpFaces(size), seed) { }
		#endregion

		#region Methods
		private static List<int> SetUpFaces(int size) {
			List<int> list = [];	

			for (int i = 0; i < size; i++) {
				list.Add(i + 1);
			}

			return list;
		}
		#endregion
	}
}