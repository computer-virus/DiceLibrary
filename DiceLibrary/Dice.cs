namespace DiceLibrary {
	public class Dice : List<Die> {
		#region Roll Methods
		public List<int> Roll() {
			List<int> rolls = [];

			foreach (Die die in this) {
				rolls.Add(die.Roll());
			}

			return rolls;
		}

		public List<int> Advantage() {
			return Advantage(2);
		}

		public List<int> Advantage(int numberOfRolls) {
			List<int> rolls = [];

			foreach (Die die in this) {
				rolls.Add(die.Advantage(numberOfRolls));
			}

			return rolls;
		}

		public List<int> Disadvantage() {
			return Disadvantage(2);
		}

		public List<int> Disadvantage(int numberOfRolls) {
			List<int> rolls = [];

			foreach (Die die in this) {
				rolls.Add(die.Disadvantage(numberOfRolls));
			}

			return rolls;
		}

		public List<int> ReRoll(int value) {
			return ReRoll(value, true);
		}

		public List<int> ReRoll(int value, bool once) {
			List<int> rolls = [];

			foreach (Die die in this) {
				rolls.Add(die.ReRoll(value, once));
			}

			return rolls;
		}
		#endregion

		#region Method Overrides
		public override string ToString() {
			return string.Join(Environment.NewLine, this);
		}
		#endregion
	}
}