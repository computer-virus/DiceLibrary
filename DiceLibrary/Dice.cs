namespace DiceLibrary {
	/// <summary>
	///		<para>Represents a <see cref="List{T}"/> of the <see cref="Die{T}"/> class with additional functionality.</para>
	/// </summary>
	public class Dice<T> : List<Die<T>> {
		#region Roll Methods
		/// <summary>
		///		<para>Rolls all of the <see cref="Die{T}"/> instances of the current <see cref="Dice{T}"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> representing the <see langword="values"/> of the randomly rolled faces of the <see cref="Die{T}"/> instances.</para>
		/// </returns>
		public List<T> Roll() {
			List<T> rolls = [];

			foreach (Die<T> die in this) {
				rolls.Add(die.Roll());
			}

			return rolls;
		}
		#endregion

		#region Parse Methods
		/// <summary>
		///		<para>Converts a <see cref="string"/>[] representation of a dice list to its <see cref="Dice{T}"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="Dice{T}"/> equivalent to the dice list contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="FormatException"></exception>
		public static Dice<T> Parse(string[] s) {
			ArgumentNullException.ThrowIfNull(s, $"Parameter {nameof(s)} cannot be null.");

			Dice<T> dice = [];

			foreach (string die in s) {
				dice.Add(Die<T>.Parse(die));
			}

			return dice;
		}

		/// <summary>
		///		<para>Converts a <see cref="string"/> representation of a dice list to its <see cref="Dice{T}"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="Dice{T}"/> equivalent to the dice list contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="FormatException"></exception>
		public static Dice<T> Parse(string s) {
			ArgumentNullException.ThrowIfNull(s, $"Parameter {nameof(s)} cannot be null.");
			
			string[] lines = s.Split(Environment.NewLine);
			Dice<T> dice = [];

			foreach (string die in lines) {
				dice.Add(Die<T>.Parse(die));
			}

			return dice;
		}
		#endregion

		#region Method Overrides
		/// <summary>
		///		<para>Returns a <see cref="string"/> representation of the current <see cref="Dice{T}"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="string"/> that represents the current <see cref="Dice{T}"/> instance.</para>
		/// </returns>
		public override string ToString() {
			return string.Join(Environment.NewLine, this);
		}
		#endregion
	}
}