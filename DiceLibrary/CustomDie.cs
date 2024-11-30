namespace DiceLibrary {
	/// <summary>
	///		<para>Represents a rollable die with a custom arrangement of faces and probabilities to roll said faces.</para>
	/// </summary>
	public class CustomDie : NumberDie {
		#region Data Members
		private List<int> _weights = [];
		#endregion

		#region Properties
		/// <summary>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the weight of each of the current <see cref="CustomDie"/> instance's faces.</para>
		/// </summary>
		public List<int> Weights {
			get {
				return _weights;
			}

			set {
				ArgumentNullException.ThrowIfNull($"{nameof(Weights)} cannot be assigned to {value}.");

				if (value.Count != Size) {
					throw new FormatException($"{nameof(Weights)} must contain {Size} values.\nCurrently contains {value.Count} values.");
				}

				if (value.Any(x => x < 0)) {
					throw new ArgumentOutOfRangeException($"{nameof(Weights)} cannot have negative values.\nOffending values: {string.Join(", ", value.Where(x => x < 0))}");
				}

				_weights = value;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		///		<para>Initializes a new instance of the <see cref="CustomDie"/> class, using the specified <paramref name="faces"/> and <paramref name="weights"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		/// <param name="weights"></param>
		public CustomDie(List<int> faces, List<int> weights) : base(faces) {
			Weights = weights;
		}

		/// <summary>
		///		<para>Initializes a new instance of the <see cref="CustomDie"/> class, using the specified <paramref name="faces"/>, <paramref name="weights"/>, and <paramref name="seed"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		/// <param name="weights"></param>
		/// <param name="seed"></param>
		public CustomDie(List<int> faces, List<int> weights, int seed) : base(faces, seed) {
			Weights = weights;
		}
		#endregion

		#region Methods
		/// <summary>
		///		<para>Adds a new <paramref name="face"/> and <paramref name="weight"/> to the current <see cref="CustomDie"/> instance.</para>
		/// </summary>
		/// <param name="face"></param>
		/// <param name="weight"></param>
		/// <returns>
		///		<para>The current <see cref="CustomDie"/> instance in order to simplify its use in instantiation.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public CustomDie AddFace(int face, int weight) {
			ArgumentOutOfRangeException.ThrowIfNegative(weight, $"Parameter {nameof(weight)} cannot be negative.");

			Faces.Add(face);
			Weights.Add(weight);

			return this;
		}

		/// <summary>
		///		<para>Removes the face and weight at the given <paramref name="index"/> of the current <see cref="CustomDie"/> instance.</para>
		/// </summary>
		/// <param name="index"></param>
		/// <returns>
		///		<para>The current <see cref="CustomDie"/> instance in order to simplify its use in instantiation.</para>
		/// </returns>
		/// <exception cref="IndexOutOfRangeException"></exception>
		public CustomDie RemoveFace(int index) {
			if (index < 0 || index >= Size) {
				throw new IndexOutOfRangeException($"Parameter {nameof(index)} must be a value between 0 and {Size}, upper bound exclusive.");
			}

			Faces.RemoveAt(index);
			Weights.RemoveAt(index);

			return this;
		}
		#endregion

		#region Method Overrides
		/// <summary>
		///		<para>Rolls the current <see cref="CustomDie"/> instance, taking into account the weights of its faces.</para>
		/// </summary>
		/// <returns>
		///		<para>An <see cref="int"/> representing the <see langword="value"/> of the randomly rolled face of the current <see cref="CustomDie"/> instance.</para>
		/// </returns>
		public override int Roll() {
			if (Weights.Count != Size || Weights.Any(x => x < 0)) {
				throw new InvalidDataException($"Instance data was not properly synced.\nPlease use {nameof(CustomDie)} specific methods when modifying a {nameof(CustomDie)}.");
			}

			if (Faces.Count < 1) {
				throw new ArgumentOutOfRangeException($"Cannot a {nameof(CustomDie)} with {Faces.Count} {nameof(Faces)}.");
			}

			int max = Weights.Sum();
			int rolledWeight = Random.Next(max);

			for (int i = 0; i < Size; i++) {
				if (rolledWeight < Weights[i]) {
					return Faces[i];
				}

				rolledWeight -= Weights[i];
			}

			throw new InvalidDataException($"Instance data was not properly synced.\nPlease use {nameof(CustomDie)} specific methods when modifying a {nameof(CustomDie)}.");
		}

		/// <summary>
		///		<para>Returns a <see cref="string"/> representation of the current <see cref="CustomDie"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="string"/> that represents the current <see cref="CustomDie"/> instance.</para>
		/// </returns>
		public override string ToString() {
			return $"{base.ToString()}:{string.Join(',', Weights)}";
		}
		#endregion
	}
}