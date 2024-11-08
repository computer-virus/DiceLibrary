namespace DiceLibrary {
	public abstract class Die {
		#region Data Members
		private int? _size;
		private int[] _faces = [];
		private int? _seed;
		private Random? _random;
		#endregion

		#region Properties
		public int Size {
			get {
				return _size ?? throw new InvalidOperationException($"{nameof(Size)} must be assigned a value before it is accessed.");
			}

			private set {
				if (_size != null) {
					throw new InvalidOperationException($"Cannot reassign the value of {nameof(Size)} once it has been assigned.");
				}

				if (value < 2) {
					throw new ArgumentOutOfRangeException($"{nameof(Size)} cannot be assigned a value that's less than 2.{Environment.NewLine}Assigned Value: {value}");
				}

				_size = value;
				_faces = new int[value];
			}
		}

		public int[] Faces {
			get {
				return _faces;
			}
		}

		public int Seed {
			get {
				return _seed ?? throw new InvalidOperationException($"{nameof(Seed)} must be assigned a value before it is accessed.");
			}

			set {
				ArgumentNullException.ThrowIfNull(value, $"{nameof(Seed)} cannot be assigned to {value}.");
				_seed = value;
				_random = new(value);
			}
		}

		private Random Random {
			get {
				return _random ?? throw new InvalidOperationException($"{nameof(Random)} must be assigned a value before it is accessed.");
			}
		}
		#endregion

		#region Constructors
		protected Die(int size) {
			Size = size;
			Seed = new Random().Next(int.MinValue, int.MaxValue);
		}

		protected Die(int size, int seed) {
			Size = size;
			Seed = seed;
		}
		#endregion

		#region Roll Methods
		public int Roll() {
			return Faces[Random.Next(Size)];
		}

		public List<int> Roll(int times) {
			ArgumentOutOfRangeException.ThrowIfNegative(times, $"Parameter {nameof(times)} cannot be negative.");

			List<int> rolls = [];

			for (int i = 0; i < times; i++) {
				rolls.Add(Roll());
			}

			return rolls;
		}

		public int Advantage() {
			return Advantage(2);
		}

		public int Advantage(int numberOfRolls) {
			return Roll(numberOfRolls).Max();
		}

		public int Disadvantage() {
			return Disadvantage(2);
		}

		public int Disadvantage(int numberOfRolls) {
			return Roll(numberOfRolls).Min();
		}

		public int ReRoll(int value) {
			return ReRoll(value, true);
		}

		public int ReRoll(int value, bool once) {
			if (value > Size && !once) {
				throw new ArgumentOutOfRangeException($"Parameter {nameof(value)} cannot exceed {nameof(Size)}.{Environment.NewLine}{nameof(Size)}: {Size}");
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
		#endregion

		#region Abstract Methods
		protected abstract void SetUpFaces();
		#endregion

		#region Method Overrides
		public override string ToString() {
			return $"{Size}:[{string.Join(',', Faces)}]:{Seed}";
		}
		#endregion
	}
}