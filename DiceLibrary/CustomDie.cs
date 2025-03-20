using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiceLibrary {
	/// <summary>
	///		<para>A representation of a rollable die with a custom arrangement of <see cref="int"/> type faces with more functionality than its <see cref="GenericDie{T}"/> counterpart.</para>
	/// </summary>
	public class CustomDie : GenericDie<int> {
		#region Properties
		/// <summary>
		///		<para>Gets an <see cref="int"/> representation of the highest value of the faces on the current <see cref="CustomDie"/> instance.</para>
		/// </summary>
		[JsonIgnore]
		public int Max {
			get {
				return Faces.Max();
			}
		}

		/// <summary>
		///		<para>Gets an <see cref="int"/> representation of the lowest value of the faces on the current <see cref="CustomDie"/> instance.</para>
		/// </summary>
		[JsonIgnore]
		public int Min {
			get {
				return Faces.Min();
			}
		}

		/// <summary>
		///		<para>Gets a <see cref="double"/> representation of the average value of the faces on the current <see cref="CustomDie"/> instance.</para>
		/// </summary>
		[JsonIgnore]
		public double Average {
			get {
				return Faces.Average();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		///		<para>Initializes a new instance of the <see cref="CustomDie"/> class, using the specified <paramref name="faces"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		public CustomDie(List<int> faces) : base(faces) { }

		/// <summary>
		///		<para>Initializes a new instance of the <see cref="CustomDie"/> class, using the specified <paramref name="faces"/> and <paramref name="seed"/>.</para>
		/// </summary>
		/// <param name="faces"></param>
		/// <param name="seed"></param>
		[JsonConstructorAttribute]
		public CustomDie(List<int> faces, int? seed) : base(faces, seed) { }
		#endregion

		#region Roll Methods
		/// <summary>
		///		<para>Rolls the current <see cref="CustomDie"/> instance, using the specified rolling <paramref name="method"/>.</para>
		/// </summary>
		/// <param name="method"></param>
		/// <returns>
		///		<para>An <see cref="int"/> representing the <see langword="value"/> of the randomly rolled face of the current <see cref="CustomDie"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public virtual int Roll(RollMethod method) {
			ArgumentOutOfRangeException.ThrowIfLessThan(Size, 1, $"Cannot roll a {nameof(CustomDie)} with {Size} {nameof(Faces)}.");

			if (!Enum.IsDefined(typeof(RollMethod), method)) {
				throw new ArgumentNullException($"Cannot roll a {nameof(CustomDie)} with unrecognized {nameof(RollMethod)} \"{method}\".");
			}

			switch (method) {
				case RollMethod.Advantage:
					return Roll(2).Max();
				case RollMethod.Disadvantage:
					return Roll(2).Min();
				case RollMethod.Exploding:
					if (Min == Max) {
						throw new InvalidOperationException($"Using the {nameof(RollMethod)} \"{method}\" on the current {nameof(CustomDie)} would cause an infinite loop.");
					}

					List<int> rolls = [];

					do {
						rolls.Add(Roll());
					} while (rolls.Last() == Max);

					return rolls.Sum();
				default:
					return Roll();
			}
		}

		/// <summary>
		///		<para>Rolls the current <see cref="CustomDie"/> instance <paramref name="n"/> times, using the specified rolling <paramref name="method"/>.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <param name="method"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the current <see cref="CustomDie"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public virtual List<int> Roll(int n, RollMethod method) {
			ArgumentOutOfRangeException.ThrowIfLessThan(Size, 1, $"Cannot roll a {nameof(CustomDie)} with {Size} {nameof(Faces)}.");
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Cannot roll a {nameof(CustomDie)} {n} times.");

			if (!Enum.IsDefined(typeof(RollMethod), method)) {
				throw new ArgumentNullException($"Cannot roll a {nameof(CustomDie)} with unrecognized {nameof(RollMethod)} \"{method}\".");
			}

			if (method == RollMethod.Exploding && Max == Min) {
				throw new InvalidOperationException($"Using the {nameof(RollMethod)} \"{method}\" on the current {nameof(CustomDie)} would cause an infinite loop.");
			}

			List<int> rolls = [];

			for (int i = 0; i < n; i++) {
				rolls.Add(Roll(method));
			}

			return rolls;
		}
		#endregion

		#region DC Methods
		/// <summary>
		///		<para>Rolls the current <see cref="CustomDie"/> instance and then adds the specified <paramref name="modifier"/>, checking it against the specified <paramref name="dc"/>.</para>
		/// </summary>
		/// <param name="dc"></param>
		/// <param name="modifier"></param>
		/// <returns>
		///		<para>A <see cref="bool"/> representation of whether the roll succeeded against the <paramref name="dc"/>.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public virtual bool DC(int dc, int modifier) {
			ArgumentOutOfRangeException.ThrowIfLessThan(Size, 1, $"Cannot roll a {nameof(CustomDie)} with {Size} {nameof(Faces)}.");

			return DC(dc, RollMethod.Normal, modifier, false);
		}

		/// <summary>
		///		<para>Rolls the current <see cref="CustomDie"/> instance and then adds the specified <paramref name="modifier"/>, checking it against the specified <paramref name="dc"/>.</para>
		/// </summary>
		/// <param name="dc"></param>
		/// <param name="modifier"></param>
		/// <param name="crits"></param>
		/// <returns>
		///		<para>A <see cref="bool"/> representation of whether the roll succeeded against the <paramref name="dc"/>.</para>
		///		<para>The roll automatically succeeds or fails if <paramref name="crits"/> is <see langword="true"/> and the current <see cref="CustomDie"/> instance rolls its highest or lowest value, respectively.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public virtual bool DC(int dc, int modifier, bool crits) {
			ArgumentOutOfRangeException.ThrowIfLessThan(Size, 1, $"Cannot roll a {nameof(CustomDie)} with {Size} {nameof(Faces)}.");

			return DC(dc, RollMethod.Normal, modifier, crits);
		}

		/// <summary>
		///		<para>Rolls the current <see cref="CustomDie"/> instance using the specified <paramref name="method"/> and then adds the specified <paramref name="modifier"/>, checking it against the specified <paramref name="dc"/>.</para>
		/// </summary>
		/// <param name="dc"></param>
		/// <param name="method"></param>
		/// <param name="modifier"></param>
		/// <returns>
		///		<para>A <see cref="bool"/> representation of whether the roll succeeded against the <paramref name="dc"/>.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public virtual bool DC(int dc, RollMethod method, int modifier) {
			ArgumentOutOfRangeException.ThrowIfLessThan(Size, 1, $"Cannot roll a {nameof(CustomDie)} with {Size} {nameof(Faces)}.");

			if (!Enum.IsDefined(typeof(RollMethod), method)) {
				throw new ArgumentNullException($"Cannot roll a {nameof(CustomDie)} with unrecognized {nameof(RollMethod)} \"{method}\".");
			}

			if (method == RollMethod.Exploding && Max == Min) {
				throw new InvalidOperationException($"Using the {nameof(RollMethod)} \"{method}\" on the current {nameof(CustomDie)} would cause an infinite loop.");
			}

			return DC(dc, method, modifier, false);
		}

		/// <summary>
		///		<para>Rolls the current <see cref="CustomDie"/> instance using the specified <paramref name="method"/> and then adds the specified <paramref name="modifier"/>, checking it against the specified <paramref name="dc"/>.</para>
		/// </summary>
		/// <param name="dc"></param>
		/// <param name="method"></param>
		/// <param name="modifier"></param>
		/// <param name="crits"></param>
		/// <returns>
		///		<para>A <see cref="bool"/> representation of whether the roll succeeded against the <paramref name="dc"/>.</para>
		///		<para>The roll automatically succeeds or fails if <paramref name="crits"/> is <see langword="true"/> and the current <see cref="CustomDie"/> instance rolls its highest or lowest value, respectively.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public virtual bool DC(int dc, RollMethod method, int modifier, bool crits) {
			ArgumentOutOfRangeException.ThrowIfLessThan(Size, 1, $"Cannot roll a {nameof(CustomDie)} with {Size} {nameof(Faces)}.");

			if (!Enum.IsDefined(typeof(RollMethod), method)) {
				throw new ArgumentNullException($"Cannot roll a {nameof(CustomDie)} with unrecognized {nameof(RollMethod)} \"{method}\".");
			}

			if (method == RollMethod.Exploding && Max == Min) {
				throw new InvalidOperationException($"Using the {nameof(RollMethod)} \"{method}\" on the current {nameof(CustomDie)} would cause an infinite loop.");
			}

			int roll = Roll(method);

			if (crits && (roll == Max || roll == Min)) {
				return roll == Max;
			}

			return roll + modifier >= dc;
		}
		#endregion

		#region Conversion Methods
		/// <summary>
		///		<para>Converts a <see cref="string"/> representation of a die to its <see cref="CustomDie"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="CustomDie"/> equivalent to the die contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="FormatException"></exception>
		public static new CustomDie Parse(string s) {
			return JsonSerializer.Deserialize<CustomDie>(s) ?? throw new FormatException($"Could not parse \"{s}\" into a {nameof(CustomDie)}.");
		}

		/// <summary>
		///		<para>Returns a <see cref="string"/> representation of the current <see cref="CustomDie"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="string"/> that represents the current <see cref="CustomDie"/> instance.</para>
		/// </returns>
		/// <exception cref="InvalidOperationException"></exception>
		public override string ToString() {
			return JsonSerializer.Serialize(this, JsonOptions) ?? throw new InvalidOperationException($"Could not parse {nameof(CustomDie)} into a string.");
		}
		#endregion
	}
}