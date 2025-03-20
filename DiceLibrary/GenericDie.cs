using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiceLibrary {
	/// <summary>
	/// <para>A generic representation of a rollable die with a custom arrangement of <typeparamref name="T"/> type faces.</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class GenericDie<T> {
		#region Data Members
		private List<T> _faces = [];
		private int? _seed;
		private Random _random = new();
		#endregion

		#region Properties
		/// <summary>
		///		<para>Gets the <see cref="JsonSerializerOptions"/> for string serialization/deserialization purposes.</para>
		/// </summary>
		[JsonIgnore]
		protected static readonly JsonSerializerOptions JsonOptions = new() {
			WriteIndented = true
		};

		/// <summary>
		///		<para>Gets or sets the <see cref="List{T}"/> representation of the faces of the current <see cref="GenericDie{T}"/> instance.</para>
		/// </summary>
		public List<T> Faces {
			get {
				return _faces;
			}

			set {
				_faces = value ?? [];
			}
		}

		/// <summary>
		///		<para>Gets an <see cref="int"/> representation of the number of faces of the current <see cref="GenericDie{T}"/> instance.</para>
		/// </summary>
		[JsonIgnore]
		public int Size {
			get {
				return Faces.Count;
			}
		}

		/// <summary>
		///		<para>Gets or sets the seed used to determine the current <see cref="GenericDie{T}"/> instance's rolls.</para>
		///		<para>A <see langword="null"/> value unsets the seed which randomizes the rolls.</para>
		/// </summary>
		public int? Seed {
			get {
				return _seed;
			}

			set {
				_seed = value;

				if (_seed.HasValue) {
					_random = new(_seed.Value);	
				} else {
					_random = new();
				}
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		///		<para>Initializes a new instance of the <see cref="GenericDie{T}"/> class, using the specified <paramref name="faces"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		public GenericDie(List<T> faces) {
			Faces = faces;
		}

		/// <summary>
		///		<para>Initializes a new instance of the <see cref="GenericDie{T}"/> class, using the specified <paramref name="faces"/> and <paramref name="seed"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		/// <param name="seed"></param>
		[JsonConstructorAttribute]
		public GenericDie(List<T> faces, int? seed) {
			Faces = faces;
			Seed = seed;
		}
		#endregion

		#region Roll Methods
		/// <summary>
		///		<para>Rolls the current <see cref="GenericDie{T}"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <typeparamref name="T"/> representing the <see langword="value"/> of the randomly rolled face of the current <see cref="GenericDie{T}"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public virtual T Roll() {
			ArgumentOutOfRangeException.ThrowIfLessThan(Size, 1, $"Cannot roll a {nameof(GenericDie<T>)} with {Size} {nameof(Faces)}.");

			return Faces[_random.Next(Size)];
		}

		/// <summary>
		///		<para>Rolls the current <see cref="GenericDie{T}"/> instance <paramref name="n"/> times.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> representing the <see langword="values"/> of the randomly rolled faces of the current <see cref="GenericDie{T}"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public virtual List<T> Roll(int n) {
			ArgumentOutOfRangeException.ThrowIfLessThan(Size, 1, $"Cannot roll a {nameof(GenericDie<T>)} with {Size} {nameof(Faces)}.");
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Cannot roll a {nameof(GenericDie<T>)} {n} times.");

			List<T> rolls = [];

			for (int i = 0; i < n; i++) {
				rolls.Add(Roll());
			}

			return rolls;
		}
		#endregion

		#region Conversion Methods
		/// <summary>
		///		<para>Converts a <see cref="string"/> representation of a die to its <see cref="GenericDie{T}"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="GenericDie{T}"/> equivalent to the die contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="FormatException"></exception>
		public static GenericDie<T> Parse(string s) {
			return JsonSerializer.Deserialize<GenericDie<T>>(s) ?? throw new FormatException($"Could not parse \"{s}\" into a {nameof(GenericDie<T>)}.");
		}

		/// <summary>
		///		<para>Returns a <see cref="string"/> representation of the current <see cref="GenericDie{T}"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="string"/> that represents the current <see cref="GenericDie{T}"/> instance.</para>
		/// </returns>
		/// <exception cref="InvalidOperationException"></exception>
		public override string ToString() {
			return JsonSerializer.Serialize(this, JsonOptions) ?? throw new InvalidOperationException($"Could not parse {nameof(GenericDie<T>)} into a string.");
		}
		#endregion
	}
}