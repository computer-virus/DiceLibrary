using DiceLibrary;

namespace DiceTest {
	internal class Program {
		static void Main(string[] args) {
			Die d4 = new D(4);
			Die d6 = new D(6);
			Die d20 = new D(20);
			Die custom = new CustomDie([1, 1, 2, 2, 3, 3]);

			Dice dice = [d4, d6, d20, custom];

            Console.WriteLine(d4.Roll());
			Console.WriteLine(string.Join(',', d6.Roll(10)));
            Console.WriteLine(d4.ReRoll(3, false));
			Console.WriteLine(d20.Advantage());
			Console.WriteLine(d20.Disadvantage());
			Console.WriteLine(string.Join(',', custom.Faces));
			Console.WriteLine(string.Join(',', dice.Roll()));
            Console.WriteLine();
            Console.WriteLine(dice.ToString());
		}
	}
}
