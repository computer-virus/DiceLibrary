namespace DiceLibrary {
	public class CustomDie : Die {
		#region Properties
		private int[] CustomFaces { get; set; }
		#endregion

		#region Constructors
		public CustomDie(int[] faces) : base(faces.Length) {
			CustomFaces = faces;
			SetUpFaces();
		}

		public CustomDie(int[] faces, int seed) : base(faces.Length, seed) {
			CustomFaces = faces;
			SetUpFaces();
		}
		#endregion

		#region Method Overrides
		protected override void SetUpFaces() {
			for (int i = 0; i < Faces.Length; i++) {
				Faces[i] = CustomFaces[i];
			}
		}
		#endregion
	}
}