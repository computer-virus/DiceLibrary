namespace DiceLibrary {
	/// <summary>
	///		<para>A representation of a rollable die with the standard arrangement of <see cref="int"/> type faces that is otherwise interchangeable with the <see cref="CustomDie"/> class.</para>
	/// </summary>
	public class Die : CustomDie {
		#region Constructors
		/// <summary>
		///		<para>Initializes a new instance of the <see cref="Die"/> class, using the specified <paramref name="size"/>.</para>
		/// </summary>
		/// <param name="size"></param>
		public Die(int size) : base(SetUpFaces(size)) { }

		/// <summary>
		///		<para>Initializes a new instance of the <see cref="Die"/> class, using the specified <paramref name="size"/> and <paramref name="seed"/>.</para>
		/// </summary>
		/// <param name="size"></param>
		/// <param name="seed"></param>
		public Die(int size, int? seed) : base(SetUpFaces(size), seed) { }
		#endregion

		#region Utility Methods
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