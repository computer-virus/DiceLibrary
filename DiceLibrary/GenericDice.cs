using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiceLibrary {
	/// <summary>
	///		<para>Represents a <see cref="List{T}"/> of the <see cref="GenericDie{T}"/> class with additional functionality.</para>
	/// </summary>
	public class GenericDice<T> : List<GenericDie<T>> {
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
		///		<para>Rolls all of the <see cref="GenericDie{T}"/> instances of the current <see cref="GenericDice{T}"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> representing the <see langword="values"/> of the randomly rolled faces of the <see cref="GenericDie{T}"/> instances.</para>
		/// </returns>
		public virtual List<T> Roll() {
			List<T> rolls = [];

			foreach (GenericDie<T> die in this) {
				rolls.Add(die.Roll());
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="GenericDie{T}"/> instances of the current <see cref="GenericDice{T}"/> instance <paramref name="n"/> times.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> representing the <see langword="values"/> of the randomly rolled faces of the <see cref="GenericDie{T}"/> instances.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public virtual List<T> Roll(int n) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Cannot roll {nameof(GenericDice<T>)} {n} times.");

			List<T> rolls = [];

			for (int i = 0; i < n; i++) {
				rolls = [.. rolls.Concat(Roll())];
			}

			return rolls;
		}
		#endregion

		#region Conversion Methods
		/// <summary>
		///		<para>Converts a <see cref="string"/> representation of a die to its <see cref="GenericDice{T}"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="GenericDice{T}"/> equivalent to the dice contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="FormatException"></exception>
		public static GenericDice<T> Parse(string s) {
			return JsonSerializer.Deserialize<GenericDice<T>>(s) ?? throw new FormatException($"Could not parse \"{s}\" into {nameof(GenericDice<T>)}.");
		}

		/// <summary>
		///		<para>Returns a <see cref="string"/> representation of the current <see cref="GenericDice{T}"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="string"/> that represents the current <see cref="GenericDice{T}"/> instance.</para>
		/// </returns>
		public override string ToString() {
			return JsonSerializer.Serialize(this, JsonOptions) ?? throw new InvalidOperationException($"Could not parse {nameof(GenericDice<T>)} into a string.");
		}
		#endregion
	}
}