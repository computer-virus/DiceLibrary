namespace DiceLibrary {
	public class D : Die {
		#region Constructors
		public D(int size) : base(size) {
			SetUpFaces();
		}

		public D(int size, int seed) : base(size, seed) {
			SetUpFaces();
		}
		#endregion

		#region Method Overrides
		protected override void SetUpFaces() {
			for (int i = 0; i < Faces.Length; i++) {
				Faces[i] = i + 1;
			}
		}
		#endregion
	}
}