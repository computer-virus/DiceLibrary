# DiceLibrary
A small library consisting of a couple of classes for the handling of dice and dice rolling.

## Available Classes
- Die\<T>: A simpler, rollable die with a custom arrangement of \<T> type faces
- Dice\<T>: A list of the Die\<T> class with extra functionality
- NumberDie (derived from Die\<int>): A rollable die with a custom arrangement of faces
- D (derived from NumberDie): A rollable die with the standard arrangement of faces
- CustomDie (derived from NumberDie): A rollable die with a custom arrangement of faces and probabilities to roll said faces
- NumberDice: A list of the NumberDie class with extra functionality

## Code Example
```cs
using DiceLibrary;

namespace Lv5AssassinRogueDemo {
	internal class Program {
		static void Main(string[] args) {
			bool isSurprising = new Random().Next(2) == 1; // 50% Chance To Be Surprising
			bool ally5ft = new Random().Next(2) == 1; // 50% Chance To Have Ally Within 5ft Of Target

			int proficiencyBonus = 3;
			int modifier = 5;

			NumberDie d20 = new D(20);

			NumberDice weaponDamage = [new D(8)];
			NumberDice sneakAttackDamage = new Dice().Add(3, 6);

			int toHit;
			if (isSurprising) {
				toHit = d20.Advantage() + modifier + proficiencyBonus;
			} else {
				toHit = d20.Roll() + modifier + proficiencyBonus;
			}

			bool crit = isSurprising || toHit == d20.Max + modifier + proficiencyBonus;
			if (crit) {
				weaponDamage = weaponDamage.Double();
				sneakAttackDamage = sneakAttackDamage.Double();
			}

			int damage = weaponDamage.Roll().Sum() + modifier;
			if (isSurprising || ally5ft) {
				damage += sneakAttackDamage.Roll().Sum();
			}

			Console.WriteLine($"Surprising Enemy: {isSurprising}");
			Console.WriteLine($"Ally Within 5ft Of Enemy: {ally5ft}");
			Console.WriteLine($"To-Hit Roll: {toHit}");
			Console.WriteLine($"Critical Hit: {crit}");
			Console.WriteLine($"Damage Roll: {damage}");
		}
	}
}
```