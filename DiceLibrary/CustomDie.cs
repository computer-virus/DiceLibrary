namespace DiceLibrary {

	/// <summary>
	///		<para>Represents a rollable die with a custom arrangement of faces.</para>
	/// </summary>
	public class CustomDie : Die {
		#region Properties
		private int[] CustomFaces { get; set; }
		#endregion

		#region Constructors
		/// <summary>
		///		<para>Initializes a new instance of the <see cref="CustomDie"/> class, using the specified <paramref name="faces"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		public CustomDie(int[] faces) : base(faces.Length) {
			CustomFaces = faces;
			SetUpFaces();
		}

		/// <summary>
		///		<para>Initializes a new instance of the <see cref="CustomDie"/> class, using the specified <paramref name="faces"/> and <paramref name="seed"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		/// <param name="seed"></param>
		public CustomDie(int[] faces, int seed) : base(faces.Length, seed) {
			CustomFaces = faces;
			SetUpFaces();
		}
		#endregion

		#region Method Overrides
		/// <summary>
		///		<para>Sets up the current <see cref="CustomDie"/> instance's faces for use in rolling.</para>
		/// </summary>
		protected override void SetUpFaces() {
			for (int i = 0; i < Faces.Length; i++) {
				Faces[i] = CustomFaces[i];
			}
		}
		#endregion
	}
}