using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiceLibrary {
	/// <summary>
	///		<para>Represents a <see cref="List{T}"/> of the <see cref="CustomDie"/> class with additional functionality.</para>
	/// </summary>
	public class Dice : List<CustomDie> {
		#region Properties
		/// <summary>
		///		<para>Gets the <see cref="JsonSerializerOptions"/> for string serialization/deserialization purposes.</para>
		/// </summary>
		[JsonIgnore]
		protected static readonly JsonSerializerOptions JsonOptions = new() {
			WriteIndented = true
		};
		#endregion

		#region Roll Methods
		/// <summary>
		///		<para>Rolls all of the <see cref="CustomDie"/> instances of the current <see cref="Dice"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the <see cref="CustomDie"/> instances.</para>
		/// </returns>
		public virtual List<int> Roll() {
			List<int> rolls = [];

			foreach (CustomDie die in this) {
				rolls.Add(die.Roll());
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="CustomDie"/> instances of the current <see cref="Dice"/> instance <paramref name="n"/> times.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the <see cref="CustomDie"/> instances.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public virtual List<int> Roll(int n) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Cannot roll {nameof(Dice)} {n} times.");

			List<int> rolls = [];

			for (int i = 0; i < n; i++) {
				rolls = [.. rolls.Concat(Roll())];
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="CustomDie"/> instances of the current <see cref="Dice"/> instance, using the specified rolling <paramref name="method"/>.</para>
		/// </summary>
		/// <param name="method"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the <see cref="CustomDie"/> instances.</para>
		/// </returns>
		public virtual List<int> Roll(RollMethod method) {
			List<int> rolls = [];

			foreach (CustomDie die in this) {
				rolls.Add(die.Roll(method));
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="CustomDie"/> instances of the current <see cref="Dice"/> instance, using the specified rolling <paramref name="method"/>, <paramref name="n"/> times.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <param name="method"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the <see cref="CustomDie"/> instances.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public virtual List<int> Roll(int n, RollMethod method) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Cannot roll {nameof(Dice)} {n} times.");

			List<int> rolls = [];

			for (int i = 0; i < n; i++) {
				rolls = [.. rolls.Concat(Roll(method))];
			}

			return rolls;
		}
		#endregion

		#region Conversion Methods
		/// <summary>
		///		<para>Converts a <see cref="string"/> representation of a die to its <see cref="Dice"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="Dice"/> equivalent to the dice contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="FormatException"></exception>
		public static Dice Parse(string s) {
			return JsonSerializer.Deserialize<Dice>(s) ?? throw new FormatException($"Could not parse \"{s}\" into {nameof(Dice)}.");
		}

		/// <summary>
		///		<para>Returns a <see cref="string"/> representation of the current <see cref="Dice"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="string"/> that represents the current <see cref="Dice"/> instance.</para>
		/// </returns>
		public override string ToString() {
			return JsonSerializer.Serialize(this, JsonOptions) ?? throw new InvalidOperationException($"Could not parse {nameof(Dice)} into a string.");
		}
		#endregion
	}
}