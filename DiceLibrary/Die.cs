namespace DiceLibrary {
	/// <summary>
	///		<para>Represents a rollable die with a custom arrangement of faces.</para>
	/// </summary>
	public class Die {
		#region Data Members
		private List<int> faces = [];
		private Random _random = new();
		#endregion

		#region Properties
		/// <summary>
		///		<para>An <see cref="List{T}"/> of <see cref="int"/>s representation of the faces of the current <see cref="Die"/> instance.</para>
		/// </summary>
		public List<int> Faces {
			get {
				return faces;
			}

			set {
				ArgumentNullException.ThrowIfNull($"{nameof(Faces)} cannot be assigned to {value}.");
				faces = value;
			}
		}

		/// <summary>
		///		<para>An <see cref="int"/> representation of the number of faces of the current <see cref="Die"/> instance.</para>
		/// </summary>
		public int Size {
			get {
				return Faces.Count;
			}
		}

		/// <summary>
		///		<para>An <see cref="int"/> representation of the highest value in the faces of the current <see cref="Die"/> instance.</para>
		/// </summary>
		public int Max {
			get {
				return Faces.Max();
			}
		}

		/// <summary>
		///		<para>An <see cref="int"/> representation of the lowest value in the faces of the current <see cref="Die"/> instance.</para>
		/// </summary>
		public int Min {
			get {
				return Faces.Min();
			}
		}

		/// <summary>
		///		<para>An <see cref="double"/> representation of the average value in the faces of the current <see cref="Die"/> instance.</para>
		/// </summary>
		public double Average {
			get {
				return Faces.Average();
			}
		}

		/// <summary>
		///		<para>The seed used to determine the current <see cref="Die"/> instance's random results.</para>
		/// </summary>
		public int Seed {
			set {
				_random = new(value);
			}
		}

		/// <summary>
		///		<para>The <see cref="System.Random"/> class used to determine the current <see cref="Die"/> instance's random results.</para>
		/// </summary>
		protected Random Random {
			get {
				return _random;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		///		<para>Initializes a new instance of the <see cref="Die"/> class, using the specified <paramref name="faces"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		public Die(List<int> faces) {
			Faces = faces;
		}

		/// <summary>
		///		<para>Initializes a new instance of the <see cref="Die"/> class, using the specified <paramref name="faces"/> and <paramref name="seed"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		/// <param name="seed"></param>
		public Die(List<int> faces, int seed) {
			Faces = faces;
			Seed = seed;
		}
		#endregion

		#region Roll Methods
		#region Int Methods
		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>An <see cref="int"/> representing the <see langword="value"/> of the randomly rolled face of the current <see cref="Die"/> instance.</para>
		/// </returns>
		public virtual int Roll() {
			if (Faces.Count < 1) {
				throw new ArgumentOutOfRangeException($"Cannot a {nameof(Die)} with {Faces.Count} {nameof(Faces)}.");
			}

			return Faces[Random.Next(Size)];
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
		///	<exception cref="ArgumentOutOfRangeException"></exception>
		public int Advantage(int n) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");
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
		///	<exception cref="ArgumentOutOfRangeException"></exception>
		public int Disadvantage(int n) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");
			return Roll(n).Min();
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance, rerolling once if the instance doesn't roll higher than <paramref name="value"/>.</para>
		/// </summary>
		/// <param name="value"></param>
		/// <returns>
		///		<para>An <see cref="int"/> representing the <see langword="value"/> of the randomly rolled face of the current <see cref="Die"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public int ReRoll(int value) {
			if (value >= Faces.Max()) {
				throw new ArgumentOutOfRangeException($"Parameter {nameof(value)} cannot be greater than or equal to {nameof(Faces)} maximum value.{Environment.NewLine}{nameof(Faces)} Max Value: {Faces.Max()}");
			}

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
			if (value >= Faces.Max()) {
				throw new ArgumentOutOfRangeException($"Parameter {nameof(value)} cannot be greater than or equal to {nameof(Faces)} maximum value.{Environment.NewLine}{nameof(Faces)} Max Value: {Faces.Max()}");
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

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance <paramref name="n"/> times, counting the number of rolls that met or exceeded the target <paramref name="value"/>.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <param name="value"></param>
		/// <returns>
		///		<para>An <see cref="int"/> representing the number of roll that met or exceeded the target <paramref name="value"/>.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public int Target(int n, int value) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");

			List<int> rolls = Roll(n);

			return rolls.Where(x => x >= value).Count();
		}
		#endregion

		#region List Methods
		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance <paramref name="n"/> times.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the current <see cref="Die"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public List<int> Roll(int n) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");

			List<int> rolls = [];

			for (int i = 0; i < n; i++) {
				rolls.Add(Roll());
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance, rolling it an additional time each time it rolls its maximum face value.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the current <see cref="Die"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public List<int> Explode() {
			if (Faces.Max() == Faces.Min()) {
				throw new ArgumentOutOfRangeException($"Cannot exploding roll a {nameof(Die)} whose {nameof(Faces)} all have the same value.");
			}

			return Explode(Faces.Max());
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance, rolling it an additonal time each time it rolls a face of <paramref name="value"/> or greater.</para>
		/// </summary>
		/// <param name="value"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the current <see cref="Die"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public List<int> Explode(int value) {
			if (value <= Faces.Min()) {
				throw new ArgumentOutOfRangeException($"Parameter {nameof(value)} cannot be less than or equal to {nameof(Faces)} minimum value.{Environment.NewLine}{nameof(Faces)} Min Value: {Faces.Min()}");
			}

			List<int> rolls = [];

			do {
				rolls.Add(Roll());
			} while (rolls.Last() >= value);

			return rolls;
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance <paramref name="n"/> times, only keeping the <paramref name="x"/> highest values.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <param name="x"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the kept <see langword="values"/> of the randomly rolled faces of the current <see cref="Die"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public List<int> KeepHighest(int n, int x) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");
			ArgumentOutOfRangeException.ThrowIfNegative(x, $"Parameter {nameof(x)} cannot be negative.");
			ArgumentOutOfRangeException.ThrowIfGreaterThan(x, n, $"Parameter {nameof(x)} cannot be greater than parameter {nameof(n)}.");

			List<int> rolls = Roll(n);
			List<int> highest = [];

			for (int i = 0; i < x; i++) {
				highest.Add(rolls.Max());
				rolls.Remove(rolls.Max());
			}

			return highest;
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die"/> instance <paramref name="n"/> times, only keeping the <paramref name="x"/> lowest values.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <param name="x"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the kept <see langword="values"/> of the randomly rolled faces of the current <see cref="Die"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public List<int> KeepLowest(int n, int x) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");
			ArgumentOutOfRangeException.ThrowIfNegative(x, $"Parameter {nameof(x)} cannot be negative.");
			ArgumentOutOfRangeException.ThrowIfGreaterThan(x, n, $"Parameter {nameof(x)} cannot be greater than parameter {nameof(n)}.");

			List<int> rolls = Roll(n);
			List<int> lowest = [];

			for (int i = 0; i < x; i++) {
				lowest.Add(rolls.Min());
				rolls.Remove(rolls.Min());
			}

			return lowest;
		}
		#endregion
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
			ArgumentNullException.ThrowIfNullOrWhiteSpace(s, $"Parameter {nameof(s)} cannot be null, empty, or whitespace.");

			List<string> sections = [.. s.Split(':')];
			List<int> faces = [];
			List<int> weights = [];

			#region Sections Count Logic
			switch (sections.Count) {
				case 2:
					string[] weightValues = sections[1].Split(',');
					try {
						foreach (string value in weightValues) {
							weights.Add(int.Parse(value.Trim()));
						}
					} catch {
						throw new FormatException($"Parameter {nameof(s)} was not in the right format.");
					}
					goto case 1;
				case 1:
					string[] faceValues = sections[0].Split(',');
					try {
						foreach (string value in faceValues) {
							faces.Add(int.Parse(value.Trim()));
						}
					} catch {
						throw new FormatException($"Parameter {nameof(s)} was not in the right format.");
					}
					break;
				default:
					throw new FormatException($"Parameter {nameof(s)} was not in the right format.");
			}
			#endregion

			#region Return Logic
			if (weights.Count > 1) {
				try {
					return new CustomDie(faces, weights);
				} catch {
					throw new FormatException($"Parameter {nameof(s)} was not in the right format.");
				}
			}
			return new Die(faces);
			#endregion
		}
		#endregion

		#region Method Overrides
		/// <summary>
		///		<para>Returns a <see cref="string"/> representation of the current <see cref="Die"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="string"/> that represents the current <see cref="Die"/> instance.</para>
		/// </returns>
		public override string ToString() {
			return $"{string.Join(',', Faces)}";
		}
		#endregion
	}
}