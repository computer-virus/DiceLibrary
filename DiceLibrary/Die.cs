namespace DiceLibrary {
	/// <summary>
	/// <para>A simpler, generic representation of a rollable die with a custom arrangement of <typeparamref name="T"/> type faces.</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Die<T> {
		#region Data Members
		private List<T> _faces = [];
		private Random _random = new();
		#endregion

		#region Properties
		/// <summary>
		///		<para>An <see cref="List{T}"/> representation of the faces of the current <see cref="Die{T}"/> instance.</para>
		/// </summary>
		public List<T> Faces {
			get {
				return _faces;
			}

			set {
				ArgumentNullException.ThrowIfNull($"{nameof(Faces)} cannot be assigned to {value}.");
				_faces = value;
			}
		}

		/// <summary>
		///		<para>An <see cref="int"/> representation of the number of faces of the current <see cref="Die{T}"/> instance.</para>
		/// </summary>
		public int Size {
			get {
				return Faces.Count;
			}
		}

		/// <summary>
		///		<para>The seed used to determine the current <see cref="Die{T}"/> instance's random results.</para>
		/// </summary>
		public int Seed {
			set {
				_random = new(value);
			}
		}

		/// <summary>
		///		<para>The <see cref="System.Random"/> class used to determine the current <see cref="Die{T}"/> instance's random results.</para>
		/// </summary>
		protected Random Random {
			get {
				return _random;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		///		<para>Initializes a new instance of the <see cref="Die{T}"/> class, using the specified <paramref name="faces"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		public Die(List<T> faces) {
			Faces = faces;
		}

		/// <summary>
		///		<para>Initializes a new instance of the <see cref="Die{T}"/> class, using the specified <paramref name="faces"/> and <paramref name="seed"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		/// <param name="seed"></param>
		public Die(List<T> faces, int seed) {
			Faces = faces;
			Seed = seed;
		}
		#endregion

		#region Roll Methods
		/// <summary>
		///		<para>Rolls the current <see cref="Die{T}"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <typeparamref name="T"/> representing the <see langword="value"/> of the randomly rolled face of the current <see cref="Die{T}"/> instance.</para>
		/// </returns>
		public virtual T Roll() {
			if (Faces.Count < 1) {
				throw new ArgumentOutOfRangeException($"Cannot a {nameof(Die<T>)} with {Faces.Count} {nameof(Faces)}.");
			}

			return Faces[Random.Next(Size)];
		}

		/// <summary>
		///		<para>Rolls the current <see cref="Die{T}"/> instance <paramref name="n"/> times.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> representing the <see langword="values"/> of the randomly rolled faces of the current <see cref="Die{T}"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public List<T> Roll(int n) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");

			List<T> rolls = [];
			
			for (int i = 0; i < n; i++) {
				rolls.Add(Roll());
			}

			return rolls;
		}
		#endregion

		#region Conversion Methods
		/// <summary>
		///		<para>Converts a <see cref="string"/> representation of a die to its <see cref="Die{T}"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="Die{T}"/> equivalent to the die contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="FormatException"></exception>
		public static Die<T> Parse(string s) {
			ArgumentNullException.ThrowIfNullOrWhiteSpace(s, $"Parameter {nameof(s)} cannot be null, empty, or whitespace.");

			List<string> sections = [.. s.Split(',')];
			Die<T> die = new([]);

			try {
				foreach (string section in sections) {
					die.Faces.Add((T)Convert.ChangeType(section.Trim(), typeof(T)));
				}
			} catch (Exception ex) {
				throw new FormatException($"Parameter {nameof(s)} was not in the right format.", ex);
			}

			return die;
		}

		/// <summary>
		///		<para>Returns a <see cref="string"/> representation of the current <see cref="Die{T}"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="string"/> that represents the current <see cref="Die{T}"/> instance.</para>
		/// </returns>
		public override string ToString() {
			return string.Join(',', Faces);
		}
		#endregion
	}
}