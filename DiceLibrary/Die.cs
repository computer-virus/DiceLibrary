namespace DiceLibrary {
	/// <summary>
	///		<para>Represents a rollable die.</para>
	/// </summary>
	public abstract class Die {
		#region Data Members
		private int? _size;
		private int[] _faces = [];
		private int? _seed;
		private Random? _random;
		#endregion

		#region Properties
		/// <summary>
		///		<para>An <see cref="int"/> representation of the number of faces of the current <see cref="Die"/> instance.</para>
		/// </summary>
		public int Size {
			get {
				return _size ?? throw new InvalidOperationException($"{nameof(Size)} must be assigned a value before it is accessed.");
			}

			private set {
				if (_size != null) {
					throw new InvalidOperationException($"Cannot reassign the value of {nameof(Size)} once it has been assigned.");
				}

				if (value < 2) {
					throw new ArgumentOutOfRangeException($"{nameof(Size)} cannot be assigned a value that's less than 2.{Environment.NewLine}Assigned Value: {value}");
				}

				_size = value;
				_faces = new int[value];
			}
		}

		/// <summary>
		///		<para>An <see cref="int"/>[] representation of the faces of the current <see cref="Die"/> instance.</para>
		/// </summary>
		public int[] Faces {
			get {
				return _faces;
			}
		}

		/// <summary>
		///		<para>The seed used to determine the current <see cref="Die"/> instance's random results.</para>
		/// </summary>
		public int Seed {
			get {
				return _seed ?? throw new InvalidOperationException($"{nameof(Seed)} must be assigned a value before it is accessed.");
			}

			set {
				ArgumentNullException.ThrowIfNull(value, $"{nameof(Seed)} cannot be assigned to {value}.");
				_seed = value;
				_random = new(value);
			}
		}

		private Random Random {
			get {
				return _random ?? throw new InvalidOperationException($"{nameof(Random)} must be assigned a value before it is accessed.");
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		///		<para>Initializes a new instance of the <see cref="Die"/> class, using the specified <paramref name="size"/>.</para>
		/// </summary>
		/// <param name="size"></param>
		protected Die(int size) {
			Size = size;
			Seed = new Random().Next(int.MinValue, int.MaxValue);
		}

		/// <summary>
		///		<para>Initializes a new instance of the <see cref="Die"/> class, using the specified <paramref name="size"/> and <paramref name="seed"/>.</para>
		/// </summary>
		/// <param name="size"></param>
		/// <param name="seed"></param>
		protected Die(int size, int seed) {
			Size = size;
			Seed = seed;
		}
		#endregion

		#region Roll Methods
		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>An <see cref="int"/> representing the <see langword="value"/> of the randomly rolled face of the current <see cref="Die"/> instance.</para>
		/// </returns>
		public int Roll() {
			return Faces[Random.Next(Size)];
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance <paramref name="n"/> times.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the current <see cref="Die"/> instance.</para>
		/// </returns>
		public List<int> Roll(int n) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");

			List<int> rolls = [];
			
			for (int i = 0; i < n; i++) {
				rolls.Add(Roll());
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance twice and keeps the higher roll.</para>
		/// </summary>
		/// <returns>
		///		<para>An <see cref="int"/> representing the <see langword="value"/> of the higher randomly rolled face of the current <see cref="Die"/> instance.</para>
		/// </returns>
		public int Advantage() {
			return Advantage(2);
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance <paramref name="n"/> times and keeps the highest roll.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <returns>
		///		<para>An <see cref="int"/> representing the <see langword="value"/> of the highest randomly rolled face of the current <see cref="Die"/> instance.</para>
		///	</returns>
		public int Advantage(int n) {
			return Roll(n).Max();
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance twice and keeps the lower roll.</para>
		/// </summary>
		/// <returns>
		///		<para>An <see cref="int"/> representing the <see langword="value"/> of the lower randomly rolled face of the current <see cref="Die"/> instance.</para>
		/// </returns>
		public int Disadvantage() {
			return Disadvantage(2);
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance <paramref name="n"/> times and keeps the lowest roll.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <returns>
		///		<para>An <see cref="int"/> representing the <see langword="value"/> of the lowest randomly rolled face of the current <see cref="Die"/> instance.</para>
		///	</returns>
		public int Disadvantage(int n) {
			return Roll(n).Min();
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance, rerolling once if the instance doesn't roll higher than <paramref name="value"/>.</para>
		/// </summary>
		/// <param name="value"></param>
		/// <returns>
		///		<para>An <see cref="int"/> representing the <see langword="value"/> of the randomly rolled face of the current <see cref="Die"/> instance.</para>
		/// </returns>
		public int ReRoll(int value) {
			return ReRoll(value, true);
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance, rerolling once if <paramref name="once"/> is <see langword="true"/> and the instance doesn't roll higher than <paramref name="value"/>.</para>
		///		<para>If <paramref name="once"/> is <see langword="false"/>, then the instance will continue to reroll until it rolls higher than <paramref name="value"/>.</para>
		/// </summary>
		/// <param name="value"></param>
		/// <param name="once"></param>
		/// <returns>
		///		An <see cref="int"/> representing the <see langword="value"/> of the randomly rolled face of the current <see cref="Die"/> instance.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public int ReRoll(int value, bool once) {
			if (value > Size && !once) {
				throw new ArgumentOutOfRangeException($"Parameter {nameof(value)} cannot exceed {nameof(Size)}.{Environment.NewLine}{nameof(Size)}: {Size}");
			}

			int roll = Roll();

			if (roll > value) {
				return roll;
			}

			do {
				roll = Roll();

				if (roll > value) {
					return roll;
				}
			} while (!once);

			return roll;
		}
		#endregion

		#region Parse Methods
		/// <summary>
		///		<para>Converts a <see cref="string"/> representation of a die to its <see cref="Die"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="Die"/> equivalent to the die contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="FormatException"></exception>
		public static Die Parse(string s) {
			ArgumentNullException.ThrowIfNull(s, $"Parameter {nameof(s)} cannot be null.");

			string[] values = s.Split(':');

			if (values.Length != 3) {
				throw new FormatException($"Parameter {nameof(s)} was not in the right format.");
			}
			
			int size = int.Parse(values[0]);

			int[] faces = new int[size];
			string[] faceValues = values[1].Split(',');
			for (int i = 0; i < size; i++) {
				faces[i] = int.Parse(faceValues[i]);
			}

			int seed = int.Parse(values[2]);

			return new CustomDie(faces, seed);
		}
		#endregion

		#region Abstract Methods
		/// <summary>
		///		<para>Sets up the current <see cref="Die"/> instance's faces for use in rolling.</para>
		///		<para>This <see langword="method"/> must be called in the <see langword="derived class"/>' constructor.</para>
		/// </summary>
		protected abstract void SetUpFaces();
		#endregion

		#region Method Overrides
		/// <summary>
		///		<para>Returns a <see cref="string"/> representation of the current <see cref="Die"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="string"/> that represents the current <see cref="Die"/> instance.</para>
		/// </returns>
		public override string ToString() {
			return $"{Size}:{string.Join(',', Faces)}:{Seed}";
		}
		#endregion
	}
}