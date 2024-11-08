﻿namespace DiceLibrary {
	/// <summary>
	///		<para>Represents a <see cref="List{T}"/> of the <see cref="Die"/> class with additional functionality.</para>
	/// </summary>
	public class Dice : List<Die> {
		#region Roll Methods
		/// <summary>
		///		<para>Rolls all of the <see cref="Die"/> instances of the current <see cref="Dice"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the <see cref="Die"/> instances.</para>
		/// </returns>
		public List<int> Roll() {
			List<int> rolls = [];

			foreach (Die die in this) {
				rolls.Add(die.Roll());
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="Die"/> instances of the current <see cref="Dice"/> instance twice and keeps the higher roll.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the higher randomly rolled faces of the <see cref="Die"/> instances.</para>
		/// </returns>
		public List<int> Advantage() {
			return Advantage(2);
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="Die"/> instances of the current <see cref="Dice"/> instance <paramref name="n"/> times and keeps the highest roll.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the highest randomly rolled faces of the <see cref="Die"/> instances.</para>
		/// </returns>
		public List<int> Advantage(int n) {
			List<int> rolls = [];

			foreach (Die die in this) {
				rolls.Add(die.Advantage(n));
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="Die"/> instances of the current <see cref="Dice"/> instance twice and keeps the lower roll.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the lower randomly rolled faces of the <see cref="Die"/> instances.</para>
		/// </returns>
		public List<int> Disadvantage() {
			return Disadvantage(2);
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="Die"/> instances of the current <see cref="Dice"/> instance <paramref name="n"/> times and keeps the lowest roll.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the lowest randomly rolled faces of the <see cref="Die"/> instances.</para>
		/// </returns>
		public List<int> Disadvantage(int n) {
			List<int> rolls = [];

			foreach (Die die in this) {
				rolls.Add(die.Disadvantage(n));
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="Die"/> instances of the current <see cref="Dice"/> instance, rerolling once if the instances don't roll higher than <paramref name="value"/>.</para>
		/// </summary>
		/// <param name="value"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the <see cref="Die"/> instances.</para>
		/// </returns>
		public List<int> ReRoll(int value) {
			return ReRoll(value, true);
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="Die"/> instances of the current <see cref="Dice"/> instance, rerolling once if <paramref name="once"/> is <see langword="true"/> and the instances don't roll higher than <paramref name="value"/>.</para>
		///		<para>If <paramref name="once"/> is <see langword="false"/>, then the <see cref="Die"/> instances will continue to reroll until they roll higher than <paramref name="value"/>.</para>
		/// </summary>
		/// <param name="value"></param>
		/// <param name="once"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the <see cref="Die"/> instances.</para>
		/// </returns>
		public List<int> ReRoll(int value, bool once) {
			List<int> rolls = [];

			foreach (Die die in this) {
				rolls.Add(die.ReRoll(value, once));
			}

			return rolls;
		}
		#endregion

		#region Parse Methods
		/// <summary>
		///		<para>Converts a <see cref="string"/>[] representation of a dice list to its <see cref="Dice"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="Dice"/> equivalent to the dice list contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static Dice Parse(string[] s) {
			ArgumentNullException.ThrowIfNull(s, $"Parameter {nameof(s)} cannot be null.");

			Dice dice = [];

			foreach (string die in s) {
				dice.Add(Die.Parse(die));
			}

			return dice;
		}

		/// <summary>
		///		<para>Converts a <see cref="string"/> representation of a dice list to its <see cref="Dice"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="Dice"/> equivalent to the dice list contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static Dice Parse(string s) {
			ArgumentNullException.ThrowIfNull(s, $"Parameter {nameof(s)} cannot be null.");
			string[] lines = s.Split(Environment.NewLine);

			Dice dice = [];

			foreach (string die in lines) {
				dice.Add(Die.Parse(die));
			}

			return dice;
		}
		#endregion

		#region Method Overrides
		/// <summary>
		///		<para>Returns a <see cref="string"/> representation of the current <see cref="Dice"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="string"/> that represents the current <see cref="Dice"/> instance.</para>
		/// </returns>
		public override string ToString() {
			return string.Join(Environment.NewLine, this);
		}
		#endregion
	}
}