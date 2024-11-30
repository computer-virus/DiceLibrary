namespace DiceLibrary {
	/// <summary>
	///		<para>Represents a <see cref="List{T}"/> of the <see cref="NumberDie"/> class with additional functionality.</para>
	/// </summary>
	public class NumberDice : List<NumberDie> {
		#region Die Population Methods
		/// <summary>
		///		<para>Adds <paramref name="n"/> new instances of <see cref="D"/>(<paramref name="size"/>) to the end of the current <see cref="NumberDice"/> instance.</para>
		/// </summary>
		/// <param name="n"></param>
		/// <param name="size"></param>
		/// <returns>
		///		<para>The current <see cref="NumberDice"/> instance in order to simplify its use in instantiation.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public NumberDice Add(int n, int size) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");
			ArgumentOutOfRangeException.ThrowIfLessThan(size, 2, $"Parameter {nameof(size)} cannot be less than 2.");

			for (int i = 0; i < n; i++) {
				Add(new D(size));
			}

			return this;
		}

		/// <summary>
		///		<para>Creates a new instance of the <see cref="NumberDice"/> class with double the current <see cref="NumberDice"/> instance's <see cref="NumberDie"/> instances.</para>
		/// </summary>
		/// <returns>A new instance of the <see cref="NumberDice"/> instance containing twice as many instances of the <see cref="NumberDie"/> clas as the current <see cref="NumberDie"/> instances.</returns>
		public NumberDice Double() {
			NumberDice dice = [];

			foreach (NumberDie die in this) {
				dice.Add(new NumberDie(die.Faces));
				dice.Add(new NumberDie(die.Faces));
			}

			return dice;
		}
		#endregion

		#region Roll Methods
		/// <summary>
		///		<para>Rolls all of the <see cref="NumberDie"/> instances of the current <see cref="NumberDice"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the <see cref="NumberDie"/> instances.</para>
		/// </returns>
		public List<int> Roll() {
			List<int> rolls = [];

			foreach (NumberDie die in this) {
				rolls.Add(die.Roll());
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="NumberDie"/> instances of the current <see cref="NumberDice"/> instance twice and keeps the higher roll.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the higher randomly rolled faces of the <see cref="NumberDie"/> instances.</para>
		/// </returns>
		public List<int> Advantage() {
			return Advantage(2);
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="NumberDie"/> instances of the current <see cref="NumberDice"/> instance <paramref name="n"/> times and keeps the highest roll.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the highest randomly rolled faces of the <see cref="NumberDie"/> instances.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public List<int> Advantage(int n) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");

			List<int> rolls = [];

			foreach (NumberDie die in this) {
				rolls.Add(die.Advantage(n));
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="NumberDie"/> instances of the current <see cref="NumberDice"/> instance twice and keeps the lower roll.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the lower randomly rolled faces of the <see cref="NumberDie"/> instances.</para>
		/// </returns>
		public List<int> Disadvantage() {
			return Disadvantage(2);
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="NumberDie"/> instances of the current <see cref="NumberDice"/> instance <paramref name="n"/> times and keeps the lowest roll.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the lowest randomly rolled faces of the <see cref="NumberDie"/> instances.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public List<int> Disadvantage(int n) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");

			List<int> rolls = [];

			foreach (NumberDie die in this) {
				rolls.Add(die.Disadvantage(n));
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="NumberDie"/> instances of the current <see cref="NumberDice"/> instance, rerolling once if the instances don't roll higher than <paramref name="value"/>.</para>
		/// </summary>
		/// <param name="value"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the <see cref="NumberDie"/> instances.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public List<int> ReRoll(int value) {
			return ReRoll(value, true);
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="NumberDie"/> instances of the current <see cref="NumberDice"/> instance, rerolling once if <paramref name="once"/> is <see langword="true"/> and the instances don't roll higher than <paramref name="value"/>.</para>
		///		<para>If <paramref name="once"/> is <see langword="false"/>, then the <see cref="NumberDie"/> instances will continue to reroll until they roll higher than <paramref name="value"/>.</para>
		/// </summary>
		/// <param name="value"></param>
		/// <param name="once"></param>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the <see langword="values"/> of the randomly rolled faces of the <see cref="NumberDie"/> instances.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public List<int> ReRoll(int value, bool once) {
			List<int> rolls = [];

			foreach (NumberDie die in this) {
				rolls.Add(die.ReRoll(value, once));
			}

			return rolls;
		}

		/// <summary>
		///		<para>Rolls all of the <see cref="NumberDie"/> instances of the current <see cref="NumberDice"/> instance <paramref name="n"/> times, counting the number of rolls that met or exceeded the target <paramref name="value"/>.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="List{T}"/> of <see cref="int"/>s representing the number of rolls that met or exceeded the target <paramref name="value"/> for each <see cref="NumberDie"/> instance.</para>
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public List<int> Target(int n, int value) {
			ArgumentOutOfRangeException.ThrowIfNegative(n, $"Parameter {nameof(n)} cannot be negative.");

			List<int> successes = [];

			foreach (NumberDie die in this) {
				successes.Add(die.Target(n, value));
			}

			return successes;
		}
		#endregion

		#region Parse Methods
		/// <summary>
		///		<para>Converts a <see cref="string"/>[] representation of a dice list to its <see cref="NumberDice"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="NumberDice"/> equivalent to the dice list contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="FormatException"></exception>
		public static NumberDice Parse(string[] s) {
			ArgumentNullException.ThrowIfNull(s, $"Parameter {nameof(s)} cannot be null.");

			NumberDice dice = [];

			foreach (string die in s) {
				dice.Add(NumberDie.Parse(die));
			}

			return dice;
		}

		/// <summary>
		///		<para>Converts a <see cref="string"/> representation of a dice list to its <see cref="NumberDice"/> equivalent.</para>
		/// </summary>
		/// <param name="s"></param>
		/// <returns>
		///		<para>A <see cref="NumberDice"/> equivalent to the dice list contained in <paramref name="s"/>.</para>
		/// </returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="FormatException"></exception>
		public static NumberDice Parse(string s) {
			ArgumentNullException.ThrowIfNull(s, $"Parameter {nameof(s)} cannot be null.");
			string[] lines = s.Split(Environment.NewLine);

			NumberDice dice = [];

			foreach (string die in lines) {
				dice.Add(NumberDie.Parse(die));
			}

			return dice;
		}
		#endregion

		#region Method Overrides
		/// <summary>
		///		<para>Returns a <see cref="string"/> representation of the current <see cref="NumberDice"/> instance.</para>
		/// </summary>
		/// <returns>
		///		<para>A <see cref="string"/> that represents the current <see cref="NumberDice"/> instance.</para>
		/// </returns>
		public override string ToString() {
			return string.Join(Environment.NewLine, this);
		}
		#endregion
	}
}