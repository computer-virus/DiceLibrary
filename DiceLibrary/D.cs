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
		public D(int size) : base(size) {
			SetUpFaces();
		}

		/// <summary>
		///		<para>Initializes a new instance of the <see cref="D"/> class, using the specified <paramref name="size"/> and <paramref name="seed"/>.</para>
		/// </summary>
		/// <param name="size"></param>
		/// <param name="seed"></param>
		public D(int size, int seed) : base(size, seed) {
			SetUpFaces();
		}
		#endregion

		#region Method Overrides
		/// <summary>
		///		<para>Sets up the current <see cref="D"/> instance's faces for use in rolling.</para>
		/// </summary>
		protected override void SetUpFaces() {
			for (int i = 0; i < Faces.Length; i++) {
				Faces[i] = i + 1;
			}
		}
		#endregion
	}
}